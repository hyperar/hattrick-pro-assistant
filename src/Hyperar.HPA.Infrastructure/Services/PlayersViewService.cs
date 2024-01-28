namespace Hyperar.HPA.Infrastructure.Services
{
    using System.Linq;
    using Application.Models.Players;
    using Application.Services;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class PlayersViewService : IPlayersViewService
    {
        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        private readonly IRepository<Domain.User> userRepository;

        public PlayersViewService(
            IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository,
            IRepository<Domain.User> userRepository)
        {
            this.seniorTeamRepository = seniorTeamRepository;
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

        public async Task<SeniorPlayer[]> GetSeniorPlayerAsync(uint seniorTeamId)
        {
            var seniorTeam = await this.seniorTeamRepository.GetByHattrickIdAsync(seniorTeamId);

            ArgumentNullException.ThrowIfNull(seniorTeam, nameof(seniorTeam));

            return seniorTeam.SeniorPlayers.Where(x => x.HattrickId != x.SeniorTeam.CoachPlayerId)
                                           .OrderByDescending(x => x.ShirtNumber.HasValue)
                                           .ThenBy(x => x.ShirtNumber)
                                           .Select(Convert)
                                           .ToArray();
        }

        private static SeniorPlayer Convert(Domain.SeniorPlayer seniorPlayer)
        {
            ArgumentNullException.ThrowIfNull(seniorPlayer.SeniorPlayerSkills, nameof(seniorPlayer.SeniorPlayerSkills));

            var currentSkills = seniorPlayer.SeniorPlayerSkills.OrderByDescending(x => x.Season)
                                                               .ThenByDescending(x => x.Week)
                                                               .First();

            var previousSkills = seniorPlayer.SeniorPlayerSkills.OrderByDescending(x => x.Season)
                                                                .ThenByDescending(x => x.Week)
                                                                .Skip(1)
                                                                .FirstOrDefault();

            return new SeniorPlayer
            {
                Id = seniorPlayer.HattrickId,
                FirstName = seniorPlayer.FirstName,
                NickName = seniorPlayer.NickName,
                LastName = seniorPlayer.LastName,
                ShirtNumber = seniorPlayer.ShirtNumber,
                AgeYears = seniorPlayer.AgeYears,
                AgeDays = seniorPlayer.AgeDays,
                TotalSkillIndex = seniorPlayer.TotalSkillIndex,
                HasMotherClubBonus = seniorPlayer.HasMotherClubBonus,
                Salary = seniorPlayer.Salary,
                Specialty = seniorPlayer.Specialty,
                Agreeability = seniorPlayer.Agreeability,
                Aggressiveness = seniorPlayer.Aggressiveness,
                Honesty = seniorPlayer.Honesty,
                Leadership = seniorPlayer.Leadership,
                BookingStatus = seniorPlayer.BookingStatus,
                Health = seniorPlayer.Health,
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
                SeasonLeagueGoals = seniorPlayer.CurrentSeasonLeagueGoals,
                SeasonCupGoals = seniorPlayer.CurrentSeasonCupGoals,
                SeasonFriendlyGoals = seniorPlayer.CurrentSeasonFriendlyGoals,
                CareerLeagueGoals = seniorPlayer.CareerGoals,
                CareerHattricks = seniorPlayer.CareerHattricks,
                TeamGoals = seniorPlayer.GoalsOnTeam,
                TeamMatches = seniorPlayer.MatchesOnTeam,
                Avatar = seniorPlayer.Avatar,
                LeagueFlag = seniorPlayer.Country.League.Flag,
                CountryName = seniorPlayer.Country.Name
            };
        }
    }
}