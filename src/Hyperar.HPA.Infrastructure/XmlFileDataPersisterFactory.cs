namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Application.Interfaces;
    using Common.Enums;
    using Infrastructure.Strategies.XmlFileDataPersister;

    public class XmlFileDataPersisterFactory : IXmlFileDataPersisterFactory
    {
        private readonly ArenaDetails arenaDetailsPersister;

        private readonly Avatars avatarsPersister;

        private readonly ManagerCompendium managerCompendiumPersister;

        private readonly Matches matchesPersister;

        private readonly Players playersPersister;

        private readonly TeamDetails teamDetailsPersister;

        private readonly WorldDetails worldDetailsPersister;

        public XmlFileDataPersisterFactory(
            ArenaDetails arenaDetailsPersister,
            Avatars avatarsPersister,
            ManagerCompendium managerCompendiumPersister,
            Matches matchesPersister,
            Players playersPersister,
            TeamDetails teamDetailsPersister,
            WorldDetails worldDetailsPersister)
        {
            this.arenaDetailsPersister = arenaDetailsPersister;
            this.avatarsPersister = avatarsPersister;
            this.managerCompendiumPersister = managerCompendiumPersister;
            this.matchesPersister = matchesPersister;
            this.playersPersister = playersPersister;
            this.teamDetailsPersister = teamDetailsPersister;
            this.worldDetailsPersister = worldDetailsPersister;
        }

        public IXmlFileDataPersisterStrategy GetPersister(XmlFileType fileType)
        {
            return fileType switch
            {
                XmlFileType.ArenaDetails => this.arenaDetailsPersister,
                XmlFileType.Avatars => this.avatarsPersister,
                XmlFileType.ManagerCompendium => this.managerCompendiumPersister,
                XmlFileType.Matches => this.matchesPersister,
                XmlFileType.Players => this.playersPersister,
                XmlFileType.TeamDetails => this.teamDetailsPersister,
                XmlFileType.WorldDetails => this.worldDetailsPersister,
                _ => throw new ArgumentOutOfRangeException(nameof(fileType))
            };
        }
    }
}