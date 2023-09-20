namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using Hyperar.HPA.Application.Hattrick.ArenaDetails;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Domain.Interfaces;

    public class ArenaDetails : IXmlFileDataPersisterStrategy
    {
        private readonly IDatabaseContext context;

        private readonly IHattrickRepository<Domain.SeniorTeamArena> seniorTeamArenaRepository;

        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public ArenaDetails(
            IDatabaseContext context,
            IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository,
            IHattrickRepository<Domain.SeniorTeamArena> seniorTeArenaRepository)
        {
            this.context = context;
            this.seniorTeamRepository = seniorTeamRepository;
            this.seniorTeamArenaRepository = seniorTeArenaRepository;
        }

        public void PersistData(IXmlFile file)
        {
            var entity = (HattrickData)file;

            this.ProcessArenaDetails(entity);
        }

        private void ProcessArenaDetails(HattrickData entity)
        {
            var arena = this.seniorTeamArenaRepository.GetByHattrickId(entity.Arena.ArenaId);

            DateTime value = entity.Arena.CurrentCapacity.RebuiltDate != null
                           ? entity.Arena.CurrentCapacity.RebuiltDate.Value
                           : entity.Arena.ExpandedCapacity.ExpansionDate != null
                           ? entity.Arena.ExpandedCapacity.ExpansionDate.Value
                           : DateTime.Now;

            if (arena == null)
            {
                var seniorTeam = this.seniorTeamRepository.GetByHattrickId(entity.Arena.Team.TeamId);

                if (seniorTeam == null)
                {
                    throw new Exception($"Senior Team with Hattrick ID \"{entity.Arena.Team.TeamId}\" not found.");
                }

                arena = new Domain.SeniorTeamArena
                {
                    HattrickId = entity.Arena.ArenaId,
                    Name = entity.Arena.ArenaName,
                    BuiltOn = value,
                    TerracesCapacity = entity.Arena.CurrentCapacity.Terraces,
                    BasicSeatCapacity = entity.Arena.CurrentCapacity.Basic,
                    RoofSeatCapacity = entity.Arena.CurrentCapacity.Roof,
                    VipLoungeCapacity = entity.Arena.CurrentCapacity.Vip,
                    TotalCapacity = entity.Arena.CurrentCapacity.Total,
                    SeniorTeam = seniorTeam
                };

                this.seniorTeamArenaRepository.Insert(arena);
            }
            else
            {
                arena.Name = entity.Arena.ArenaName;
                arena.BuiltOn = value;
                arena.TerracesCapacity = entity.Arena.CurrentCapacity.Terraces;
                arena.BasicSeatCapacity = entity.Arena.CurrentCapacity.Basic;
                arena.RoofSeatCapacity = entity.Arena.CurrentCapacity.Roof;
                arena.VipLoungeCapacity = entity.Arena.CurrentCapacity.Vip;
                arena.TotalCapacity = entity.Arena.CurrentCapacity.Total;

                this.seniorTeamArenaRepository.Update(arena);
            }

            this.context.Save();
        }
    }
}