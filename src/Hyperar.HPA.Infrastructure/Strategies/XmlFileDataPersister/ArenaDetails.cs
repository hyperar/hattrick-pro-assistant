namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Domain.Interfaces;

    using Hattrick = Application.Hattrick.ArenaDetails;

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
                if (file is Hattrick.HattrickData xmlEntity)
                {
                    await this.ProcessArenaDetailsAsync(xmlEntity);
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

        private async Task ProcessArenaDetailsAsync(Hattrick.HattrickData xmlEntity)
        {
            var arena = await this.seniorTeamArenaRepository.GetByHattrickIdAsync(xmlEntity.Arena.ArenaId);

            DateTime value = xmlEntity.Arena.CurrentCapacity.RebuiltDate != null
                           ? xmlEntity.Arena.CurrentCapacity.RebuiltDate.Value
                           : xmlEntity.Arena.ExpandedCapacity.ExpansionDate != null
                           ? xmlEntity.Arena.ExpandedCapacity.ExpansionDate.Value
                           : DateTime.Now;

            if (arena == null)
            {
                var seniorTeam = await this.seniorTeamRepository.GetByHattrickIdAsync(xmlEntity.Arena.Team.TeamId);

                ArgumentNullException.ThrowIfNull(seniorTeam, nameof(seniorTeam));

                arena = new Domain.SeniorTeamArena
                {
                    HattrickId = xmlEntity.Arena.ArenaId,
                    Name = xmlEntity.Arena.ArenaName,
                    BuiltOn = value,
                    TerracesCapacity = xmlEntity.Arena.CurrentCapacity.Terraces,
                    BasicSeatCapacity = xmlEntity.Arena.CurrentCapacity.Basic,
                    RoofSeatCapacity = xmlEntity.Arena.CurrentCapacity.Roof,
                    VipLoungeCapacity = xmlEntity.Arena.CurrentCapacity.Vip,
                    TotalCapacity = xmlEntity.Arena.CurrentCapacity.Total,
                    SeniorTeam = seniorTeam
                };

                await this.seniorTeamArenaRepository.InsertAsync(arena);
            }
            else
            {
                arena.Name = xmlEntity.Arena.ArenaName;
                arena.BuiltOn = value;
                arena.TerracesCapacity = xmlEntity.Arena.CurrentCapacity.Terraces;
                arena.BasicSeatCapacity = xmlEntity.Arena.CurrentCapacity.Basic;
                arena.RoofSeatCapacity = xmlEntity.Arena.CurrentCapacity.Roof;
                arena.VipLoungeCapacity = xmlEntity.Arena.CurrentCapacity.Vip;
                arena.TotalCapacity = xmlEntity.Arena.CurrentCapacity.Total;

                this.seniorTeamArenaRepository.Update(arena);
            }

            await this.databaseContext.SaveAsync();
        }
    }
}