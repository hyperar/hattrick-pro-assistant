namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Shared.Enums;
    using Shared.ExtensionMethods;
    using Models = Shared.Models.Hattrick;

    public class MatchDetails : DownloadTaskStrategyBase, IPersisterStrategy
    {
        private readonly IAuditableRepository<Domain.Junior.MatchArena> juniorMatchArenaRepository;

        private readonly IAuditableRepository<Domain.Junior.MatchEvent> juniorMatchEventRepository;

        private readonly IHattrickRepository<Domain.Junior.Match> juniorMatchRepository;

        private readonly IAuditableRepository<Domain.Junior.MatchTeamBooking> juniorMatchTeamBookingRepository;

        private readonly IAuditableRepository<Domain.Junior.MatchTeamGoal> juniorMatchTeamGoalRepository;

        private readonly IAuditableRepository<Domain.Junior.MatchTeamInjury> juniorMatchTeamInjuryRepository;

        private readonly IAuditableRepository<Domain.Junior.MatchTeam> juniorMatchTeamRepository;

        private readonly IHattrickRepository<Domain.Junior.Team> juniorTeamRepository;

        private readonly IHattrickRepository<Domain.Junior.UpcomingMatch> juniorUpcomingMatchRepository;

        private readonly IHattrickRepository<Domain.League> leagueRepository;

        private readonly IAuditableRepository<Domain.Senior.MatchArena> seniorMatchArenaRepository;

        private readonly IAuditableRepository<Domain.Senior.MatchEvent> seniorMatchEventRepository;

        private readonly IAuditableRepository<Domain.Senior.MatchReferee> seniorMatchRefereeRepository;

        private readonly IHattrickRepository<Domain.Senior.Match> seniorMatchRepository;

        private readonly IAuditableRepository<Domain.Senior.MatchTeamBooking> seniorMatchTeamBookingRepository;

        private readonly IAuditableRepository<Domain.Senior.MatchTeamGoal> seniorMatchTeamGoalRepository;

        private readonly IAuditableRepository<Domain.Senior.MatchTeamInjury> seniorMatchTeamInjuryRepository;

        private readonly IAuditableRepository<Domain.Senior.MatchTeam> seniorMatchTeamRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> seniorTeamRepository;

        private readonly IHattrickRepository<Domain.Senior.UpcomingMatch> seniorUpcomingMatchRepository;

        public MatchDetails(
            IAuditableRepository<Domain.Junior.MatchArena> juniorMatchArenaRepository,
            IAuditableRepository<Domain.Junior.MatchEvent> juniorMatchEventRepository,
            IHattrickRepository<Domain.Junior.Match> juniorMatchRepository,
            IAuditableRepository<Domain.Junior.MatchTeamBooking> juniorMatchTeamBookingRepository,
            IAuditableRepository<Domain.Junior.MatchTeamGoal> juniorMatchTeamGoalRepository,
            IAuditableRepository<Domain.Junior.MatchTeamInjury> juniorMatchTeamInjuryRepository,
            IAuditableRepository<Domain.Junior.MatchTeam> juniorMatchTeamRepository,
            IHattrickRepository<Domain.Junior.Team> juniorTeamRepository,
            IHattrickRepository<Domain.Junior.UpcomingMatch> juniorUpcomingMatchRepository,
            IHattrickRepository<Domain.League> leagueRepository,
            IAuditableRepository<Domain.Senior.MatchArena> seniorMatchArenaRepository,
            IAuditableRepository<Domain.Senior.MatchEvent> seniorMatchEventRepository,
            IAuditableRepository<Domain.Senior.MatchReferee> seniorMatchRefereeRepository,
            IHattrickRepository<Domain.Senior.Match> seniorMatchRepository,
            IAuditableRepository<Domain.Senior.MatchTeamBooking> seniorMatchTeamBookingRepository,
            IAuditableRepository<Domain.Senior.MatchTeamGoal> seniorMatchTeamGoalRepository,
            IAuditableRepository<Domain.Senior.MatchTeamInjury> seniorMatchTeamInjuryRepository,
            IAuditableRepository<Domain.Senior.MatchTeam> seniorMatchTeamRepository,
            IHattrickRepository<Domain.Senior.Team> seniorTeamRepository,
            IHattrickRepository<Domain.Senior.UpcomingMatch> seniorUpcomingMatchRepository)
        {
            this.juniorMatchArenaRepository = juniorMatchArenaRepository;
            this.juniorMatchEventRepository = juniorMatchEventRepository;
            this.juniorMatchRepository = juniorMatchRepository;
            this.juniorMatchTeamBookingRepository = juniorMatchTeamBookingRepository;
            this.juniorMatchTeamGoalRepository = juniorMatchTeamGoalRepository;
            this.juniorMatchTeamInjuryRepository = juniorMatchTeamInjuryRepository;
            this.juniorMatchTeamRepository = juniorMatchTeamRepository;
            this.juniorTeamRepository = juniorTeamRepository;
            this.juniorUpcomingMatchRepository = juniorUpcomingMatchRepository;
            this.leagueRepository = leagueRepository;
            this.seniorMatchArenaRepository = seniorMatchArenaRepository;
            this.seniorMatchEventRepository = seniorMatchEventRepository;
            this.seniorMatchRefereeRepository = seniorMatchRefereeRepository;
            this.seniorMatchRepository = seniorMatchRepository;
            this.seniorMatchTeamBookingRepository = seniorMatchTeamBookingRepository;
            this.seniorMatchTeamGoalRepository = seniorMatchTeamGoalRepository;
            this.seniorMatchTeamInjuryRepository = seniorMatchTeamInjuryRepository;
            this.seniorMatchTeamRepository = seniorMatchTeamRepository;
            this.seniorTeamRepository = seniorTeamRepository;
            this.seniorUpcomingMatchRepository = seniorUpcomingMatchRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(task.ContextId, nameof(task.ContextId));

            if (task.XmlFile is Models.MatchDetails.HattrickData file)
            {
                if (file.Match.FinishedDate is null)
                {
                    await this.ProcessUpcomingMatchFileAsync(
                        file.Match,
                        file.SourceSystem.ToMatchSystem(),
                        task.ContextId.Value,
                        cancellationToken);
                }
                else
                {
                    await this.ProcessFinishedMatchFileAsync(
                        file.Match,
                        file.SourceSystem.ToMatchSystem(),
                        task.ContextId.Value,
                        cancellationToken);
                }
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }

        private async Task<Domain.Junior.Match> ProcessFinishedJuniorMatchAsync(
            Models.MatchDetails.Match xmlMatch,
            MatchSystem matchSystem,
            Domain.Junior.Team juniorTeam,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(xmlMatch.AddedMinutes, nameof(xmlMatch.AddedMinutes));

            var match = await this.juniorMatchRepository.GetByHattrickIdAsync(xmlMatch.MatchId);

            if (match == null)
            {
                match = await this.juniorMatchRepository.InsertAsync(
                    new Domain.Junior.Match
                    {
                        HattrickId = xmlMatch.MatchId,
                        Date = xmlMatch.MatchDate,
                        System = matchSystem,
                        Type = (MatchType)xmlMatch.MatchType,
                        Rules = (MatchRule)xmlMatch.MatchRuleId,
                        AddedMinutes = xmlMatch.AddedMinutes.Value,
                        Team = juniorTeam
                    });
            }

            return match;
        }

        private async Task ProcessFinishedMatchFileAsync(
            Models.MatchDetails.Match xmlMatch,
            MatchSystem matchSystem,
            long teamId,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(xmlMatch.EventList, nameof(xmlMatch.EventList));
            ArgumentNullException.ThrowIfNull(xmlMatch.Bookings, nameof(xmlMatch.Bookings));
            ArgumentNullException.ThrowIfNull(xmlMatch.Injuries, nameof(xmlMatch.Injuries));
            ArgumentNullException.ThrowIfNull(xmlMatch.Scorers, nameof(xmlMatch.Scorers));
            ArgumentNullException.ThrowIfNull(xmlMatch.PossessionFirstHalfHome, nameof(xmlMatch.PossessionFirstHalfHome));
            ArgumentNullException.ThrowIfNull(xmlMatch.PossessionSecondHalfHome, nameof(xmlMatch.PossessionSecondHalfHome));
            ArgumentNullException.ThrowIfNull(xmlMatch.PossessionFirstHalfAway, nameof(xmlMatch.PossessionFirstHalfAway));
            ArgumentNullException.ThrowIfNull(xmlMatch.PossessionSecondHalfAway, nameof(xmlMatch.PossessionSecondHalfAway));

            if (matchSystem == MatchSystem.Youth)
            {
                var juniorTeam = await this.juniorTeamRepository.GetByHattrickIdAsync(teamId);

                ArgumentNullException.ThrowIfNull(juniorTeam, nameof(juniorTeam));

                var juniorMatch = await this.ProcessFinishedJuniorMatchAsync(
                    xmlMatch,
                    matchSystem,
                    juniorTeam,
                    cancellationToken);

                await this.ProcessJuniorMatchArenaAsync(xmlMatch.Arena, juniorMatch);

                var homeMatchTeam = await this.ProcessJuniorMatchTeamAsync(
                    xmlMatch.HomeTeam,
                    MatchTeamLocation.Home,
                    xmlMatch.PossessionFirstHalfHome.Value,
                    xmlMatch.PossessionSecondHalfHome.Value,
                    juniorMatch);

                var awayMatchTeam = await this.ProcessJuniorMatchTeamAsync(
                    xmlMatch.AwayTeam,
                    MatchTeamLocation.Away,
                    xmlMatch.PossessionFirstHalfAway.Value,
                    xmlMatch.PossessionSecondHalfAway.Value,
                    juniorMatch);

                foreach (var xmlEvent in xmlMatch.EventList)
                {
                    await this.ProcessJuniorMatchEventAsync(xmlEvent, juniorMatch);
                }

                foreach (var xmlBooking in xmlMatch.Bookings)
                {
                    var bookingSeniorTeam = xmlBooking.BookingTeamId == homeMatchTeam.TeamHattrickId
                        ? homeMatchTeam
                        : awayMatchTeam;

                    await this.ProcessJuniorMatchTeamBookingAsync(xmlBooking, bookingSeniorTeam);
                }

                foreach (var xmlInjury in xmlMatch.Injuries)
                {
                    var injuryJuniorTeam = xmlInjury.InjuryTeamId == homeMatchTeam.TeamHattrickId
                        ? homeMatchTeam
                        : awayMatchTeam;

                    await this.ProcessJuniorMatchTeamInjuryAsync(xmlInjury, injuryJuniorTeam);
                }

                foreach (var xmlGoal in xmlMatch.Scorers)
                {
                    var goalJuniorTeam = xmlGoal.ScorerTeamId == homeMatchTeam.TeamHattrickId
                        ? homeMatchTeam
                        : awayMatchTeam;

                    await this.ProcessJuniorMatchTeamGoalAsync(xmlGoal, goalJuniorTeam);
                }
            }
            else
            {
                var seniorTeam = await this.seniorTeamRepository.GetByHattrickIdAsync(teamId);

                ArgumentNullException.ThrowIfNull(seniorTeam, nameof(seniorTeam));

                var seniorMatch = await this.ProcessFinishedSeniorMatchAsync(
                    xmlMatch,
                    matchSystem,
                    seniorTeam,
                    cancellationToken);

                await this.ProcessSeniorMatchArenaAsync(xmlMatch.Arena, seniorMatch);

                var homeMatchTeam = await this.ProcessSeniorMatchTeamAsync(
                    xmlMatch.HomeTeam,
                    MatchTeamLocation.Home,
                    xmlMatch.PossessionFirstHalfHome.Value,
                    xmlMatch.PossessionSecondHalfHome.Value,
                    seniorMatch,
                    cancellationToken);

                var awayMatchTeam = await this.ProcessSeniorMatchTeamAsync(
                    xmlMatch.AwayTeam,
                    MatchTeamLocation.Away,
                    xmlMatch.PossessionFirstHalfAway.Value,
                    xmlMatch.PossessionSecondHalfAway.Value,
                    seniorMatch,
                    cancellationToken);

                foreach (var xmlEvent in xmlMatch.EventList)
                {
                    await this.ProcessSeniorMatchEventAsync(xmlEvent, seniorMatch);
                }

                foreach (var xmlBooking in xmlMatch.Bookings)
                {
                    var bookingSeniorTeam = xmlBooking.BookingTeamId == homeMatchTeam.TeamHattrickId
                        ? homeMatchTeam
                        : awayMatchTeam;

                    await this.ProcessSeniorMatchTeamBookingAsync(xmlBooking, bookingSeniorTeam);
                }

                foreach (var xmlInjury in xmlMatch.Injuries)
                {
                    var injurySeniorTeam = xmlInjury.InjuryTeamId == homeMatchTeam.TeamHattrickId
                        ? homeMatchTeam
                        : awayMatchTeam;

                    await this.ProcessSeniorMatchTeamInjuryAsync(xmlInjury, injurySeniorTeam);
                }

                foreach (var xmlGoal in xmlMatch.Scorers)
                {
                    var goalSeniorTeam = xmlGoal.ScorerTeamId == homeMatchTeam.TeamHattrickId
                        ? homeMatchTeam
                        : awayMatchTeam;

                    await this.ProcessSeniorMatchTeamGoalAsync(xmlGoal, goalSeniorTeam);
                }

                if (xmlMatch.MatchOfficials != null)
                {
                    // In some occassions, the Referee nodes come populated but with empty values
                    // so we check it before processing it.
                    if (xmlMatch.MatchOfficials.Referee.RefereeId != 0)
                    {
                        await this.ProcessSeniorMatchRefereeAsync(xmlMatch.MatchOfficials.Referee, seniorMatch);
                    }

                    if (xmlMatch.MatchOfficials.RefereeAssistant1.RefereeId != 0)
                    {
                        await this.ProcessSeniorMatchRefereeAsync(xmlMatch.MatchOfficials.RefereeAssistant1, seniorMatch);
                    }

                    if (xmlMatch.MatchOfficials.RefereeAssistant2.RefereeId != 0)
                    {
                        await this.ProcessSeniorMatchRefereeAsync(xmlMatch.MatchOfficials.RefereeAssistant2, seniorMatch);
                    }
                }
            }
        }

        private async Task<Domain.Senior.Match> ProcessFinishedSeniorMatchAsync(
            Models.MatchDetails.Match xmlMatch,
            MatchSystem matchSystem,
            Domain.Senior.Team seniorTeam,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(xmlMatch.AddedMinutes, nameof(xmlMatch.AddedMinutes));

            var match = await this.seniorMatchRepository.GetByHattrickIdAsync(xmlMatch.MatchId);

            if (match == null)
            {
                match = await this.seniorMatchRepository.InsertAsync(
                    new Domain.Senior.Match
                    {
                        HattrickId = xmlMatch.MatchId,
                        Date = xmlMatch.MatchDate,
                        System = matchSystem,
                        Type = (MatchType)xmlMatch.MatchType,
                        Rules = (MatchRule)xmlMatch.MatchRuleId,
                        AddedMinutes = xmlMatch.AddedMinutes.Value,
                        Team = seniorTeam
                    });
            }

            return match;
        }

        private async Task ProcessJuniorMatchArenaAsync(Models.MatchDetails.Arena xmlArena, Domain.Junior.Match juniorMatch)
        {
            ArgumentNullException.ThrowIfNull(xmlArena.WeatherId, nameof(xmlArena.WeatherId));
            ArgumentNullException.ThrowIfNull(xmlArena.SoldTerraces, nameof(xmlArena.SoldTerraces));
            ArgumentNullException.ThrowIfNull(xmlArena.SoldBasic, nameof(xmlArena.SoldBasic));
            ArgumentNullException.ThrowIfNull(xmlArena.SoldRoof, nameof(xmlArena.SoldRoof));
            ArgumentNullException.ThrowIfNull(xmlArena.SoldVip, nameof(xmlArena.SoldVip));
            ArgumentNullException.ThrowIfNull(xmlArena.SoldTotal, nameof(xmlArena.SoldTotal));

            var arena = await this.juniorMatchArenaRepository.Query(x => x.ArenaHattrickId == xmlArena.ArenaId
                                                                      && x.MatchHattrickId == juniorMatch.HattrickId)
                .SingleOrDefaultAsync();

            if (arena == null)
            {
                await this.juniorMatchArenaRepository.InsertAsync(
                    new Domain.Junior.MatchArena
                    {
                        ArenaHattrickId = xmlArena.ArenaId,
                        MatchHattrickId = juniorMatch.HattrickId,
                        Name = xmlArena.ArenaName,
                        Weather = (Weather)xmlArena.WeatherId.Value,
                        AttendanceTerraces = xmlArena.SoldTerraces.Value,
                        AttendanceBasic = xmlArena.SoldBasic.Value,
                        AttendanceRoof = xmlArena.SoldRoof.Value,
                        AttendanceVip = xmlArena.SoldVip.Value,
                        AttendanceTotal = xmlArena.SoldTotal.Value,
                        Match = juniorMatch
                    });
            }
        }

        private async Task ProcessJuniorMatchEventAsync(
            Models.MatchDetails.Event xmlEvent,
            Domain.Junior.Match juniorMatch)
        {
            var matchEvent = await this.juniorMatchEventRepository.Query(x => x.MatchHattrickId == juniorMatch.HattrickId
                                                                           && x.Index == xmlEvent.Index)
                .SingleOrDefaultAsync();

            if (matchEvent == null)
            {
                await this.juniorMatchEventRepository.InsertAsync(
                    new Domain.Junior.MatchEvent
                    {
                        Index = xmlEvent.Index,
                        MatchHattrickId = juniorMatch.HattrickId,
                        Minute = xmlEvent.Minute,
                        MatchPart = (MatchPart)xmlEvent.MatchPart,
                        Type = xmlEvent.EventTypeId,
                        Variation = xmlEvent.EventVariation,
                        SubjectTeamHattrickId = xmlEvent.SubjectTeamId != 0 ? xmlEvent.SubjectTeamId : null,
                        SubjectPlayerHattrickId = xmlEvent.SubjectPlayerId != 0 ? xmlEvent.SubjectPlayerId : null,
                        ObjectPlayerHattrickId = xmlEvent.ObjectPlayerId != 0 ? xmlEvent.ObjectPlayerId : null,
                        Text = string.IsNullOrWhiteSpace(xmlEvent.EventText) ? null : xmlEvent.EventText,
                        Match = juniorMatch
                    });
            }
            else
            {
                matchEvent.Text = string.IsNullOrWhiteSpace(xmlEvent.EventText) ? null : xmlEvent.EventText;

                this.juniorMatchEventRepository.Update(matchEvent);
            }
        }

        private async Task<Domain.Junior.MatchTeam> ProcessJuniorMatchTeamAsync(
            Models.MatchDetails.Team xmlTeam,
            MatchTeamLocation teamLocation,
            int firstHalfPossession,
            int secondHalfPossession,
            Domain.Junior.Match juniorMatch)
        {
            ArgumentNullException.ThrowIfNull(xmlTeam.Formation, nameof(xmlTeam.Formation));
            ArgumentNullException.ThrowIfNull(xmlTeam.TacticType, nameof(xmlTeam.TacticType));
            ArgumentNullException.ThrowIfNull(xmlTeam.TacticSkill, nameof(xmlTeam.TacticSkill));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingMidfield, nameof(xmlTeam.RatingMidfield));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingLeftDef, nameof(xmlTeam.RatingLeftDef));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingMidDef, nameof(xmlTeam.RatingMidDef));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingRightDef, nameof(xmlTeam.RatingRightDef));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingLeftAtt, nameof(xmlTeam.RatingLeftAtt));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingMidAtt, nameof(xmlTeam.RatingMidAtt));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingRightAtt, nameof(xmlTeam.RatingRightAtt));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingSetPiecesDef, nameof(xmlTeam.RatingSetPiecesDef));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingSetPiecesAtt, nameof(xmlTeam.RatingSetPiecesAtt));
            ArgumentNullException.ThrowIfNull(xmlTeam.NrOfChancesLeft, nameof(xmlTeam.NrOfChancesLeft));
            ArgumentNullException.ThrowIfNull(xmlTeam.NrOfChancesCenter, nameof(xmlTeam.NrOfChancesCenter));
            ArgumentNullException.ThrowIfNull(xmlTeam.NrOfChancesRight, nameof(xmlTeam.NrOfChancesRight));
            ArgumentNullException.ThrowIfNull(xmlTeam.NrOfChancesSpecialEvents, nameof(xmlTeam.NrOfChancesSpecialEvents));
            ArgumentNullException.ThrowIfNull(xmlTeam.NrOfChancesOther, nameof(xmlTeam.NrOfChancesOther));

            var matchTeam = await this.juniorMatchTeamRepository.Query(x => x.TeamHattrickId == xmlTeam.TeamId
                                                                         && x.MatchHattrickId == juniorMatch.HattrickId)
                .SingleOrDefaultAsync();

            if (matchTeam == null)
            {
                matchTeam = await this.juniorMatchTeamRepository.InsertAsync(
                    new Domain.Junior.MatchTeam
                    {
                        TeamHattrickId = xmlTeam.TeamId,
                        MatchHattrickId = juniorMatch.HattrickId,
                        Name = xmlTeam.TeamName,
                        Location = teamLocation,
                        Formation = xmlTeam.Formation,
                        TacticType = (MatchTacticType)xmlTeam.TacticType,
                        TacticSkill = (SkillLevel)xmlTeam.TacticSkill,
                        FirstHalfPossession = firstHalfPossession,
                        SecondHalfPossession = secondHalfPossession,
                        MidfieldRating = (MatchSectorRating)xmlTeam.RatingMidfield,
                        LeftDefenseRating = (MatchSectorRating)xmlTeam.RatingLeftDef,
                        CenterDefenseRating = (MatchSectorRating)xmlTeam.RatingMidDef,
                        RightDefenseRating = (MatchSectorRating)xmlTeam.RatingRightDef,
                        LeftAttackRating = (MatchSectorRating)xmlTeam.RatingLeftAtt,
                        CenterAttackRating = (MatchSectorRating)xmlTeam.RatingMidAtt,
                        RightAttackRating = (MatchSectorRating)xmlTeam.RatingRightAtt,
                        DefenseIndirectSetPiecesRating = (MatchSectorRating)xmlTeam.RatingSetPiecesDef,
                        AttackIndirectSetPiecesRating = (MatchSectorRating)xmlTeam.RatingSetPiecesAtt,
                        ChancesOnLeft = xmlTeam.NrOfChancesLeft.Value,
                        ChancesOnCenter = xmlTeam.NrOfChancesCenter.Value,
                        ChancesOnRight = xmlTeam.NrOfChancesRight.Value,
                        SpecialEventChances = xmlTeam.NrOfChancesSpecialEvents.Value,
                        OtherChances = xmlTeam.NrOfChancesOther.Value,
                        Match = juniorMatch
                    });
            }

            return matchTeam;
        }

        private async Task ProcessJuniorMatchTeamBookingAsync(
            Models.MatchDetails.Booking xmlBooking,
            Domain.Junior.MatchTeam juniorMatchTeam)
        {
            var juniorTeamBooking = await this.juniorMatchTeamBookingRepository.Query(x => x.TeamHattrickId == juniorMatchTeam.TeamHattrickId
                                                                                        && x.MatchHattrickId == juniorMatchTeam.MatchHattrickId
                                                                                        && x.Index == xmlBooking.Index)
                .SingleOrDefaultAsync();

            if (juniorTeamBooking == null)
            {
                await this.juniorMatchTeamBookingRepository.InsertAsync(
                    new Domain.Junior.MatchTeamBooking
                    {
                        Index = xmlBooking.Index,
                        TeamHattrickId = juniorMatchTeam.TeamHattrickId,
                        MatchHattrickId = juniorMatchTeam.TeamHattrickId,
                        Minute = xmlBooking.BookingMinute,
                        MatchPart = (MatchPart)xmlBooking.MatchPart,
                        PlayerHattrickId = xmlBooking.BookingPlayerId,
                        PlayerName = xmlBooking.BookingPlayerName,
                        Type = (BookingType)xmlBooking.BookingType,
                        MatchTeam = juniorMatchTeam
                    });
            }
        }

        private async Task ProcessJuniorMatchTeamGoalAsync(
            Models.MatchDetails.Goal xmlGoal,
            Domain.Junior.MatchTeam juniorMatchTeam)
        {
            var juniorTeamGoal = await this.juniorMatchTeamGoalRepository.Query(x => x.TeamHattrickId == juniorMatchTeam.TeamHattrickId
                                                                                  && x.MatchHattrickId == juniorMatchTeam.MatchHattrickId
                                                                                  && x.Index == xmlGoal.Index)
                .SingleOrDefaultAsync();

            if (juniorTeamGoal == null)
            {
                await this.juniorMatchTeamGoalRepository.InsertAsync(
                    new Domain.Junior.MatchTeamGoal
                    {
                        Index = xmlGoal.Index,
                        TeamHattrickId = juniorMatchTeam.TeamHattrickId,
                        MatchHattrickId = juniorMatchTeam.MatchHattrickId,
                        Minute = xmlGoal.ScorerMinute,
                        MatchPart = (MatchPart)xmlGoal.MatchPart,
                        PlayerHattrickId = xmlGoal.ScorerPlayerId,
                        PlayerName = xmlGoal.ScorerPlayerName,
                        HomeTeamScore = xmlGoal.ScorerHomeGoals,
                        AwayTeamScore = xmlGoal.ScorerAwayGoals,
                        MatchTeam = juniorMatchTeam
                    });
            }
        }

        private async Task ProcessJuniorMatchTeamInjuryAsync(
            Models.MatchDetails.Injury xmlInjury,
            Domain.Junior.MatchTeam juniorMatchTeam)
        {
            var juniorTeamInjury = await this.juniorMatchTeamInjuryRepository.Query(x => x.TeamHattrickId == juniorMatchTeam.TeamHattrickId
                                                                                      && x.MatchHattrickId == juniorMatchTeam.MatchHattrickId
                                                                                      && x.Index == xmlInjury.Index)
                .SingleOrDefaultAsync();

            if (juniorTeamInjury == null)
            {
                await this.juniorMatchTeamInjuryRepository.InsertAsync(
                    new Domain.Junior.MatchTeamInjury
                    {
                        TeamHattrickId = juniorMatchTeam.TeamHattrickId,
                        MatchHattrickId = juniorMatchTeam.MatchHattrickId,
                        Index = xmlInjury.Index,
                        Minute = xmlInjury.InjuryMinute,
                        MatchPart = (MatchPart)xmlInjury.MatchPart,
                        PlayerHattrickId = xmlInjury.InjuryPlayerId,
                        PlayerName = xmlInjury.InjuryPlayerName,
                        Type = (InjuryType)xmlInjury.InjuryType,
                        MatchTeam = juniorMatchTeam
                    });
            }
        }

        private async Task ProcessJuniorUpcomingMatchAsync(
            Models.MatchDetails.Match xmlMatch,
            MatchSystem matchSystem,
            Domain.Junior.Team juniorTeam,
            CancellationToken cancellationToken)
        {
            var match = await this.juniorUpcomingMatchRepository.GetByHattrickIdAsync(xmlMatch.MatchId);

            if (match == null)
            {
                match = await this.juniorUpcomingMatchRepository.InsertAsync(
                    new Domain.Junior.UpcomingMatch
                    {
                        HattrickId = xmlMatch.MatchId,
                        TeamHattrickId = juniorTeam.HattrickId,
                        Date = xmlMatch.MatchDate,
                        System = matchSystem,
                        Type = (MatchType)xmlMatch.MatchType,
                        ContextId = xmlMatch.MatchContextId,
                        HomeTeamHattrickId = xmlMatch.HomeTeam.TeamId,
                        HomeTeamName = xmlMatch.HomeTeam.TeamName,
                        AwayTeamHattrickId = xmlMatch.AwayTeam.TeamId,
                        AwayTeamName = xmlMatch.AwayTeam.TeamName,
                        Team = juniorTeam
                    });
            }
        }

        private async Task ProcessSeniorMatchArenaAsync(Models.MatchDetails.Arena xmlArena, Domain.Senior.Match seniorMatch)
        {
            ArgumentNullException.ThrowIfNull(xmlArena.WeatherId, nameof(xmlArena.WeatherId));
            ArgumentNullException.ThrowIfNull(xmlArena.SoldTerraces, nameof(xmlArena.SoldTerraces));
            ArgumentNullException.ThrowIfNull(xmlArena.SoldBasic, nameof(xmlArena.SoldBasic));
            ArgumentNullException.ThrowIfNull(xmlArena.SoldRoof, nameof(xmlArena.SoldRoof));
            ArgumentNullException.ThrowIfNull(xmlArena.SoldVip, nameof(xmlArena.SoldVip));
            ArgumentNullException.ThrowIfNull(xmlArena.SoldTotal, nameof(xmlArena.SoldTotal));

            var arena = await this.seniorMatchArenaRepository.Query(x => x.ArenaHattrickId == xmlArena.ArenaId
                                                                      && x.MatchHattrickId == seniorMatch.HattrickId)
                .SingleOrDefaultAsync();

            if (arena == null)
            {
                await this.seniorMatchArenaRepository.InsertAsync(
                    new Domain.Senior.MatchArena
                    {
                        ArenaHattrickId = xmlArena.ArenaId,
                        MatchHattrickId = seniorMatch.HattrickId,
                        Name = xmlArena.ArenaName,
                        Weather = (Weather)xmlArena.WeatherId.Value,
                        AttendanceTerraces = xmlArena.SoldTerraces.Value,
                        AttendanceBasic = xmlArena.SoldBasic.Value,
                        AttendanceRoof = xmlArena.SoldRoof.Value,
                        AttendanceVip = xmlArena.SoldVip.Value,
                        AttendanceTotal = xmlArena.SoldTotal.Value,
                        Match = seniorMatch
                    });
            }
        }

        private async Task ProcessSeniorMatchEventAsync(
            Models.MatchDetails.Event xmlEvent,
            Domain.Senior.Match seniorMatch)
        {
            var matchEvent = await this.seniorMatchEventRepository.Query(x => x.MatchHattrickId == seniorMatch.HattrickId
                                                                           && x.Index == xmlEvent.Index)
                .SingleOrDefaultAsync();

            if (matchEvent == null)
            {
                await this.seniorMatchEventRepository.InsertAsync(
                    new Domain.Senior.MatchEvent
                    {
                        Index = xmlEvent.Index,
                        MatchHattrickId = seniorMatch.HattrickId,
                        Minute = xmlEvent.Minute,
                        MatchPart = (MatchPart)xmlEvent.MatchPart,
                        Type = xmlEvent.EventTypeId,
                        Variation = xmlEvent.EventVariation,
                        SubjectTeamHattrickId = xmlEvent.SubjectTeamId != 0 ? xmlEvent.SubjectTeamId : null,
                        SubjectPlayerHattrickId = xmlEvent.SubjectPlayerId != 0 ? xmlEvent.SubjectPlayerId : null,
                        ObjectPlayerHattrickId = xmlEvent.ObjectPlayerId != 0 ? xmlEvent.ObjectPlayerId : null,
                        Text = string.IsNullOrWhiteSpace(xmlEvent.EventText) ? null : xmlEvent.EventText,
                        Match = seniorMatch
                    });
            }
            else
            {
                matchEvent.Text = string.IsNullOrWhiteSpace(xmlEvent.EventText) ? null : xmlEvent.EventText;

                this.seniorMatchEventRepository.Update(matchEvent);
            }
        }

        private async Task ProcessSeniorMatchRefereeAsync(Models.MatchDetails.Referee xmlReferee, Domain.Senior.Match seniorMatch)
        {
            var referee = await this.seniorMatchRefereeRepository.Query(x => x.RefereeHattrickId == xmlReferee.RefereeId
                                                                          && x.MatchHattrickId == seniorMatch.HattrickId)
                .SingleOrDefaultAsync();

            if (referee == null)
            {
                // The property is named Country since it's called that in the documentation, but it is actually the League ID. Thanks Hattrick!.
                var league = await this.leagueRepository.GetByHattrickIdAsync(xmlReferee.RefereeCountryId);

                ArgumentNullException.ThrowIfNull(league, nameof(league));
                ArgumentNullException.ThrowIfNull(league.Country, nameof(league.Country));

                await this.seniorMatchRefereeRepository.InsertAsync(
                    new Domain.Senior.MatchReferee
                    {
                        RefereeHattrickId = xmlReferee.RefereeId,
                        MatchHattrickId = seniorMatch.HattrickId,
                        Name = xmlReferee.RefereeName,
                        Country = league.Country,
                        Match = seniorMatch
                    });
            }
        }

        private async Task<Domain.Senior.MatchTeam> ProcessSeniorMatchTeamAsync(
            Models.MatchDetails.Team xmlTeam,
            MatchTeamLocation teamLocation,
            int firstHalfPossession,
            int secondHalfPossession,
            Domain.Senior.Match seniorMatch,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(xmlTeam.DressUri, nameof(xmlTeam.DressUri));
            ArgumentNullException.ThrowIfNull(xmlTeam.Formation, nameof(xmlTeam.Formation));
            ArgumentNullException.ThrowIfNull(xmlTeam.TacticType, nameof(xmlTeam.TacticType));
            ArgumentNullException.ThrowIfNull(xmlTeam.TacticSkill, nameof(xmlTeam.TacticSkill));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingMidfield, nameof(xmlTeam.RatingMidfield));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingLeftDef, nameof(xmlTeam.RatingLeftDef));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingMidDef, nameof(xmlTeam.RatingMidDef));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingRightDef, nameof(xmlTeam.RatingRightDef));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingLeftAtt, nameof(xmlTeam.RatingLeftAtt));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingMidAtt, nameof(xmlTeam.RatingMidAtt));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingRightAtt, nameof(xmlTeam.RatingRightAtt));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingSetPiecesDef, nameof(xmlTeam.RatingSetPiecesDef));
            ArgumentNullException.ThrowIfNull(xmlTeam.RatingSetPiecesAtt, nameof(xmlTeam.RatingSetPiecesAtt));
            ArgumentNullException.ThrowIfNull(xmlTeam.NrOfChancesLeft, nameof(xmlTeam.NrOfChancesLeft));
            ArgumentNullException.ThrowIfNull(xmlTeam.NrOfChancesCenter, nameof(xmlTeam.NrOfChancesCenter));
            ArgumentNullException.ThrowIfNull(xmlTeam.NrOfChancesRight, nameof(xmlTeam.NrOfChancesRight));
            ArgumentNullException.ThrowIfNull(xmlTeam.NrOfChancesSpecialEvents, nameof(xmlTeam.NrOfChancesSpecialEvents));
            ArgumentNullException.ThrowIfNull(xmlTeam.NrOfChancesOther, nameof(xmlTeam.NrOfChancesOther));

            var matchTeam = await this.seniorMatchTeamRepository.Query(x => x.TeamHattrickId == xmlTeam.TeamId
                                                                         && x.MatchHattrickId == seniorMatch.HattrickId)
                .SingleOrDefaultAsync();

            if (matchTeam == null)
            {
                var matchKitBytes = await GetImageBytesFromCacheAsync(xmlTeam.DressUri, cancellationToken);

                ArgumentNullException.ThrowIfNull(matchKitBytes, nameof(matchKitBytes));

                matchTeam = await this.seniorMatchTeamRepository.InsertAsync(
                    new Domain.Senior.MatchTeam
                    {
                        TeamHattrickId = xmlTeam.TeamId,
                        MatchHattrickId = seniorMatch.HattrickId,
                        Name = xmlTeam.TeamName,
                        Location = teamLocation,
                        Formation = xmlTeam.Formation,
                        TacticType = (MatchTacticType)xmlTeam.TacticType,
                        TacticSkill = (SkillLevel)xmlTeam.TacticSkill,
                        FirstHalfPossession = firstHalfPossession,
                        SecondHalfPossession = secondHalfPossession,
                        MidfieldRating = (MatchSectorRating)xmlTeam.RatingMidfield,
                        LeftDefenseRating = (MatchSectorRating)xmlTeam.RatingLeftDef,
                        CenterDefenseRating = (MatchSectorRating)xmlTeam.RatingMidDef,
                        RightDefenseRating = (MatchSectorRating)xmlTeam.RatingRightDef,
                        LeftAttackRating = (MatchSectorRating)xmlTeam.RatingLeftAtt,
                        CenterAttackRating = (MatchSectorRating)xmlTeam.RatingMidAtt,
                        RightAttackRating = (MatchSectorRating)xmlTeam.RatingRightAtt,
                        DefenseIndirectSetPiecesRating = (MatchSectorRating)xmlTeam.RatingSetPiecesDef,
                        AttackIndirectSetPiecesRating = (MatchSectorRating)xmlTeam.RatingSetPiecesAtt,
                        Attitude = xmlTeam.TeamAttitude != null ? (MatchTeamAttitude)xmlTeam.TeamAttitude : null,
                        ChancesOnLeft = xmlTeam.NrOfChancesLeft.Value,
                        ChancesOnCenter = xmlTeam.NrOfChancesCenter.Value,
                        ChancesOnRight = xmlTeam.NrOfChancesRight.Value,
                        SpecialEventChances = xmlTeam.NrOfChancesSpecialEvents.Value,
                        OtherChances = xmlTeam.NrOfChancesOther.Value,
                        MatchKitBytes = matchKitBytes,
                        Match = seniorMatch
                    });
            }

            return matchTeam;
        }

        private async Task ProcessSeniorMatchTeamBookingAsync(
            Models.MatchDetails.Booking xmlBooking,
            Domain.Senior.MatchTeam seniorMatchTeam)
        {
            var seniorTeamBooking = await this.seniorMatchTeamBookingRepository.Query(x => x.TeamHattrickId == seniorMatchTeam.TeamHattrickId
                                                                                        && x.MatchHattrickId == seniorMatchTeam.MatchHattrickId
                                                                                        && x.Index == xmlBooking.Index)
                .SingleOrDefaultAsync();

            if (seniorTeamBooking == null)
            {
                await this.seniorMatchTeamBookingRepository.InsertAsync(
                    new Domain.Senior.MatchTeamBooking
                    {
                        Index = xmlBooking.Index,
                        TeamHattrickId = seniorMatchTeam.TeamHattrickId,
                        MatchHattrickId = seniorMatchTeam.TeamHattrickId,
                        Minute = xmlBooking.BookingMinute,
                        MatchPart = (MatchPart)xmlBooking.MatchPart,
                        PlayerHattrickId = xmlBooking.BookingPlayerId,
                        PlayerName = xmlBooking.BookingPlayerName,
                        Type = (BookingType)xmlBooking.BookingType,
                        MatchTeam = seniorMatchTeam
                    });
            }
        }

        private async Task ProcessSeniorMatchTeamGoalAsync(
            Models.MatchDetails.Goal xmlGoal,
            Domain.Senior.MatchTeam seniorMatchTeam)
        {
            var seniorTeamGoal = await this.seniorMatchTeamGoalRepository.Query(x => x.TeamHattrickId == seniorMatchTeam.TeamHattrickId
                                                                                  && x.MatchHattrickId == seniorMatchTeam.MatchHattrickId
                                                                                  && x.Index == xmlGoal.Index)
                .SingleOrDefaultAsync();

            if (seniorTeamGoal == null)
            {
                await this.seniorMatchTeamGoalRepository.InsertAsync(
                    new Domain.Senior.MatchTeamGoal
                    {
                        Index = xmlGoal.Index,
                        TeamHattrickId = seniorMatchTeam.TeamHattrickId,
                        MatchHattrickId = seniorMatchTeam.MatchHattrickId,
                        Minute = xmlGoal.ScorerMinute,
                        MatchPart = (MatchPart)xmlGoal.MatchPart,
                        PlayerHattrickId = xmlGoal.ScorerPlayerId,
                        PlayerName = xmlGoal.ScorerPlayerName,
                        HomeTeamScore = xmlGoal.ScorerHomeGoals,
                        AwayTeamScore = xmlGoal.ScorerAwayGoals,
                        MatchTeam = seniorMatchTeam
                    });
            }
        }

        private async Task ProcessSeniorMatchTeamInjuryAsync(
            Models.MatchDetails.Injury xmlInjury,
            Domain.Senior.MatchTeam seniorMatchTeam)
        {
            var seniorTeamInjury = await this.seniorMatchTeamInjuryRepository.Query(x => x.TeamHattrickId == seniorMatchTeam.TeamHattrickId
                                                                                      && x.MatchHattrickId == seniorMatchTeam.MatchHattrickId
                                                                                      && x.Index == xmlInjury.Index)
                .SingleOrDefaultAsync();

            if (seniorTeamInjury == null)
            {
                await this.seniorMatchTeamInjuryRepository.InsertAsync(
                    new Domain.Senior.MatchTeamInjury
                    {
                        TeamHattrickId = seniorMatchTeam.TeamHattrickId,
                        MatchHattrickId = seniorMatchTeam.MatchHattrickId,
                        Index = xmlInjury.Index,
                        Minute = xmlInjury.InjuryMinute,
                        MatchPart = (MatchPart)xmlInjury.MatchPart,
                        PlayerHattrickId = xmlInjury.InjuryPlayerId,
                        PlayerName = xmlInjury.InjuryPlayerName,
                        Type = (InjuryType)xmlInjury.InjuryType,
                        MatchTeam = seniorMatchTeam
                    });
            }
        }

        private async Task ProcessSeniorUpcomingMatchAsync(
            Models.MatchDetails.Match xmlMatch,
            MatchSystem matchSystem,
            Domain.Senior.Team seniorTeam,
            CancellationToken cancellationToken)
        {
            var match = await this.seniorUpcomingMatchRepository.GetByHattrickIdAsync(xmlMatch.MatchId);

            if (match == null)
            {
                match = await this.seniorUpcomingMatchRepository.InsertAsync(
                    new Domain.Senior.UpcomingMatch
                    {
                        HattrickId = xmlMatch.MatchId,
                        TeamHattrickId = seniorTeam.HattrickId,
                        Date = xmlMatch.MatchDate,
                        System = matchSystem,
                        Type = (MatchType)xmlMatch.MatchType,
                        ContextId = xmlMatch.MatchContextId,
                        HomeTeamHattrickId = xmlMatch.HomeTeam.TeamId,
                        HomeTeamName = xmlMatch.HomeTeam.TeamName,
                        AwayTeamHattrickId = xmlMatch.AwayTeam.TeamId,
                        AwayTeamName = xmlMatch.AwayTeam.TeamName,
                        Team = seniorTeam
                    });
            }
        }

        private async Task ProcessUpcomingMatchFileAsync(
            Models.MatchDetails.Match xmlMatch,
            MatchSystem matchSystem,
            long teamId,
            CancellationToken cancellationToken)
        {
            if (matchSystem == MatchSystem.Youth)
            {
                var juniorTeam = await this.juniorTeamRepository.GetByHattrickIdAsync(teamId);

                ArgumentNullException.ThrowIfNull(juniorTeam, nameof(juniorTeam));

                await this.ProcessJuniorUpcomingMatchAsync(
                    xmlMatch,
                    matchSystem,
                    juniorTeam,
                    cancellationToken);
            }
            else
            {
                var seniorTeam = await this.seniorTeamRepository.GetByHattrickIdAsync(teamId);

                ArgumentNullException.ThrowIfNull(seniorTeam, nameof(seniorTeam));

                await this.ProcessSeniorUpcomingMatchAsync(
                    xmlMatch,
                    matchSystem,
                    seniorTeam,
                    cancellationToken);
            }
        }
    }
}