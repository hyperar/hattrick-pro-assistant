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

    public class PlayerDetails : DownloadTaskStrategyBase, IPersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IHattrickRepository<Domain.Senior.Player> playerRepository;

        private readonly IAuditableRepository<Domain.Senior.PlayerSkillSet> playerSkillSetRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> seniorTeamRepository;

        public PlayerDetails(
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.Senior.Player> playerRepository,
            IAuditableRepository<Domain.Senior.PlayerSkillSet> playerSkillSetRepository,
            IHattrickRepository<Domain.Senior.Team> teamRepository)
        {
            this.countryRepository = countryRepository;
            this.playerRepository = playerRepository;
            this.playerSkillSetRepository = playerSkillSetRepository;
            this.seniorTeamRepository = teamRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            if (task.XmlFile is Models.PlayerDetails.HattrickData file)
            {
                var team = await this.seniorTeamRepository.GetByHattrickIdAsync(file.Player.OwningTeam.TeamId);

                ArgumentNullException.ThrowIfNull(team, nameof(team));

                var player = await this.ProcessPlayerAsync(file.Player, team);

                await this.ProcessPlayerSkillSet(file.Player, player, team.League.Season, team.League.Week);
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }

        private async Task<Domain.Senior.Player> ProcessPlayerAsync(Models.PlayerDetails.Player xmlPlayer, Domain.Senior.Team team)
        {
            var player = await this.playerRepository.GetByHattrickIdAsync(xmlPlayer.PlayerId);

            if (player == null)
            {
                var country = await this.countryRepository.GetByHattrickIdAsync(xmlPlayer.NativeCountryId);

                ArgumentNullException.ThrowIfNull(country, nameof(country));

                player = await this.playerRepository.InsertAsync(
                    new Domain.Senior.Player
                    {
                        HattrickId = xmlPlayer.PlayerId,
                        FirstName = xmlPlayer.FirstName,
                        NickName = xmlPlayer.NickName,
                        LastName = xmlPlayer.LastName,
                        AgeYears = xmlPlayer.Age,
                        AgeDays = xmlPlayer.AgeDays,
                        JoinedOn = xmlPlayer.ArrivalDate,
                        NextBirthDay = xmlPlayer.NextBirthDay,
                        ShirtNumber = xmlPlayer.PlayerNumber,
                        Category = (PlayerCategory)xmlPlayer.PlayerCategoryId,
                        HasMotherClubBonus = xmlPlayer.MotherClubBonus,
                        HealthStatus = xmlPlayer.InjuryLevel,
                        BookingStatus = (BookingStatus)xmlPlayer.Cards,
                        Statement = xmlPlayer.Statement,
                        Notes = xmlPlayer.OwnerNotes,
                        TotalSkillIndex = xmlPlayer.TSI,
                        Salary = xmlPlayer.Salary,
                        IsTransferListed = xmlPlayer.TransferListed,
                        Agreeability = (AgreeabilityLevel)xmlPlayer.Agreeability,
                        Aggressiveness = (AggressivenessLevel)xmlPlayer.Aggressiveness,
                        Honesty = (HonestyLevel)xmlPlayer.Honesty,
                        Leadership = (SkillLevel)xmlPlayer.Leadership,
                        Specialty = (Specialty)xmlPlayer.Specialty,
                        SeniorNationalTeamCaps = xmlPlayer.Caps,
                        JuniorNationalTeamCaps = xmlPlayer.CapsU20,
                        SeasonLeagueGoals = xmlPlayer.LeagueGoals,
                        SeasonCupGoals = xmlPlayer.CupGoals,
                        SeasonFriendlyGoals = xmlPlayer.FriendliesGoals,
                        CareerGoals = xmlPlayer.CareerGoals,
                        CareerHattricks = xmlPlayer.CareerHattricks,
                        GoalsOnTeam = xmlPlayer.GoalsCurrentTeam,
                        MatchesOnTeam = xmlPlayer.MatchesCurrentTeam,
                        IsForeign = xmlPlayer.IsAbroad,
                        Country = country,
                        Team = team
                    });
            }
            else
            {
                player.FirstName = xmlPlayer.FirstName;
                player.NickName = xmlPlayer.NickName;
                player.LastName = xmlPlayer.LastName;
                player.AgeYears = xmlPlayer.Age;
                player.AgeDays = xmlPlayer.AgeDays;
                player.NextBirthDay = xmlPlayer.NextBirthDay;
                player.ShirtNumber = xmlPlayer.PlayerNumber;
                player.Category = (PlayerCategory)xmlPlayer.PlayerCategoryId;
                player.HasMotherClubBonus = xmlPlayer.MotherClubBonus;
                player.HealthStatus = xmlPlayer.InjuryLevel;
                player.BookingStatus = (BookingStatus)xmlPlayer.Cards;
                player.Statement = xmlPlayer.Statement;
                player.Notes = xmlPlayer.OwnerNotes;
                player.TotalSkillIndex = xmlPlayer.TSI;
                player.Salary = xmlPlayer.Salary;
                player.IsTransferListed = xmlPlayer.TransferListed;
                player.SeniorNationalTeamCaps = xmlPlayer.Caps;
                player.JuniorNationalTeamCaps = xmlPlayer.CapsU20;
                player.SeasonLeagueGoals = xmlPlayer.LeagueGoals;
                player.SeasonCupGoals = xmlPlayer.CupGoals;
                player.SeasonFriendlyGoals = xmlPlayer.FriendliesGoals;
                player.CareerGoals = xmlPlayer.CareerGoals;
                player.CareerHattricks = xmlPlayer.CareerHattricks;
                player.GoalsOnTeam = xmlPlayer.GoalsCurrentTeam;
                player.MatchesOnTeam = xmlPlayer.MatchesCurrentTeam;

                this.playerRepository.Update(player);
            }

            return player;
        }

        private async Task ProcessPlayerSkillSet(
            Models.PlayerDetails.Player xmlPlayer,
            Domain.Senior.Player player,
            int season,
            int week)
        {
            var playerSkillSet = await this.playerSkillSetRepository.Query(x => x.PlayerHattrickId == player.HattrickId
                                                                             && x.Season == season
                                                                             && x.Week == week)
                .SingleOrDefaultAsync();

            if (playerSkillSet == null)
            {
                await this.playerSkillSetRepository.InsertAsync(
                    new Domain.Senior.PlayerSkillSet
                    {
                        Season = season,
                        Week = week,
                        Form = (SkillLevel)xmlPlayer.PlayerForm,
                        Stamina = (SkillLevel)xmlPlayer.PlayerSkills.StaminaSkill,
                        Goalkeeping = (SkillLevel)xmlPlayer.PlayerSkills.KeeperSkill,
                        Defending = (SkillLevel)xmlPlayer.PlayerSkills.DefenderSkill,
                        Playmaking = (SkillLevel)xmlPlayer.PlayerSkills.PlaymakerSkill,
                        Winger = (SkillLevel)xmlPlayer.PlayerSkills.WingerSkill,
                        Passing = (SkillLevel)xmlPlayer.PlayerSkills.PassingSkill,
                        Scoring = (SkillLevel)xmlPlayer.PlayerSkills.ScorerSkill,
                        SetPieces = (SkillLevel)xmlPlayer.PlayerSkills.SetPiecesSkill,
                        Experience = (SkillLevel)xmlPlayer.Experience,
                        Loyalty = (SkillLevel)xmlPlayer.Loyalty,
                        Player = player
                    });
            }
            else
            {
                playerSkillSet.Form = (SkillLevel)xmlPlayer.PlayerForm;
                playerSkillSet.Stamina = (SkillLevel)xmlPlayer.PlayerSkills.StaminaSkill;
                playerSkillSet.Goalkeeping = (SkillLevel)xmlPlayer.PlayerSkills.KeeperSkill;
                playerSkillSet.Defending = (SkillLevel)xmlPlayer.PlayerSkills.DefenderSkill;
                playerSkillSet.Playmaking = (SkillLevel)xmlPlayer.PlayerSkills.PlaymakerSkill;
                playerSkillSet.Winger = (SkillLevel)xmlPlayer.PlayerSkills.WingerSkill;
                playerSkillSet.Passing = (SkillLevel)xmlPlayer.PlayerSkills.PassingSkill;
                playerSkillSet.Scoring = (SkillLevel)xmlPlayer.PlayerSkills.ScorerSkill;
                playerSkillSet.SetPieces = (SkillLevel)xmlPlayer.PlayerSkills.SetPiecesSkill;
                playerSkillSet.Experience = (SkillLevel)xmlPlayer.Experience;
                playerSkillSet.Loyalty = (SkillLevel)xmlPlayer.Loyalty;

                this.playerSkillSetRepository.Update(playerSkillSet);
            }
        }
    }
}