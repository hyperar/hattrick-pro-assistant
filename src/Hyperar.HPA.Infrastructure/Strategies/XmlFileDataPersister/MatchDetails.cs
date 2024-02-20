namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Hattrick = Application.Hattrick.MatchDetails;

    public class MatchDetails : XmlFileDataPersisterBase, IXmlFileDataPersisterStrategy
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

        public override Task PersistDataAsync(IXmlFile file)
        {
            throw new NotSupportedException();
        }

        public override async Task PersistDataWithContextAsync(IXmlFile file, uint contextId)
        {
            if (file is Hattrick.HattrickData xmlEntity)
            {
                var team = await this.teamRepository.GetByHattrickIdAsync(contextId);

                ArgumentNullException.ThrowIfNull(team, nameof(team));

                await this.ProcessMatchAsync(xmlEntity.Match, xmlEntity.SourceSystem, team);
            }
            else
            {
                throw new ArgumentException(file.GetType().FullName, nameof(file));
            }
        }

        private async Task ProcessArenaAsync(Hattrick.Arena xmlArena, Domain.Senior.Match match)
        {
            if (xmlArena.ArenaId == 0)
            {
                return;
            }

            var arena = await this.matchArenaRepository.Query(x => x.MatchHattrickId == match.HattrickId)
                                                       .SingleOrDefaultAsync();

            if (arena == null)
            {
                await this.matchArenaRepository.InsertAsync(
                    new Domain.Senior.MatchArena
                    {
                        HattrickId = xmlArena.ArenaId,
                        Name = xmlArena.ArenaName,
                        Attendance = xmlArena.SoldTotal,
                        TerracesSold = xmlArena.SoldTerraces,
                        BasicSeatsSold = xmlArena.SoldBasic,
                        RoofSeatsSold = xmlArena.SoldRoof,
                        VipSeatsSold = xmlArena.SoldVip,
                        Match = match
                    });
            }
        }

        private async Task ProcessEventAsync(Hattrick.Event xmlEvent, Domain.Senior.Match match)
        {
            var dbEvent = await this.matchEventRepository.Query(x => x.Index == xmlEvent.Index
                                                                  && x.Match.HattrickId == match.HattrickId)
                                                         .SingleOrDefaultAsync();

            if (dbEvent == null)
            {
                await this.matchEventRepository.InsertAsync(
                    new Domain.Senior.MatchEvent
                    {
                        Index = xmlEvent.Index,
                        PlayerHattrickId = xmlEvent.SubjectPlayerId == 0 ? null : xmlEvent.SubjectPlayerId,
                        AdditionalPlayerHattrickId = xmlEvent.ObjectPlayerId == 0 ? null : xmlEvent.ObjectPlayerId,
                        TeamHattrickId = xmlEvent.SubjectTeamId == 0 ? null : xmlEvent.SubjectTeamId,
                        Type = xmlEvent.EventTypeId,
                        Variation = xmlEvent.EventVariation,
                        Text = string.IsNullOrWhiteSpace(xmlEvent.EventText) ? null : RemoveTags(xmlEvent.EventText),
                        Minute = xmlEvent.Minute,
                        MatchPart = xmlEvent.MatchPart,
                        Match = match
                    });
            }
        }

        private async Task ProcessMatchAsync(Hattrick.Match xmlMatch, string sourceSystem, Domain.Senior.Team team)
        {
            var match = await this.matchRepository.GetByHattrickIdAsync(xmlMatch.MatchId);

            match ??= await this.matchRepository.InsertAsync(new Domain.Senior.Match
            {
                HattrickId = xmlMatch.MatchId,
                SourceSystem = sourceSystem,
                Type = xmlMatch.MatchType,
                CompetitionId = xmlMatch.MatchContextId != 0 ? xmlMatch.MatchContextId : null,
                Rules = xmlMatch.MatchRuleId,
                HomeTeamHattrickId = xmlMatch.HomeTeam.TeamId,
                AwayTeamHattrickId = xmlMatch.AwayTeam.TeamId,
                StartDate = xmlMatch.MatchDate,
                FinishDate = xmlMatch.FinishedDate,
                AddedMinutes = xmlMatch.AddedMinutes,
                Weather = xmlMatch.Arena.WeatherId,
                Team = team
            });

            await this.ProcessArenaAsync(xmlMatch.Arena, match);

            await this.ProcessMatchTeamAsync(xmlMatch.HomeTeam, xmlMatch, match, true);

            await this.ProcessMatchTeamAsync(xmlMatch.AwayTeam, xmlMatch, match, false);

            if (xmlMatch.MatchOfficials != null)
            {
                await this.ProcessMatchOfficialAsync(xmlMatch.MatchOfficials.Referee, match);
                await this.ProcessMatchOfficialAsync(xmlMatch.MatchOfficials.RefereeAssistant1, match);
                await this.ProcessMatchOfficialAsync(xmlMatch.MatchOfficials.RefereeAssistant2, match);
            }

            if (xmlMatch.EventList != null)
            {
                foreach (var xmlEvent in xmlMatch.EventList)
                {
                    await this.ProcessEventAsync(xmlEvent, match);
                }
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessMatchOfficialAsync(Hattrick.Referee xmlReferee, Domain.Senior.Match match)
        {
            var referee = await this.matchOfficialRepository.Query(x => x.HattrickId == xmlReferee.RefereeId
                                                                     && x.Match.HattrickId == match.HattrickId)
                                                            .SingleOrDefaultAsync();

            if (referee == null)
            {
                var league = await this.leagueRepository.GetByHattrickIdAsync(xmlReferee.RefereeCountryId);

                ArgumentNullException.ThrowIfNull(league, nameof(league));
                ArgumentNullException.ThrowIfNull(league.Country, nameof(league.Country));

                referee = await this.matchOfficialRepository.InsertAsync(
                    new Domain.Senior.MatchOfficial
                    {
                        HattrickId = xmlReferee.RefereeId,
                        Name = xmlReferee.RefereeName,
                        Match = match,
                        Country = league.Country
                    });
            }
        }

        private async Task ProcessMatchTeamAsync(
            Hattrick.Team xmlTeam,
            Hattrick.Match xmlMatch,
            Domain.Senior.Match match,
            bool isHomeTeam)
        {
            var team = await this.matchTeamRepository.Query(x => x.HattrickId == xmlTeam.TeamId
                                                              && x.Match.HattrickId == match.HattrickId)
                                                     .SingleOrDefaultAsync();

            if (team == null)
            {
                team = await this.matchTeamRepository.InsertAsync(
                    new Domain.Senior.MatchTeam
                    {
                        HattrickId = xmlTeam.TeamId,
                        Name = xmlTeam.TeamName,
                        MatchKitUrl = !string.IsNullOrWhiteSpace(xmlTeam.DressUri) ? NormalizeUrl(xmlTeam.DressUri) : null,
                        MatchKitBytes = !string.IsNullOrWhiteSpace(xmlTeam.DressUri) ? await DownloadWebResourceAsync(xmlTeam.DressUri) : null,
                        Formation = xmlTeam.Formation,
                        Score = xmlTeam.Goals,
                        TacticType = xmlTeam.TacticType,
                        TacticLevel = xmlTeam.TacticSkill,
                        MidfieldRating = xmlTeam.RatingMidfield,
                        RightDefenseRating = xmlTeam.RatingRightDef,
                        CentralDefenseRating = xmlTeam.RatingMidDef,
                        LeftDefenseRating = xmlTeam.RatingLeftDef,
                        RightAttackRating = xmlTeam.RatingRightAtt,
                        CentralAttackRating = xmlTeam.RatingMidAtt,
                        LeftAttackRating = xmlTeam.RatingLeftAtt,
                        DefenseIndirectSetPiecesRating = xmlTeam.RatingSetPiecesDef,
                        AttackIndirectSetPiecesRating = xmlTeam.RatingSetPiecesAtt,
                        Attitude = xmlTeam.TeamAttitude,
                        ChancesOnRight = xmlTeam.NrOfChancesRight,
                        ChancesOnCenter = xmlTeam.NrOfChancesCenter,
                        ChancesOnLeft = xmlTeam.NrOfChancesLeft,
                        SpecialEventChances = xmlTeam.NrOfChancesSpecialEvents,
                        OtherChances = xmlTeam.NrOfChancesOther,
                        FirstHalfPosession = isHomeTeam ? xmlMatch.PossessionFirstHalfHome : xmlMatch.PossessionFirstHalfAway,
                        SecondHalfPosession = isHomeTeam ? xmlMatch.PossessionSecondHalfHome : xmlMatch.PossessionSecondHalfAway,
                        Match = match
                    });

                await this.databaseContext.SaveAsync();

                if (xmlMatch.Scorers != null)
                {
                    foreach (var xmlGoal in xmlMatch.Scorers.Where(x => x.ScorerTeamId == xmlTeam.TeamId))
                    {
                        await this.ProcessMatchTeamGoalsAsync(xmlGoal, team);
                    }
                }

                if (xmlMatch.Bookings != null)
                {
                    foreach (var xmlBooking in xmlMatch.Bookings.Where(x => x.BookingTeamId == xmlTeam.TeamId))
                    {
                        await this.ProcessMatchTeamBookingAsync(xmlBooking, team);
                    }
                }

                if (xmlMatch.Injuries != null)
                {
                    foreach (var xmlInjury in xmlMatch.Injuries.Where(x => x.InjuryTeamId == xmlTeam.TeamId))
                    {
                        await this.ProcessMatchTeamInjuryAsync(xmlInjury, team);
                    }
                }
            }
        }

        private async Task ProcessMatchTeamBookingAsync(Hattrick.Booking xmlBooking, Domain.Senior.MatchTeam team)
        {
            var booking = await this.matchTeamBookingRepository.Query(x => x.Index == xmlBooking.Index
                                                                        && x.Team.Id == team.Id)
                                                               .SingleOrDefaultAsync();

            booking ??= await this.matchTeamBookingRepository.InsertAsync(
                    new Domain.Senior.MatchTeamBooking
                    {
                        Index = xmlBooking.Index,
                        PlayerHattrickId = xmlBooking.BookingPlayerId,
                        PlayerName = xmlBooking.BookingPlayerName,
                        Type = xmlBooking.BookingType,
                        Minute = xmlBooking.BookingMinute,
                        MatchPart = xmlBooking.MatchPart,
                        Team = team
                    });
        }

        private async Task ProcessMatchTeamGoalsAsync(Hattrick.Goal xmlGoal, Domain.Senior.MatchTeam team)
        {
            var goal = await this.matchTeamGoalRepository.Query(x => x.Index == xmlGoal.Index
                                                                  && x.Team.HattrickId == team.Match.HattrickId)
                                                         .SingleOrDefaultAsync();

            goal ??= await this.matchTeamGoalRepository.InsertAsync(
                    new Domain.Senior.MatchTeamGoal
                    {
                        Index = xmlGoal.Index,
                        PlayerHattrickId = xmlGoal.ScorerPlayerId,
                        PlayerName = xmlGoal.ScorerPlayerName,
                        HomeTeamScore = xmlGoal.ScorerHomeGoals,
                        AwayTeamScore = xmlGoal.ScorerAwayGoals,
                        Minute = xmlGoal.ScorerMinute,
                        MatchPart = xmlGoal.MatchPart,
                        Team = team
                    });
        }

        private async Task ProcessMatchTeamInjuryAsync(Hattrick.Injury xmlInjury, Domain.Senior.MatchTeam team)
        {
            var injury = await this.matchTeamInjuryRepository.Query(x => x.Index == xmlInjury.Index
                                                                      && x.Team.Id == team.Id)
                                                             .SingleOrDefaultAsync();

            injury ??= await this.matchTeamInjuryRepository.InsertAsync(
                    new Domain.Senior.MatchTeamInjury
                    {
                        Index = xmlInjury.Index,
                        PlayerHattrickId = xmlInjury.InjuryPlayerId,
                        PlayerName = xmlInjury.InjuryPlayerName,
                        Type = xmlInjury.InjuryType,
                        Minute = xmlInjury.InjuryMinute,
                        MatchPart = xmlInjury.MatchPart,
                        Team = team
                    });
        }
    }
}