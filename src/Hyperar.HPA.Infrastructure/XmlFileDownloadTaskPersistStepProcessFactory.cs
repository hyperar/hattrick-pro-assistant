namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Application.Interfaces;
    using Shared.Enums;
    using Strategies.FileDownloadTaskStepProcess.Persister;

    public class XmlFileDownloadTaskPersistStepProcessFactory : IXmlFileDownloadTaskPersistStepProcessFactory
    {
        private readonly ArenaDetails arenaDetailsPersister;

        private readonly Avatars avatarsPersister;

        private readonly Club clubPersister;

        private readonly Default defaultPersister;

        private readonly HallOfFamePlayers hallOfFamePlayersPersister;

        private readonly ManagerCompendium managerCompendiumPersister;

        private readonly MatchDetails matchDetailsPersister;

        private readonly MatchLineUp matchLineUpPersister;

        private readonly PlayerDetails playerDetailsPersister;

        private readonly Players playersPersister;

        private readonly StaffAvatars staffAvatarsPersister;

        private readonly StaffList staffListPersister;

        private readonly TeamDetails teamDetailsPersister;

        private readonly WorldDetails worldDetailsPersister;

        public XmlFileDownloadTaskPersistStepProcessFactory(
            ArenaDetails arenaDetailsPersister,
            Avatars avatarsPersister,
            Club clubPersister,
            Default defaultPersister,
            HallOfFamePlayers hallOfFamePlayersPersister,
            ManagerCompendium managerCompendiumPersister,
            MatchDetails matchDetailsPersister,
            MatchLineUp matchLineUpPersister,
            Players playersPersister,
            PlayerDetails playerDetailsPersister,
            StaffAvatars staffAvatarsPersister,
            StaffList staffListPersister,
            TeamDetails teamDetailsPersister,
            WorldDetails worldDetailsPersister)
        {
            this.arenaDetailsPersister = arenaDetailsPersister;
            this.avatarsPersister = avatarsPersister;
            this.clubPersister = clubPersister;
            this.defaultPersister = defaultPersister;
            this.hallOfFamePlayersPersister = hallOfFamePlayersPersister;
            this.managerCompendiumPersister = managerCompendiumPersister;
            this.matchDetailsPersister = matchDetailsPersister;
            this.matchLineUpPersister = matchLineUpPersister;
            this.playersPersister = playersPersister;
            this.playerDetailsPersister = playerDetailsPersister;
            this.staffAvatarsPersister = staffAvatarsPersister;
            this.staffListPersister = staffListPersister;
            this.teamDetailsPersister = teamDetailsPersister;
            this.worldDetailsPersister = worldDetailsPersister;
        }

        public IFileDownloadTaskStepProcessStrategy GetPersister(IFileDownloadTask fileDownloadTask)
        {
            return fileDownloadTask is IXmlFileDownloadTask xmlFileDownloadTask
                 ? (IFileDownloadTaskStepProcessStrategy)(xmlFileDownloadTask.XmlFileType switch
                 {
                     XmlFileType.ArenaDetails => this.arenaDetailsPersister,
                     XmlFileType.Avatars => this.avatarsPersister,
                     XmlFileType.Club => this.clubPersister,
                     XmlFileType.HallOfFamePlayers => this.hallOfFamePlayersPersister,
                     XmlFileType.ManagerCompendium => this.managerCompendiumPersister,
                     XmlFileType.MatchDetails => this.matchDetailsPersister,
                     XmlFileType.MatchLineUp => this.matchLineUpPersister,
                     XmlFileType.Players => this.playersPersister,
                     XmlFileType.PlayerDetails => this.playerDetailsPersister,
                     XmlFileType.StaffAvatars => this.staffAvatarsPersister,
                     XmlFileType.StaffList => this.staffListPersister,
                     XmlFileType.TeamDetails => this.teamDetailsPersister,
                     XmlFileType.WorldDetails => this.worldDetailsPersister,
                     _ => this.defaultPersister
                 })
                 : throw new ArgumentException(Globalization.Translations.UnexpectedFileDownloadTaskType);
        }
    }
}