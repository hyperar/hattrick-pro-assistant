namespace Hyperar.HPA.Infrastructure.Services
{
    using System.Linq;
    using Shared.Models.UI.Players;
    using Application.Services;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Hyperar.HPA.Shared.Enums;

    public class PlayersViewService : IPlayersViewService
    {
        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        private readonly IRepository<Domain.User> userRepository;

        public PlayersViewService(
            IHattrickRepository<Domain.Senior.Team> teamRepository,
            IRepository<Domain.User> userRepository)
        {
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
        }

        public async Task<Currency> GetManagerCurrencyAsync()
        {
            Domain.User? user = await this.userRepository.Query().SingleOrDefaultAsync();

            ArgumentNullException.ThrowIfNull(user, nameof(user));
            ArgumentNullException.ThrowIfNull(user.Manager, nameof(user.Manager));

            return new Currency
            {
                Name = user.Manager.CurrencyName,
                Rate = user.Manager.CurrencyRate
            };
        }

        public async Task<Player[]> GetPlayersAsync(long teamId)
        {
            Domain.Senior.Team? team = await this.teamRepository.GetByHattrickIdAsync(teamId);

            ArgumentNullException.ThrowIfNull(team, nameof(team));

            return team.Players.Where(x => x.HattrickId != x.Team.CoachPlayerId)
                               .OrderByDescending(x => x.ShirtNumber.HasValue)
                               .ThenBy(x => x.ShirtNumber)
                               .Select(Convert)
                               .ToArray();
        }

        private static Player Convert(Domain.Senior.Player player)
        {
            ArgumentNullException.ThrowIfNull(player.PlayerSkillSets, nameof(player.PlayerSkillSets));

            Domain.Senior.PlayerSkillSet currentSkills = player.PlayerSkillSets.OrderByDescending(x => x.Season)
                                                      .ThenByDescending(x => x.Week)
                                                      .First();

            Domain.Senior.PlayerSkillSet? previousSkills = player.PlayerSkillSets.OrderByDescending(x => x.Season)
                                                       .ThenByDescending(x => x.Week)
                                                       .Skip(1)
                                                       .FirstOrDefault();

            return new Player
            {
                HattrickId = player.HattrickId,
                FirstName = player.FirstName,
                NickName = player.NickName,
                LastName = player.LastName,
                ShirtNumber = player.ShirtNumber,
                AgeYears = player.AgeYears,
                AgeDays = player.AgeDays,
                TotalSkillIndex = player.TotalSkillIndex,
                Country = new Country()
                {
                    FlagBytes = player.Country.League.FlagBytes,
                    HattrickId = player.Country.HattrickId,
                    Name = player.Country.Name
                },
                HasMotherClubBonus = player.HasMotherClubBonus,
                IsTransferListed = player.IsTransferListed,
                Salary = player.Salary,
                Specialty = player.Specialty,
                AgreeabilityLevel = player.Agreeability,
                AggressivenessLevel = player.Aggressiveness,
                HonestyLevel = player.Honesty,
                LeadershipLevel = player.Leadership,
                BookingStatus = player.BookingStatus,
                HealthStatus = player.Health,
                FormLevel = currentSkills.Form,
                FormLevelDelta = GetPlayerSkillDelta(currentSkills.Form, previousSkills?.Form),
                StaminaLevel = currentSkills.Stamina,
                StaminaLevelDelta = GetPlayerSkillDelta(currentSkills.Stamina, previousSkills?.Stamina),
                KeeperLevel = currentSkills.Keeper,
                KeeperLevelDelta = GetPlayerSkillDelta(currentSkills.Keeper, previousSkills?.Keeper),
                DefendingLevel = currentSkills.Defending,
                DefendingLevelDelta = GetPlayerSkillDelta(currentSkills.Defending, previousSkills?.Defending),
                PlaymakingLevel = currentSkills.Playmaking,
                PlaymakingLevelDelta = GetPlayerSkillDelta(currentSkills.Playmaking, previousSkills?.Playmaking),
                WingerLevel = currentSkills.Winger,
                WingerLevelDelta = GetPlayerSkillDelta(currentSkills.Winger, previousSkills?.Winger),
                PassingLevel = currentSkills.Passing,
                PassingLevelDelta = GetPlayerSkillDelta(currentSkills.Passing, previousSkills?.Passing),
                ScoringLevel = currentSkills.Scoring,
                ScoringLevelDelta = GetPlayerSkillDelta(currentSkills.Scoring, previousSkills?.Scoring),
                SetPiecesLevel = currentSkills.SetPieces,
                SetPiecesLevelDelta = GetPlayerSkillDelta(currentSkills.SetPieces, previousSkills?.SetPieces),
                LoyaltyLevel = currentSkills.Loyalty,
                LoyaltyLevelDelta = GetPlayerSkillDelta(currentSkills.Loyalty, previousSkills?.Loyalty),
                ExperienceLevel = currentSkills.Experience,
                ExperienceLevelDelta = GetPlayerSkillDelta(currentSkills.Experience, previousSkills?.Experience),
                SeasonLeagueGoals = player.CurrentSeasonLeagueGoals,
                SeasonCupGoals = player.CurrentSeasonCupGoals,
                SeasonFriendlyGoals = player.CurrentSeasonFriendlyGoals,
                CareerLeagueGoals = player.CareerGoals,
                CareerHattricks = player.CareerHattricks,
                GoalsOnTeam = player.GoalsOnTeam,
                MatchesOnTeam = player.MatchesOnTeam,
                AvatarBytes = player.AvatarBytes ?? Array.Empty<byte>(),
            };
        }

        private static short GetPlayerSkillDelta(SkillLevel currentSkillLevel, SkillLevel? previousSkillLevel)
        {
            short currentSkillLevelAux = (short)currentSkillLevel;
            short previousSkillLevelAux = (short)(previousSkillLevel ?? currentSkillLevel);

            return (short)(currentSkillLevelAux - previousSkillLevelAux);
        }
    }
}