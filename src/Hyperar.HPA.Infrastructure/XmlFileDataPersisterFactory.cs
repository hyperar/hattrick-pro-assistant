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

        private readonly HallOfFamePlayers hallOfFamePlayersPersister;

        private readonly ManagerCompendium managerCompendiumPersister;

        private readonly MatchDetails matchDetailsPersister;

        private readonly Matches matchesPersister;

        private readonly Players playersPersister;

        private readonly StaffAvatars staffAvatarsPersister;

        private readonly StaffList staffListPersister;

        private readonly TeamDetails teamDetailsPersister;

        private readonly WorldDetails worldDetailsPersister;

        public XmlFileDataPersisterFactory(
            ArenaDetails arenaDetailsPersister,
            Avatars avatarsPersister,
            HallOfFamePlayers hallOfFamePlayersPersister,
            ManagerCompendium managerCompendiumPersister,
            MatchDetails matchDetailsPersister,
            Matches matchesPersister,
            Players playersPersister,
            StaffAvatars staffAvatarsPersister,
            StaffList staffListPersister,
            TeamDetails teamDetailsPersister,
            WorldDetails worldDetailsPersister)
        {
            this.arenaDetailsPersister = arenaDetailsPersister;
            this.avatarsPersister = avatarsPersister;
            this.hallOfFamePlayersPersister = hallOfFamePlayersPersister;
            this.managerCompendiumPersister = managerCompendiumPersister;
            this.matchDetailsPersister = matchDetailsPersister;
            this.matchesPersister = matchesPersister;
            this.playersPersister = playersPersister;
            this.staffAvatarsPersister = staffAvatarsPersister;
            this.staffListPersister = staffListPersister;
            this.teamDetailsPersister = teamDetailsPersister;
            this.worldDetailsPersister = worldDetailsPersister;
        }

        public IXmlFileDataPersisterStrategy GetPersister(XmlFileType fileType)
        {
            return fileType switch
            {
                XmlFileType.ArenaDetails => this.arenaDetailsPersister,
                XmlFileType.Avatars => this.avatarsPersister,
                XmlFileType.HallOfFamePlayers => this.hallOfFamePlayersPersister,
                XmlFileType.ManagerCompendium => this.managerCompendiumPersister,
                XmlFileType.MatchDetails => this.matchDetailsPersister,
                XmlFileType.Matches => this.matchesPersister,
                XmlFileType.Players => this.playersPersister,
                XmlFileType.StaffAvatars => this.staffAvatarsPersister,
                XmlFileType.StaffList => this.staffListPersister,
                XmlFileType.TeamDetails => this.teamDetailsPersister,
                XmlFileType.WorldDetails => this.worldDetailsPersister,
                _ => throw new ArgumentOutOfRangeException(nameof(fileType))
            };
        }
    }
}