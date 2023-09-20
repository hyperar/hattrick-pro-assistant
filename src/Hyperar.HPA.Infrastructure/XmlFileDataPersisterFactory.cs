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
            switch (fileType)
            {
                case XmlFileType.ArenaDetails:
                    return this.arenaDetailsPersister;

                case XmlFileType.ManagerCompendium:
                    return this.managerCompendiumPersister;

                case XmlFileType.Players:
                    return this.playersPersister;

                case XmlFileType.TeamDetails:
                    return this.teamDetailsPersister;

                case XmlFileType.WorldDetails:
                    return this.worldDetailsPersister;

                default:
                    throw new NotImplementedException($"No XmlFileDataPersister strategy found for {fileType}.");
            }
        }
    }
}