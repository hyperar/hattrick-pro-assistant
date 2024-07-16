namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick;

    public class YouthPlayerDetails : DownloadTaskStrategyBase, IPersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IHattrickRepository<Domain.Junior.Player> juniorPlayerRepository;

        private readonly IHattrickRepository<Domain.Junior.Team> juniorTeamRepository;

        private readonly IAuditableRepository<Domain.Junior.PlayerSkillSet> junorPlayerSkillSetRepository;

        public YouthPlayerDetails(
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.Junior.Player> playerRepository,
            IAuditableRepository<Domain.Junior.PlayerSkillSet> playerSkillSetRepository,
            IHattrickRepository<Domain.Junior.Team> teamRepository)
        {
            this.countryRepository = countryRepository;
            this.juniorPlayerRepository = playerRepository;
            this.junorPlayerSkillSetRepository = playerSkillSetRepository;
            this.juniorTeamRepository = teamRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            if (task.XmlFile is Models.YouthPlayerDetails.HattrickData file)
            {
                ArgumentNullException.ThrowIfNull(file.YouthPlayer.OwningYouthTeam, nameof(file.YouthPlayer.OwningYouthTeam));

                var juniorTeam = await this.juniorTeamRepository.GetByHattrickIdAsync(file.YouthPlayer.OwningYouthTeam.YouthTeamId);

                ArgumentNullException.ThrowIfNull(juniorTeam, nameof(juniorTeam));

                var juniorPlayer = await this.ProcessPlayerAsync(file.YouthPlayer, juniorTeam);

                await this.ProcessPlayerSkillSet(
                    file.YouthPlayer.PlayerSkills,
                    juniorPlayer,
                    juniorTeam.SeniorTeam.League.Season,
                    juniorTeam.SeniorTeam.League.Week);
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }

        private async Task<Domain.Junior.Player> ProcessPlayerAsync(Models.YouthPlayerDetails.YouthPlayer xmlPlayer, Domain.Junior.Team team)
        {
            var juniorPlayer = await this.juniorPlayerRepository.GetByHattrickIdAsync(xmlPlayer.YouthPlayerId);

            var nextBirthDay = DateTime.Today.AddDays(112 - xmlPlayer.AgeDays);

            if (juniorPlayer == null)
            {
                var country = await this.countryRepository.GetByHattrickIdAsync(xmlPlayer.NativeCountryId);

                ArgumentNullException.ThrowIfNull(country, nameof(country));

                juniorPlayer = await this.juniorPlayerRepository.InsertAsync(
                    new Domain.Junior.Player
                    {
                        HattrickId = xmlPlayer.YouthPlayerId,
                        FirstName = xmlPlayer.FirstName,
                        NickName = xmlPlayer.NickName,
                        LastName = xmlPlayer.LastName,
                        AgeYears = xmlPlayer.Age,
                        AgeDays = xmlPlayer.AgeDays,
                        JoinedOn = xmlPlayer.ArrivalDate,
                        NextBirthDay = nextBirthDay,
                        ShirtNumber = xmlPlayer.PlayerNumber,
                        Category = (PlayerCategory)xmlPlayer.PlayerCategoryId,
                        HealthStatus = xmlPlayer.InjuryLevel,
                        BookingStatus = (BookingStatus)xmlPlayer.Cards,
                        Statement = xmlPlayer.Statement,
                        Notes = xmlPlayer.OwnerNotes,
                        Specialty = (Specialty)xmlPlayer.Specialty,
                        SeasonLeagueGoals = xmlPlayer.LeagueGoals,
                        SeasonFriendlyGoals = xmlPlayer.FriendlyGoals,
                        CareerGoals = xmlPlayer.CareerGoals,
                        CareerHattricks = xmlPlayer.CareerHattricks,
                        Country = country,
                        Team = team
                    });
            }
            else
            {
                juniorPlayer.FirstName = xmlPlayer.FirstName;
                juniorPlayer.NickName = xmlPlayer.NickName;
                juniorPlayer.LastName = xmlPlayer.LastName;
                juniorPlayer.AgeYears = xmlPlayer.Age;
                juniorPlayer.AgeDays = xmlPlayer.AgeDays;
                juniorPlayer.NextBirthDay = nextBirthDay;
                juniorPlayer.ShirtNumber = xmlPlayer.PlayerNumber;
                juniorPlayer.Category = (PlayerCategory)xmlPlayer.PlayerCategoryId;
                juniorPlayer.HealthStatus = xmlPlayer.InjuryLevel;
                juniorPlayer.BookingStatus = (BookingStatus)xmlPlayer.Cards;
                juniorPlayer.Statement = xmlPlayer.Statement;
                juniorPlayer.Notes = xmlPlayer.OwnerNotes;
                juniorPlayer.SeasonLeagueGoals = xmlPlayer.LeagueGoals;
                juniorPlayer.SeasonFriendlyGoals = xmlPlayer.FriendlyGoals;
                juniorPlayer.CareerGoals = xmlPlayer.CareerGoals;
                juniorPlayer.CareerHattricks = xmlPlayer.CareerHattricks;

                this.juniorPlayerRepository.Update(juniorPlayer);
            }

            return juniorPlayer;
        }

        private async Task ProcessPlayerSkillSet(
            Models.YouthPlayerDetails.PlayerSkills xmlPlayerSkills,
            Domain.Junior.Player juniorPlayer,
            int season,
            int week)
        {
            var playerSkillSet = await this.junorPlayerSkillSetRepository.Query(x => x.PlayerHattrickId == juniorPlayer.HattrickId
                                                                             && x.Season == season
                                                                             && x.Week == week)
                .SingleOrDefaultAsync();

            if (playerSkillSet == null)
            {
                await this.junorPlayerSkillSetRepository.InsertAsync(
                    new Domain.Junior.PlayerSkillSet
                    {
                        Season = season,
                        Week = week,
                        Goalkeeping = xmlPlayerSkills.KeeperSkill.Value != null ? (SkillLevel)xmlPlayerSkills.KeeperSkill.Value : null,
                        GoalkeepingMax = xmlPlayerSkills.KeeperSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.KeeperSkillMax.Value : null,
                        IsGoalkeepingMaxReached = xmlPlayerSkills.KeeperSkill.IsMaxReached,
                        Defending = xmlPlayerSkills.DefenderSkill.Value != null ? (SkillLevel)xmlPlayerSkills.DefenderSkill.Value : null,
                        DefendingMax = xmlPlayerSkills.DefenderSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.DefenderSkillMax.Value : null,
                        IsDefendingMaxReached = xmlPlayerSkills.DefenderSkill.IsMaxReached,
                        Playmaking = xmlPlayerSkills.PlaymakerSkill.Value != null ? (SkillLevel)xmlPlayerSkills.PlaymakerSkill.Value : null,
                        PlaymakingMax = xmlPlayerSkills.PlaymakerSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.PlaymakerSkillMax.Value : null,
                        IsPlaymakingMaxReached = xmlPlayerSkills.PlaymakerSkill.IsMaxReached,
                        Winger = xmlPlayerSkills.WingerSkill.Value != null ? (SkillLevel)xmlPlayerSkills.WingerSkill.Value : null,
                        WingerMax = xmlPlayerSkills.WingerSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.WingerSkillMax.Value : null,
                        IsWingerMaxReached = xmlPlayerSkills.WingerSkill.IsMaxReached,
                        Passing = xmlPlayerSkills.PassingSkill.Value != null ? (SkillLevel)xmlPlayerSkills.PassingSkill.Value : null,
                        PassingMax = xmlPlayerSkills.PassingSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.PassingSkillMax.Value : null,
                        IsPassingMaxReached = xmlPlayerSkills.PassingSkill.IsMaxReached,
                        Scoring = xmlPlayerSkills.ScorerSkill.Value != null ? (SkillLevel)xmlPlayerSkills.ScorerSkill.Value : null,
                        ScoringMax = xmlPlayerSkills.ScorerSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.ScorerSkillMax.Value : null,
                        IsScoringMaxReached = xmlPlayerSkills.ScorerSkill.IsMaxReached,
                        SetPieces = xmlPlayerSkills.SetPiecesSkill.Value != null ? (SkillLevel)xmlPlayerSkills.SetPiecesSkill.Value : null,
                        SetPiecesMax = xmlPlayerSkills.SetPiecesSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.SetPiecesSkillMax.Value : null,
                        IsSetPiecesMaxReached = xmlPlayerSkills.SetPiecesSkill.IsMaxReached,
                        Player = juniorPlayer
                    });
            }
            else
            {
                playerSkillSet.Goalkeeping = xmlPlayerSkills.KeeperSkill.Value != null ? (SkillLevel)xmlPlayerSkills.KeeperSkill.Value : null;
                playerSkillSet.GoalkeepingMax = xmlPlayerSkills.KeeperSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.KeeperSkillMax.Value : null;
                playerSkillSet.IsGoalkeepingMaxReached = xmlPlayerSkills.KeeperSkill.IsMaxReached;
                playerSkillSet.Defending = xmlPlayerSkills.DefenderSkill.Value != null ? (SkillLevel)xmlPlayerSkills.DefenderSkill.Value : null;
                playerSkillSet.DefendingMax = xmlPlayerSkills.DefenderSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.DefenderSkillMax.Value : null;
                playerSkillSet.IsDefendingMaxReached = xmlPlayerSkills.DefenderSkill.IsMaxReached;
                playerSkillSet.Playmaking = xmlPlayerSkills.PlaymakerSkill.Value != null ? (SkillLevel)xmlPlayerSkills.PlaymakerSkill.Value : null;
                playerSkillSet.PlaymakingMax = xmlPlayerSkills.PlaymakerSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.PlaymakerSkillMax.Value : null;
                playerSkillSet.IsPlaymakingMaxReached = xmlPlayerSkills.PlaymakerSkill.IsMaxReached;
                playerSkillSet.Winger = xmlPlayerSkills.WingerSkill.Value != null ? (SkillLevel)xmlPlayerSkills.WingerSkill.Value : null;
                playerSkillSet.WingerMax = xmlPlayerSkills.WingerSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.WingerSkillMax.Value : null;
                playerSkillSet.IsWingerMaxReached = xmlPlayerSkills.WingerSkill.IsMaxReached;
                playerSkillSet.Passing = xmlPlayerSkills.PassingSkill.Value != null ? (SkillLevel)xmlPlayerSkills.PassingSkill.Value : null;
                playerSkillSet.PassingMax = xmlPlayerSkills.PassingSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.PassingSkillMax.Value : null;
                playerSkillSet.IsPassingMaxReached = xmlPlayerSkills.PassingSkill.IsMaxReached;
                playerSkillSet.Scoring = xmlPlayerSkills.ScorerSkill.Value != null ? (SkillLevel)xmlPlayerSkills.ScorerSkill.Value : null;
                playerSkillSet.ScoringMax = xmlPlayerSkills.ScorerSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.ScorerSkillMax.Value : null;
                playerSkillSet.IsScoringMaxReached = xmlPlayerSkills.ScorerSkill.IsMaxReached;
                playerSkillSet.SetPieces = xmlPlayerSkills.SetPiecesSkill.Value != null ? (SkillLevel)xmlPlayerSkills.SetPiecesSkill.Value : null;
                playerSkillSet.SetPiecesMax = xmlPlayerSkills.SetPiecesSkillMax.Value != null ? (SkillLevel)xmlPlayerSkills.SetPiecesSkillMax.Value : null;
                playerSkillSet.IsSetPiecesMaxReached = xmlPlayerSkills.SetPiecesSkill.IsMaxReached;

                this.junorPlayerSkillSetRepository.Update(playerSkillSet);
            }
        }
    }
}