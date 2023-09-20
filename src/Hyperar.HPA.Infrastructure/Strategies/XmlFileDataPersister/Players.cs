namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using System.Linq;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Domain.Interfaces;
    using Hattrick = Application.Hattrick.Players;

    public class Players : IXmlFileDataPersisterStrategy
    {
        private readonly IDatabaseContext context;

        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IHattrickRepository<Domain.SeniorPlayer> seniorPlayerRepository;

        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public Players(
            IDatabaseContext context,
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.SeniorPlayer> seniorPlayerRepository,
            IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository)
        {
            this.context = context;
            this.countryRepository = countryRepository;
            this.seniorPlayerRepository = seniorPlayerRepository;
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public void PersistData(IXmlFile file)
        {
            if (file is not Hattrick.HattrickData entity)
            {
                throw new ArgumentException(nameof(file));
            }

            if (entity.IsPlayingMatch)
            {
                return;
            }

            var seniorTeam = this.seniorTeamRepository.GetByHattrickId(entity.Team.TeamId);

            if (seniorTeam == null)
            {
                throw new Exception($"Senior Team with Hattrick ID \"{entity.Team.TeamId}\" not found.");
            }

            this.context.BeginTransaction();

            List<uint> xmlPlayerIds = entity.Team.PlayerList.Select(x => x.PlayerId).ToList();

            var seniorPlayersToDelete = this.seniorPlayerRepository.Query(x => !xmlPlayerIds.Contains(x.HattrickId)).ToList();

            foreach (var curSeniorPlayer in seniorPlayersToDelete)
            {
                this.seniorPlayerRepository.Delete(curSeniorPlayer.HattrickId);
            }

            foreach (var curXmlPlayer in entity.Team.PlayerList)
            {
                this.ProcessPlayer(curXmlPlayer, seniorTeam);
            }

            this.context.Save();

            this.context.EndTransaction();
        }

        private void ProcessPlayer(Hattrick.Player xmlPlayer, Domain.SeniorTeam seniorTeam)
        {
            var seniorPlayer = this.seniorPlayerRepository.GetByHattrickId(xmlPlayer.PlayerId);

            if (seniorPlayer == null)
            {
                var country = this.countryRepository.GetByHattrickId(xmlPlayer.CountryId);

                if (country == null)
                {
                    throw new Exception($"Country with Hattrick ID \"{xmlPlayer.CountryId}\" not found.");
                }

                seniorPlayer = new Domain.SeniorPlayer
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
                    MatchesOnTeam = (uint)xmlPlayer.MatchesCurrentTeam,
                    SeniorNationalTeamCaps = xmlPlayer.Caps,
                    YouthNationalTeamCaps = xmlPlayer.CapsU20,
                    BookingStatus = (BookingStatus)xmlPlayer.Cards,
                    Health = xmlPlayer.InjuryLevel,
                    Loyalty = xmlPlayer.Loyalty,
                    Form = xmlPlayer.PlayerForm,
                    Stamina = xmlPlayer.StaminaSkill,
                    Keeper = xmlPlayer.KeeperSkill,
                    Defending = xmlPlayer.DefenderSkill,
                    Playmaking = xmlPlayer.PlaymakerSkill,
                    Winger = xmlPlayer.WingerSkill,
                    Passing = xmlPlayer.PassingSkill,
                    Scoring = xmlPlayer.ScorerSkill,
                    SetPieces = xmlPlayer.SetPiecesSkill,
                    Experience = xmlPlayer.Experience,
                    Category = xmlPlayer.PlayerCategoryId,
                    Country = country,
                    SeniorTeam = seniorTeam
                };

                this.seniorPlayerRepository.Insert(seniorPlayer);
            }
            else
            {
                seniorPlayer.FirstName = xmlPlayer.FirstName;
                seniorPlayer.NickName = xmlPlayer.NickName;
                seniorPlayer.LastName = xmlPlayer.LastName;
                seniorPlayer.ShirtNumber = xmlPlayer.PlayerNumber;
                seniorPlayer.IsCoach = xmlPlayer.TrainerData != null;
                seniorPlayer.AgeYears = xmlPlayer.Age;
                seniorPlayer.AgeDays = xmlPlayer.AgeDays;
                seniorPlayer.Notes = xmlPlayer.OwnerNotes;
                seniorPlayer.Statement = xmlPlayer.Statement;
                seniorPlayer.TotalSkillIndex = xmlPlayer.Tsi;
                seniorPlayer.HasMotherClubBonus = xmlPlayer.MotherClubBonus;
                seniorPlayer.Salary = xmlPlayer.Salary;
                seniorPlayer.IsTransferListed = xmlPlayer.TransferListed;
                seniorPlayer.EnrolledOnNationalTeam = xmlPlayer.NationalTeamId != null;
                seniorPlayer.CurrentSeasonLeagueGoals = (uint)xmlPlayer.LeagueGoals;
                seniorPlayer.CurrentSeasonCupGoals = (uint)xmlPlayer.CupGoals;
                seniorPlayer.CurrentSeasonFriendlyGoals = (uint)xmlPlayer.FriendliesGoals;
                seniorPlayer.CareerGoals = (uint)xmlPlayer.CareerGoals;
                seniorPlayer.CareerHattricks = (uint)xmlPlayer.CareerHattricks;
                seniorPlayer.GoalsOnTeam = (uint)xmlPlayer.GoalsCurrentTeam;
                seniorPlayer.MatchesOnTeam = (uint)xmlPlayer.MatchesCurrentTeam;
                seniorPlayer.SeniorNationalTeamCaps = xmlPlayer.Caps;
                seniorPlayer.YouthNationalTeamCaps = xmlPlayer.CapsU20;
                seniorPlayer.BookingStatus = (BookingStatus)xmlPlayer.Cards;
                seniorPlayer.Health = xmlPlayer.InjuryLevel;
                seniorPlayer.Loyalty = xmlPlayer.Loyalty;
                seniorPlayer.Form = xmlPlayer.PlayerForm;
                seniorPlayer.Stamina = xmlPlayer.StaminaSkill;
                seniorPlayer.Keeper = xmlPlayer.KeeperSkill;
                seniorPlayer.Defending = xmlPlayer.DefenderSkill;
                seniorPlayer.Playmaking = xmlPlayer.PlaymakerSkill;
                seniorPlayer.Winger = xmlPlayer.WingerSkill;
                seniorPlayer.Passing = xmlPlayer.PassingSkill;
                seniorPlayer.Scoring = xmlPlayer.ScorerSkill;
                seniorPlayer.SetPieces = xmlPlayer.SetPiecesSkill;
                seniorPlayer.Experience = xmlPlayer.Experience;
                seniorPlayer.Category = xmlPlayer.PlayerCategoryId;

                this.seniorPlayerRepository.Update(seniorPlayer);
            }
        }
    }
}