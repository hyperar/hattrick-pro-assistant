namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Hattrick.TeamDetails;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Interfaces;

    public class TeamDetails : IXmlFileDataPersisterStrategy
    {
        private readonly IDatabaseContext context;

        private readonly IHattrickRepository<Domain.League> leagueRepository;

        private readonly IHattrickRepository<Domain.Manager> managerRepository;

        private readonly IHattrickRepository<Domain.Region> regionRepository;

        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public TeamDetails(
            IDatabaseContext context,
            IHattrickRepository<Domain.League> leagueRepository,
            IHattrickRepository<Manager> managerRepository,
            IHattrickRepository<Domain.Region> regionRepository,
            IHattrickRepository<SeniorTeam> seniorTeamRepository)
        {
            this.context = context;
            this.leagueRepository = leagueRepository;
            this.managerRepository = managerRepository;
            this.regionRepository = regionRepository;
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public void PersistData(IXmlFile file)
        {
            var entity = (HattrickData)file;

            this.ProcessTeamDetails(entity);
        }

        private void ProcessTeam(Team xmlTeam, Manager manager)
        {
            var seniorTeam = this.seniorTeamRepository.GetByHattrickId(xmlTeam.TeamId);

            var league = this.leagueRepository.GetByHattrickId(xmlTeam.League.LeagueId);

            if (league != null)
            {
                var region = this.regionRepository.GetByHattrickId(xmlTeam.Region.RegionId);

                if (region != null)
                {
                    if (seniorTeam == null)
                    {
                        seniorTeam = new Domain.SeniorTeam
                        {
                            HattrickId = xmlTeam.TeamId,
                            Name = xmlTeam.TeamName,
                            ShortName = xmlTeam.ShortTeamName,
                            IsPrimary = xmlTeam.IsPrimaryClub,
                            FoundedOn = xmlTeam.FoundedDate,
                            CoachPlayerId = xmlTeam.Trainer.PlayerId,
                            IsPlayingCup = xmlTeam.Cup != null && xmlTeam.Cup.StillInCup,
                            GlobalRanking = xmlTeam.PowerRating.GlobalRanking,
                            LeagueRanking = xmlTeam.PowerRating.LeagueRanking,
                            RegionRanking = xmlTeam.PowerRating.RegionRanking,
                            PowerRanking = xmlTeam.PowerRating.PowerRating,
                            TeamRank = xmlTeam.TeamRank ?? 0,
                            NumberOfConsecutiveUndefeatedMatches = xmlTeam.NumberOfUndefeated ?? 0,
                            NumberOfConsecutiveWonMatches = xmlTeam.NumberOfVictories ?? 0,
                            League = league,
                            Manager = manager,
                            Region = region
                        };

                        this.seniorTeamRepository.Insert(seniorTeam);
                    }
                    else
                    {
                        seniorTeam.HattrickId = xmlTeam.TeamId;
                        seniorTeam.Name = xmlTeam.TeamName;
                        seniorTeam.ShortName = xmlTeam.ShortTeamName;
                        seniorTeam.IsPrimary = xmlTeam.IsPrimaryClub;
                        seniorTeam.FoundedOn = xmlTeam.FoundedDate;
                        seniorTeam.CoachPlayerId = xmlTeam.Trainer.PlayerId;
                        seniorTeam.IsPlayingCup = xmlTeam.Cup != null && xmlTeam.Cup.StillInCup;
                        seniorTeam.GlobalRanking = xmlTeam.PowerRating.GlobalRanking;
                        seniorTeam.LeagueRanking = xmlTeam.PowerRating.LeagueRanking;
                        seniorTeam.RegionRanking = xmlTeam.PowerRating.RegionRanking;
                        seniorTeam.PowerRanking = xmlTeam.PowerRating.PowerRating;
                        seniorTeam.TeamRank = xmlTeam.TeamRank ?? 0;
                        seniorTeam.NumberOfConsecutiveUndefeatedMatches = xmlTeam.NumberOfUndefeated ?? 0;
                        seniorTeam.NumberOfConsecutiveWonMatches = xmlTeam.NumberOfVictories ?? 0;
                        seniorTeam.Region = region;
                    }
                }
                else
                {
                    throw new Exception($"Region with Hattrick ID \"{xmlTeam.Region.RegionId}\" not found.");
                }
            }
            else
            {
                throw new Exception($"League with Hattrick ID \"{xmlTeam.League.LeagueId}\" not found.");
            }
        }

        private void ProcessTeamDetails(HattrickData entity)
        {
            var manager = this.managerRepository.GetByHattrickId(entity.User.UserId);

            if (manager != null)
            {
                foreach (var curXmlTeam in entity.Teams)
                {
                    this.ProcessTeam(curXmlTeam, manager);
                }

                this.context.Save();
            }
            else
            {
                throw new Exception($"Manager with Hattrick ID \"{entity.User.UserId}\" not found.");
            }
        }
    }
}