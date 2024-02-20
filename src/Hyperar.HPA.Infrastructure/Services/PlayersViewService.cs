namespace Hyperar.HPA.Infrastructure.Services
{
    using System.Linq;
    using Application.Models.Players;
    using Application.Services;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

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
            var user = await this.userRepository.Query().SingleOrDefaultAsync();

            ArgumentNullException.ThrowIfNull(user, nameof(user));
            ArgumentNullException.ThrowIfNull(user.Manager, nameof(user.Manager));

            return new Currency
            {
                Name = user.Manager.CurrencyName,
                Rate = user.Manager.CurrencyRate
            };
        }

        public async Task<Player[]> GetPlayerAsync(uint teamId)
        {
            var team = await this.teamRepository.GetByHattrickIdAsync(teamId);

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

            var currentSkills = player.PlayerSkillSets.OrderByDescending(x => x.Season)
                                                      .ThenByDescending(x => x.Week)
                                                      .First();

            var previousSkills = player.PlayerSkillSets.OrderByDescending(x => x.Season)
                                                       .ThenByDescending(x => x.Week)
                                                       .Skip(1)
                                                       .FirstOrDefault();

            return new Player
            {
                Id = player.HattrickId,
                FirstName = player.FirstName,
                NickName = player.NickName,
                LastName = player.LastName,
                ShirtNumber = player.ShirtNumber,
                AgeYears = player.AgeYears,
                AgeDays = player.AgeDays,
                TotalSkillIndex = player.TotalSkillIndex,
                HasMotherClubBonus = player.HasMotherClubBonus,
                Salary = player.Salary,
                Specialty = player.Specialty,
                Agreeability = player.Agreeability,
                Aggressiveness = player.Aggressiveness,
                Honesty = player.Honesty,
                Leadership = player.Leadership,
                BookingStatus = player.BookingStatus,
                Health = player.Health,
                Form = currentSkills.Form,
                FormDelta = previousSkills == null ? null : currentSkills.Form - previousSkills.Form,
                Stamina = currentSkills.Stamina,
                StaminaDelta = previousSkills == null ? null : currentSkills.Stamina - previousSkills.Stamina,
                Keeper = currentSkills.Keeper,
                KeeperDelta = previousSkills == null ? null : currentSkills.Keeper - previousSkills.Keeper,
                Defending = currentSkills.Defending,
                DefendingDelta = previousSkills == null ? null : currentSkills.Defending - previousSkills.Defending,
                Playmaking = currentSkills.Playmaking,
                PlaymakingDelta = previousSkills == null ? null : currentSkills.Playmaking - previousSkills.Playmaking,
                Winger = currentSkills.Winger,
                WingerDelta = previousSkills == null ? null : currentSkills.Winger - previousSkills.Winger,
                Passing = currentSkills.Passing,
                PassingDelta = previousSkills == null ? null : currentSkills.Passing - previousSkills.Passing,
                Scoring = currentSkills.Scoring,
                ScoringDelta = previousSkills == null ? null : currentSkills.Scoring - previousSkills.Scoring,
                SetPieces = currentSkills.SetPieces,
                SetPiecesDelta = previousSkills == null ? null : currentSkills.SetPieces - previousSkills.SetPieces,
                Loyalty = currentSkills.Loyalty,
                LoyaltyDelta = previousSkills == null ? null : currentSkills.Loyalty - previousSkills.Loyalty,
                Experience = currentSkills.Experience,
                ExperienceDelta = previousSkills == null ? null : currentSkills.Experience - previousSkills.Experience,
                SeasonLeagueGoals = player.CurrentSeasonLeagueGoals,
                SeasonCupGoals = player.CurrentSeasonCupGoals,
                SeasonFriendlyGoals = player.CurrentSeasonFriendlyGoals,
                CareerLeagueGoals = player.CareerGoals,
                CareerHattricks = player.CareerHattricks,
                TeamGoals = player.GoalsOnTeam,
                TeamMatches = player.MatchesOnTeam,
                Avatar = player.AvatarBytes,
                LeagueFlag = player.Country.League.FlagBytes,
                CountryName = player.Country.Name
            };
        }
    }
}