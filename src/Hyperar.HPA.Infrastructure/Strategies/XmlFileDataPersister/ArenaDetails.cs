namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using Hattrick = Application.Hattrick.ArenaDetails;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Domain.Interfaces;

    public class ArenaDetails : IXmlFileDataPersisterStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.SeniorTeamArena> seniorTeamArenaRepository;

        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public ArenaDetails(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository,
            IHattrickRepository<Domain.SeniorTeamArena> seniorTeArenaRepository)
        {
            this.databaseContext = databaseContext;
            this.seniorTeamRepository = seniorTeamRepository;
            this.seniorTeamArenaRepository = seniorTeArenaRepository;
        }

        public async Task PersistDataAsync(IXmlFile file)
        {
            try
            {
                if (file is Hattrick.HattrickData entity)
                {
                    await this.ProcessArenaDetailsAsync(entity);
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

        private async Task ProcessArenaDetailsAsync(Hattrick.HattrickData entity)
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

                ArgumentNullException.ThrowIfNull(seniorTeam, nameof(seniorTeam));

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
                arena.Name = entity.Arena.ArenaName;
                arena.BuiltOn = value;
                arena.TerracesCapacity = entity.Arena.CurrentCapacity.Terraces;
                arena.BasicSeatCapacity = entity.Arena.CurrentCapacity.Basic;
                arena.RoofSeatCapacity = entity.Arena.CurrentCapacity.Roof;
                arena.VipLoungeCapacity = entity.Arena.CurrentCapacity.Vip;
                arena.TotalCapacity = entity.Arena.CurrentCapacity.Total;

                this.seniorTeamArenaRepository.Update(arena);
            }

            await this.databaseContext.SaveAsync();
        }
    }
}