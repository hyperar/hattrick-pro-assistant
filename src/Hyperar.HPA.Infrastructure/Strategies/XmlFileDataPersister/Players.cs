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

        private readonly IHattrickRepository<Domain.SeniorPlayer> seniorPlayerRepository;

        private readonly IRepository<Domain.SeniorPlayerSkill> seniorPlayerSkillRepository;

        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public Players(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.SeniorPlayer> seniorPlayerRepository,
            IRepository<Domain.SeniorPlayerSkill> seniorPlayerSkillRepository,
            IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.seniorPlayerRepository = seniorPlayerRepository;
            this.seniorPlayerSkillRepository = seniorPlayerSkillRepository;
            this.seniorTeamRepository = seniorTeamRepository;
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

        private async Task ProcessPlayerAsync(Hattrick.Player xmlPlayer, Domain.SeniorTeam seniorTeam)
        {
            var seniorPlayer = await this.seniorPlayerRepository.GetByHattrickIdAsync(xmlPlayer.PlayerId);

            if (seniorPlayer == null)
            {
                var country = await this.countryRepository.GetByHattrickIdAsync(xmlPlayer.CountryId);

                ArgumentNullException.ThrowIfNull(country, nameof(country));

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
                    Category = xmlPlayer.PlayerCategoryId,
                    Country = country,
                    SeniorTeam = seniorTeam
                };

                await this.seniorPlayerRepository.InsertAsync(seniorPlayer);
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
                seniorPlayer.MatchesOnTeam = xmlPlayer.MatchesCurrentTeam;
                seniorPlayer.SeniorNationalTeamCaps = xmlPlayer.Caps;
                seniorPlayer.YouthNationalTeamCaps = xmlPlayer.CapsU20;
                seniorPlayer.BookingStatus = (BookingStatus)xmlPlayer.Cards;
                seniorPlayer.Health = xmlPlayer.InjuryLevel;
                seniorPlayer.Category = xmlPlayer.PlayerCategoryId;

                this.seniorPlayerRepository.Update(seniorPlayer);
            }

            await this.ProcessPlayerSkillAsync(xmlPlayer, seniorPlayer);

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessPlayersAsync(Hattrick.HattrickData xmlEntity)
        {
            if (xmlEntity.IsPlayingMatch)
            {
                return;
            }

            var seniorTeam = await this.seniorTeamRepository.GetByHattrickIdAsync(xmlEntity.Team.TeamId);

            ArgumentNullException.ThrowIfNull(seniorTeam, nameof(seniorTeam));

            List<uint> xmlPlayerIds = xmlEntity.Team.PlayerList.Select(x => x.PlayerId).ToList();

            var seniorPlayersToDelete = await this.seniorPlayerRepository.Query(x => x.SeniorTeam.HattrickId == seniorTeam.HattrickId
                                                                                  && !xmlPlayerIds.Contains(x.HattrickId)).ToListAsync();

            foreach (var curSeniorPlayer in seniorPlayersToDelete)
            {
                await this.seniorPlayerRepository.DeleteAsync(curSeniorPlayer.HattrickId);
            }

            foreach (var curXmlPlayer in xmlEntity.Team.PlayerList)
            {
                await this.ProcessPlayerAsync(curXmlPlayer, seniorTeam);
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessPlayerSkillAsync(Hattrick.Player xmlPlayer, Domain.SeniorPlayer seniorPlayer)
        {
            var seniorPlayerSkill = await this.seniorPlayerSkillRepository.Query(x => x.SeniorPlayer.HattrickId == xmlPlayer.PlayerId
                                                                                   && x.Loyalty == xmlPlayer.Loyalty
                                                                                   && x.Form == xmlPlayer.PlayerForm
                                                                                   && x.Stamina == xmlPlayer.StaminaSkill
                                                                                   && x.Keeper == xmlPlayer.KeeperSkill
                                                                                   && x.Defending == xmlPlayer.DefenderSkill
                                                                                   && x.Playmaking == xmlPlayer.PlaymakerSkill
                                                                                   && x.Winger == xmlPlayer.WingerSkill
                                                                                   && x.Passing == xmlPlayer.PassingSkill
                                                                                   && x.Scoring == xmlPlayer.ScorerSkill
                                                                                   && x.SetPieces == xmlPlayer.SetPiecesSkill
                                                                                   && x.Experience == xmlPlayer.Experience)
                                                                          .SingleOrDefaultAsync();

            if (seniorPlayerSkill == null)
            {
                seniorPlayerSkill = new Domain.SeniorPlayerSkill
                {
                    UpdatedOn = DateTime.Now,
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
                    SeniorPlayer = seniorPlayer
                };

                await this.seniorPlayerSkillRepository.InsertAsync(seniorPlayerSkill);
            }
        }
    }
}