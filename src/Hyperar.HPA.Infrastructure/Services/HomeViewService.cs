namespace Hyperar.HPA.Infrastructure.Services
{
    using Hyperar.HPA.Application.Services;
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class HomeViewService : IHomeViewService
    {
        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public HomeViewService(IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository)
        {
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public async Task<Application.Models.SeniorPlayer[]> GetSeniorPlayerAsync(uint seniorTeamId)
        {
            var seniorTeam = await this.seniorTeamRepository.GetByHattrickIdAsync(seniorTeamId);

            ArgumentNullException.ThrowIfNull(seniorTeam);

            return seniorTeam.SeniorPlayers?.Select(x => Convert(x)).ToArray() ?? Array.Empty<Application.Models.SeniorPlayer>();
        }

        private static Application.Models.SeniorPlayer Convert(Domain.SeniorPlayer seniorPlayer)
        {
            ArgumentNullException.ThrowIfNull(seniorPlayer.SeniorPlayerSkills);

            var mostRecentSkills = seniorPlayer.SeniorPlayerSkills.OrderByDescending(x => x.UpdatedOn)
                .First();

            return new Application.Models.SeniorPlayer
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
                Loyalty = mostRecentSkills.Loyalty,
                Form = mostRecentSkills.Form,
                Stamina = mostRecentSkills.Stamina,
                Keeper = mostRecentSkills.Keeper,
                Defending = mostRecentSkills.Defending,
                Playmaking = mostRecentSkills.Playmaking,
                Winger = mostRecentSkills.Winger,
                Passing = mostRecentSkills.Passing,
                Scoring = mostRecentSkills.Scoring,
                SetPieces = mostRecentSkills.SetPieces,
                Experience = mostRecentSkills.Experience,
                SeasonLeagueGoals = seniorPlayer.CurrentSeasonLeagueGoals,
                SeasonCupGoals = seniorPlayer.CurrentSeasonCupGoals,
                SeasonFriendlyGoals = seniorPlayer.CurrentSeasonFriendlyGoals,
                CareerLeagueGoals = seniorPlayer.CareerGoals,
                CareerHattricks = seniorPlayer.CareerHattricks,
                TeamGoals = seniorPlayer.GoalsOnTeam,
                TeamMatches = seniorPlayer.MatchesOnTeam
            };
        }
    }
}