namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Hyperar.HPA.Domain.Interfaces;
    using Hyperar.HPA.Shared.Enums;
    using Microsoft.EntityFrameworkCore;
    using Models = Shared.Models.Hattrick;

    public class MatchDetails : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.League> leagueRepository;

        private readonly IRepository<Domain.Senior.MatchArena> matchArenaRepository;

        private readonly IRepository<Domain.Senior.MatchEvent> matchEventRepository;

        private readonly IRepository<Domain.Senior.MatchOfficial> matchOfficialRepository;

        private readonly IHattrickRepository<Domain.Senior.Match> matchRepository;

        private readonly IRepository<Domain.Senior.MatchTeamBooking> matchTeamBookingRepository;

        private readonly IRepository<Domain.Senior.MatchTeamGoal> matchTeamGoalRepository;

        private readonly IRepository<Domain.Senior.MatchTeamInjury> matchTeamInjuryRepository;

        private readonly IRepository<Domain.Senior.MatchTeam> matchTeamRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        public MatchDetails(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.League> leagueRepository,
            IRepository<Domain.Senior.MatchArena> matchArenaRepository,
            IRepository<Domain.Senior.MatchEvent> matchEventRepository,
            IRepository<Domain.Senior.MatchOfficial> matchOfficialRepository,
            IHattrickRepository<Domain.Senior.Match> matchRepository,
            IRepository<Domain.Senior.MatchTeamBooking> matchTeamBookingRepository,
            IRepository<Domain.Senior.MatchTeamGoal> matchTeamGoalRepository,
            IRepository<Domain.Senior.MatchTeamInjury> matchTeamInjuryRepository,
            IRepository<Domain.Senior.MatchTeam> matchTeamRepository,
            IHattrickRepository<Domain.Senior.Team> teamRepository)
        {
            this.databaseContext = databaseContext;
            this.leagueRepository = leagueRepository;
            this.matchArenaRepository = matchArenaRepository;
            this.matchEventRepository = matchEventRepository;
            this.matchOfficialRepository = matchOfficialRepository;
            this.matchRepository = matchRepository;
            this.matchTeamBookingRepository = matchTeamBookingRepository;
            this.matchTeamGoalRepository = matchTeamGoalRepository;
            this.matchTeamInjuryRepository = matchTeamInjuryRepository;
            this.matchTeamRepository = matchTeamRepository;
            this.teamRepository = teamRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.MatchDetails.HattrickData file)
            {
                ArgumentNullException.ThrowIfNull(fileDownloadTask.ContextId);

                var match = await this.ProcessMatchAsync(file.Match, file.SourceSystem, fileDownloadTask.ContextId.Value);

                await this.ProcessMatchArenaAsync(file.Match.Arena, match);

                if (file.Match.EventList != null)
                {
                    foreach (var xmlEvent in file.Match.EventList)
                    {
                        await this.ProcessMatchEventAsync(xmlEvent, match);
                    }
                }

                if (file.Match.MatchOfficials != null)
                {
                    // Some matches are broken and don't have all match officials.
                    if (file.Match.MatchOfficials.Referee.RefereeId > 0)
                    {
                        await this.ProcessMatchOfficialAsync(file.Match.MatchOfficials.Referee, match);
                    }

                    if (file.Match.MatchOfficials.RefereeAssistant1.RefereeId > 0)
                    {
                        await this.ProcessMatchOfficialAsync(file.Match.MatchOfficials.RefereeAssistant1, match);
                    }

                    if (file.Match.MatchOfficials.RefereeAssistant2.RefereeId > 0)
                    {
                        await this.ProcessMatchOfficialAsync(file.Match.MatchOfficials.RefereeAssistant2, match);
                    }
                }

                var homeTeam = await this.ProcessMatchTeamAsync(
                    file.Match.HomeTeam,
                    MatchTeamLocation.Home,
                    file.Match.PossessionFirstHalfHome,
                    file.Match.PossessionSecondHalfHome,
                    match);

                var awayTeam = await this.ProcessMatchTeamAsync(
                    file.Match.AwayTeam,
                    MatchTeamLocation.Away,
                    file.Match.PossessionFirstHalfAway,
                    file.Match.PossessionSecondHalfAway,
                    match);

                if (file.Match.Bookings != null)
                {
                    foreach (var xmlBooking in file.Match.Bookings)
                    {
                        var matchTeam = xmlBooking.BookingTeamId == homeTeam.HattrickId
                                      ? homeTeam
                                      : awayTeam;

                        await this.ProcessMatchTeamBookingAsync(xmlBooking, matchTeam);
                    }
                }

                if (file.Match.Scorers != null)
                {
                    foreach (var xmlGoal in file.Match.Scorers)
                    {
                        var matchTeam = xmlGoal.ScorerTeamId == homeTeam.HattrickId
                                      ? homeTeam
                                      : awayTeam;

                        await this.ProcessMatchTeamGoalAsync(xmlGoal, matchTeam);
                    }
                }

                if (file.Match.Injuries != null)
                {
                    foreach (var xmlInjury in file.Match.Injuries)
                    {
                        var matchTeam = xmlInjury.InjuryTeamId == homeTeam.HattrickId
                                      ? homeTeam
                                      : awayTeam;

                        await this.ProcessMatchTeamInjuryAsync(xmlInjury, matchTeam);
                    }
                }
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.MatchDetails.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessMatchArenaAsync(
            Models.MatchDetails.Arena xmlArena,
            Domain.Senior.Match match)
        {
            var matchArena = await this.matchArenaRepository.Query(x => x.MatchHattrickId == match.HattrickId)
                                                            .SingleOrDefaultAsync();

            if (matchArena == null)
            {
                await this.matchArenaRepository.InsertAsync(
                    Domain.Senior.MatchArena.Create(
                        xmlArena,
                        match));
            }
            else if (matchArena.HasChanged(xmlArena))
            {
                matchArena.Update(xmlArena);

                this.matchArenaRepository.Update(matchArena);
            }
        }

        private async Task<Domain.Senior.Match> ProcessMatchAsync(
            Models.MatchDetails.Match xmlMatch,
            string sourceSytem,
            long teamId)
        {
            var match = await this.matchRepository.GetByHattrickIdAsync(xmlMatch.MatchId);

            MatchResult? matchResult = null;

            if (xmlMatch.FinishedDate.HasValue)
            {
                if (xmlMatch.HomeTeam.Goals == xmlMatch.AwayTeam.Goals)
                {
                    matchResult = MatchResult.Drawn;
                }
                else if (xmlMatch.HomeTeam.TeamId == teamId)
                {
                    matchResult = xmlMatch.HomeTeam.Goals > xmlMatch.AwayTeam.Goals
                                ? MatchResult.Won
                                : MatchResult.Lost;
                }
                else
                {
                    matchResult = xmlMatch.HomeTeam.Goals < xmlMatch.AwayTeam.Goals
                                ? MatchResult.Won
                                : MatchResult.Lost;
                }
            }

            if (match == null)
            {
                var team = await this.teamRepository.GetByHattrickIdAsync(teamId);

                ArgumentNullException.ThrowIfNull(team, nameof(team));

                match = await this.matchRepository.InsertAsync(
                    Domain.Senior.Match.Create(
                        xmlMatch,
                        sourceSytem,
                        matchResult,
                        team));
            }
            else if (match.HasChanged(xmlMatch, matchResult))
            {
                match.Update(xmlMatch, matchResult);

                this.matchRepository.Update(match);
            }

            return match;
        }

        private async Task ProcessMatchEventAsync(Models.MatchDetails.Event xmlEvent, Domain.Senior.Match match)
        {
            var matchEvent = await this.matchEventRepository.Query(x => x.MatchHattrickId == match.HattrickId
                                                                     && x.Index == xmlEvent.Index)
                                                            .SingleOrDefaultAsync();

            if (matchEvent == null)
            {
                await this.matchEventRepository.InsertAsync(
                    Domain.Senior.MatchEvent.Create(
                        xmlEvent,
                        match));
            }
            else if (matchEvent.HasChanged(xmlEvent))
            {
                matchEvent.Update(xmlEvent);

                this.matchEventRepository.Update(matchEvent);
            }
        }

        private async Task ProcessMatchOfficialAsync(Models.MatchDetails.Referee xmlOfficial, Domain.Senior.Match match)
        {
            var matchOfficial = await this.matchOfficialRepository.Query(x => x.HattrickId == xmlOfficial.RefereeId
                                                                           && x.MatchHattrickId == match.HattrickId)
                                                                  .SingleOrDefaultAsync();

            if (matchOfficial == null)
            {
                // The field is named Country, but it actually is the League ID.
                var league = await this.leagueRepository.GetByHattrickIdAsync(xmlOfficial.RefereeCountryId);

                ArgumentNullException.ThrowIfNull(league, nameof(league));
                ArgumentNullException.ThrowIfNull(league.Country, nameof(league.Country));

                await this.matchOfficialRepository.InsertAsync(
                    Domain.Senior.MatchOfficial.Create(
                        xmlOfficial,
                        match,
                        league.Country));
            }
        }

        private async Task<Domain.Senior.MatchTeam> ProcessMatchTeamAsync(
            Models.MatchDetails.Team xmlTeam,
            MatchTeamLocation location,
            byte? firstHalfPosession,
            byte? secondHalfPosession,
            Domain.Senior.Match match)
        {
            var matchTeam = await this.matchTeamRepository.Query(x => x.HattrickId == xmlTeam.TeamId
                                                                   && x.MatchHattrickId == match.HattrickId)
                                                          .SingleOrDefaultAsync();

            if (matchTeam == null)
            {
                matchTeam = await this.matchTeamRepository.InsertAsync(
                    Domain.Senior.MatchTeam.Create(
                        xmlTeam,
                        location,
                        firstHalfPosession,
                        secondHalfPosession,
                        match));
            }
            else if (matchTeam.HasChanged(xmlTeam, firstHalfPosession, secondHalfPosession))
            {
                matchTeam.Update(xmlTeam, firstHalfPosession, secondHalfPosession);

                this.matchTeamRepository.Update(matchTeam);
            }

            return matchTeam;
        }

        private async Task ProcessMatchTeamBookingAsync(Models.MatchDetails.Booking xmlBooking, Domain.Senior.MatchTeam matchTeam)
        {
            var booking = await this.matchTeamBookingRepository.Query(x => x.MatchTeamId == matchTeam.Id
                                                                        && x.Index == xmlBooking.Index)
                                                               .SingleOrDefaultAsync();

            if (booking == null)
            {
                await this.matchTeamBookingRepository.InsertAsync(
                    Domain.Senior.MatchTeamBooking.Create(
                        xmlBooking,
                        matchTeam));
            }
        }

        private async Task ProcessMatchTeamGoalAsync(Models.MatchDetails.Goal xmlGoal, Domain.Senior.MatchTeam matchTeam)
        {
            var goal = await this.matchTeamGoalRepository.Query(x => x.MatchTeamId == matchTeam.Id
                                                                  && x.Index == xmlGoal.Index)
                                                         .SingleOrDefaultAsync();

            if (goal == null)
            {
                await this.matchTeamGoalRepository.InsertAsync(
                    Domain.Senior.MatchTeamGoal.Create(
                        xmlGoal,
                        matchTeam));
            }
        }

        private async Task ProcessMatchTeamInjuryAsync(Models.MatchDetails.Injury xmlInjury, Domain.Senior.MatchTeam matchTeam)
        {
            var injury = await this.matchTeamInjuryRepository.Query(x => x.MatchTeamId == matchTeam.Id
                                                                      && x.Index == xmlInjury.Index)
                                                             .SingleOrDefaultAsync();

            if (injury == null)
            {
                await this.matchTeamInjuryRepository.InsertAsync(
                    Domain.Senior.MatchTeamInjury.Create(
                        xmlInjury,
                        matchTeam));
            }
        }
    }
}