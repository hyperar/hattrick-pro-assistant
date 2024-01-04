namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister;

    public class XmlFileDataPersisterFactory : IXmlFileDataPersisterFactory
    {
        private readonly ArenaDetails arenaDetailsPersister;

        private readonly ManagerCompendium managerCompendiumPersister;

        private readonly Players playersPersister;

        private readonly TeamDetails teamDetailsPersister;

        private readonly WorldDetails worldDetailsPersister;

        public XmlFileDataPersisterFactory(
            ArenaDetails arenaDetailsPersister,
            ManagerCompendium managerCompendiumPersister,
            Players playersPersister,
            TeamDetails teamDetailsPersister,
            WorldDetails worldDetailsPersister)
        {
            this.arenaDetailsPersister = arenaDetailsPersister;
            this.managerCompendiumPersister = managerCompendiumPersister;
            this.playersPersister = playersPersister;
            this.teamDetailsPersister = teamDetailsPersister;
            this.worldDetailsPersister = worldDetailsPersister;
        }

        public IXmlFileDataPersisterStrategy GetPersister(XmlFileType fileType)
        {
            return fileType switch
            {
                XmlFileType.ArenaDetails => this.arenaDetailsPersister,
                XmlFileType.ManagerCompendium => this.managerCompendiumPersister,
                XmlFileType.Players => this.playersPersister,
                XmlFileType.TeamDetails => this.teamDetailsPersister,
                XmlFileType.WorldDetails => this.worldDetailsPersister,
                _ => throw new ArgumentOutOfRangeException(nameof(fileType))
            };
        }
    }
}