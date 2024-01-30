namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using System.Linq;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Common.Enums;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Hattrick = Application.Hattrick.Players;

    public class Players : IXmlFileDataPersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.PlayerAvatarLayer> playerAvatarLayerRepository;

        private readonly IHattrickRepository<Domain.Player> playerRepository;

        private readonly IRepository<Domain.PlayerSkillSet> playerSkillSetRepository;

        private readonly IHattrickRepository<Domain.Team> teamRepository;

        public Players(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.Player> playerRepository,
            IRepository<Domain.PlayerAvatarLayer> playerAvatarLayerRepository,
            IRepository<Domain.PlayerSkillSet> playerSkillSetRepository,
            IHattrickRepository<Domain.Team> teamRepository)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.playerRepository = playerRepository;
            this.playerAvatarLayerRepository = playerAvatarLayerRepository;
            this.playerSkillSetRepository = playerSkillSetRepository;
            this.teamRepository = teamRepository;
        }

        public async Task PersistDataAsync(IXmlFile file)
        {
            try
            {
                if (file is Hattrick.HattrickData xmlEntity)
                {
                    await this.ProcessPlayersAsync(xmlEntity);
                }
                else
                {
                    throw new ArgumentException(file.GetType().FullName, nameof(file));
                }
            }
            catch
            {
                this.databaseContext.Cancel();

                throw;
            }
        }

        private async Task ProcessPlayerAsync(Hattrick.Player xmlPlayer, Domain.Team team)
        {
            var player = await this.playerRepository.GetByHattrickIdAsync(xmlPlayer.PlayerId);

            if (player == null)
            {
                var country = await this.countryRepository.GetByHattrickIdAsync(xmlPlayer.CountryId);

                ArgumentNullException.ThrowIfNull(country, nameof(country));

                player = new Domain.Player
                {
                    HattrickId = xmlPlayer.PlayerId,
                    FirstName = xmlPlayer.FirstName,
                    NickName = xmlPlayer.NickName,
                    LastName = xmlPlayer.LastName,
                    ShirtNumber = xmlPlayer.PlayerNumber,
                    IsCoach = xmlPlayer.TrainerData != null,
                    AgeYears = xmlPlayer.Age,
                    AgeDays = xmlPlayer.AgeDays,
                    JoinedTeamOn = xmlPlayer.ArrivalDate,
                    Notes = xmlPlayer.OwnerNotes,
                    Statement = xmlPlayer.Statement,
                    TotalSkillIndex = xmlPlayer.Tsi,
                    HasMotherClubBonus = xmlPlayer.MotherClubBonus,
                    Salary = xmlPlayer.Salary,
                    IsForeign = xmlPlayer.IsAbroad,
                    Agreeability = xmlPlayer.Agreeability,
                    Aggressiveness = xmlPlayer.Aggressiveness,
                    Honesty = xmlPlayer.Honesty,
                    Leadership = xmlPlayer.Leadership,
                    Specialty = xmlPlayer.Specialty,
                    IsTransferListed = xmlPlayer.TransferListed,
                    EnrolledOnNationalTeam = xmlPlayer.NationalTeamId != null,
                    CurrentSeasonLeagueGoals = (uint)xmlPlayer.LeagueGoals,
                    CurrentSeasonCupGoals = (uint)xmlPlayer.CupGoals,
                    CurrentSeasonFriendlyGoals = (uint)xmlPlayer.FriendliesGoals,
                    CareerGoals = (uint)xmlPlayer.CareerGoals,
                    CareerHattricks = (uint)xmlPlayer.CareerHattricks,
                    GoalsOnTeam = (uint)xmlPlayer.GoalsCurrentTeam,
                    MatchesOnTeam = xmlPlayer.MatchesCurrentTeam,
                    NationalTeamCaps = xmlPlayer.Caps,
                    YouthNationalTeamCaps = xmlPlayer.CapsU20,
                    BookingStatus = (BookingStatus)xmlPlayer.Cards,
                    Health = xmlPlayer.InjuryLevel,
                    Category = xmlPlayer.PlayerCategoryId,
                    Country = country,
                    Team = team
                };

                await this.playerRepository.InsertAsync(player);
            }
            else
            {
                player.FirstName = xmlPlayer.FirstName;
                player.NickName = xmlPlayer.NickName;
                player.LastName = xmlPlayer.LastName;
                player.ShirtNumber = xmlPlayer.PlayerNumber;
                player.IsCoach = xmlPlayer.TrainerData != null;
                player.AgeYears = xmlPlayer.Age;
                player.AgeDays = xmlPlayer.AgeDays;
                player.Notes = xmlPlayer.OwnerNotes;
                player.Statement = xmlPlayer.Statement;
                player.TotalSkillIndex = xmlPlayer.Tsi;
                player.HasMotherClubBonus = xmlPlayer.MotherClubBonus;
                player.Salary = xmlPlayer.Salary;
                player.IsTransferListed = xmlPlayer.TransferListed;
                player.EnrolledOnNationalTeam = xmlPlayer.NationalTeamId != null;
                player.CurrentSeasonLeagueGoals = (uint)xmlPlayer.LeagueGoals;
                player.CurrentSeasonCupGoals = (uint)xmlPlayer.CupGoals;
                player.CurrentSeasonFriendlyGoals = (uint)xmlPlayer.FriendliesGoals;
                player.CareerGoals = (uint)xmlPlayer.CareerGoals;
                player.CareerHattricks = (uint)xmlPlayer.CareerHattricks;
                player.GoalsOnTeam = (uint)xmlPlayer.GoalsCurrentTeam;
                player.MatchesOnTeam = xmlPlayer.MatchesCurrentTeam;
                player.NationalTeamCaps = xmlPlayer.Caps;
                player.YouthNationalTeamCaps = xmlPlayer.CapsU20;
                player.BookingStatus = (BookingStatus)xmlPlayer.Cards;
                player.Health = xmlPlayer.InjuryLevel;
                player.Category = xmlPlayer.PlayerCategoryId;

                this.playerRepository.Update(player);
            }

            await this.ProcessPlayerSkillAsync(xmlPlayer, player, team.League.Season, team.League.Week);

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessPlayersAsync(Hattrick.HattrickData xmlEntity)
        {
            if (xmlEntity.IsPlayingMatch)
            {
                return;
            }

            var team = await this.teamRepository.GetByHattrickIdAsync(xmlEntity.Team.TeamId);

            ArgumentNullException.ThrowIfNull(team, nameof(team));

            List<uint> xmlPlayerIds = xmlEntity.Team.PlayerList.Select(x => x.PlayerId).ToList();

            var playersIdsToDelete = await this.playerRepository.Query(x => x.Team.HattrickId == team.HattrickId)
                                                                .Select(x => x.HattrickId)
                                                                .Except(xmlPlayerIds)
                                                                .ToListAsync();

            var playerAvatarLayersIdsToDelete = this.playerRepository.Query(x => playersIdsToDelete.Contains(x.HattrickId))
                                                                     .SelectMany(x => x.AvatarLayers.Select(y => y.Id))
                                                                     .ToList();

            var playerSkillSetsIdsToDelete = this.playerRepository.Query(x => playersIdsToDelete.Contains(x.HattrickId))
                                                                  .SelectMany(x => x.PlayerSkillSets.Select(y => y.Id))
                                                                  .ToList();

            await this.playerAvatarLayerRepository.DeleteRangeAsync(playerAvatarLayersIdsToDelete);
            await this.playerSkillSetRepository.DeleteRangeAsync(playerSkillSetsIdsToDelete);
            await this.playerRepository.DeleteRangeAsync(playersIdsToDelete);

            foreach (var curXmlPlayer in xmlEntity.Team.PlayerList)
            {
                await this.ProcessPlayerAsync(curXmlPlayer, team);
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessPlayerSkillAsync(Hattrick.Player xmlPlayer, Domain.Player player, uint season, uint week)
        {
            var currentWeekSkill = await this.playerSkillSetRepository.Query(x => x.Player.HattrickId == xmlPlayer.PlayerId
                                                                               && x.Season == season
                                                                               && x.Week == week)
                                                                      .SingleOrDefaultAsync();

            if (currentWeekSkill == null)
            {
                currentWeekSkill = new Domain.PlayerSkillSet
                {
                    Form = xmlPlayer.PlayerForm,
                    Stamina = xmlPlayer.StaminaSkill,
                    Keeper = xmlPlayer.KeeperSkill,
                    Defending = xmlPlayer.DefenderSkill,
                    Playmaking = xmlPlayer.PlaymakerSkill,
                    Winger = xmlPlayer.WingerSkill,
                    Passing = xmlPlayer.PassingSkill,
                    Scoring = xmlPlayer.ScorerSkill,
                    SetPieces = xmlPlayer.SetPiecesSkill,
                    Loyalty = xmlPlayer.Loyalty,
                    Experience = xmlPlayer.Experience,
                    Season = season,
                    Week = week,
                    Player = player
                };

                await this.playerSkillSetRepository.InsertAsync(currentWeekSkill);
            }
            else
            {
                currentWeekSkill.Form = xmlPlayer.PlayerForm;
                currentWeekSkill.Stamina = xmlPlayer.StaminaSkill;
                currentWeekSkill.Keeper = xmlPlayer.KeeperSkill;
                currentWeekSkill.Defending = xmlPlayer.DefenderSkill;
                currentWeekSkill.Playmaking = xmlPlayer.PlaymakerSkill;
                currentWeekSkill.Winger = xmlPlayer.WingerSkill;
                currentWeekSkill.Passing = xmlPlayer.PassingSkill;
                currentWeekSkill.Scoring = xmlPlayer.ScorerSkill;
                currentWeekSkill.SetPieces = xmlPlayer.SetPiecesSkill;
                currentWeekSkill.Experience = xmlPlayer.Experience;
                currentWeekSkill.Loyalty = xmlPlayer.Loyalty;

                this.playerSkillSetRepository.Update(currentWeekSkill);
            }

            await this.databaseContext.SaveAsync();
        }
    }
}