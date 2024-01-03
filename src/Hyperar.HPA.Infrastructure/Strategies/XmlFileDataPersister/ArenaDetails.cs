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

        public async Task PersistDataAsync(IXmlFile file)
        {
            var entity = (HattrickData)file;

            await this.ProcessArenaDetailsAsync(entity);
        }

        private async Task ProcessArenaDetailsAsync(HattrickData entity)
        {
            var arena = await this.seniorTeamArenaRepository.GetByHattrickIdAsync(entity.Arena.ArenaId);

            DateTime value = entity.Arena.CurrentCapacity.RebuiltDate != null
                           ? entity.Arena.CurrentCapacity.RebuiltDate.Value
                           : entity.Arena.ExpandedCapacity.ExpansionDate != null
                           ? entity.Arena.ExpandedCapacity.ExpansionDate.Value
                           : DateTime.Now;

            if (arena == null)
            {
                var seniorTeam = await this.seniorTeamRepository.GetByHattrickIdAsync(entity.Arena.Team.TeamId);

                if (seniorTeam != null)
                {
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

                    await this.seniorTeamArenaRepository.InsertAsync(arena);
                }
                else
                {
                    throw new Exception($"Senior Team with Hattrick ID \"{entity.Arena.Team.TeamId}\" not found.");
                }
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

            await this.context.SaveAsync();
        }
    }
}