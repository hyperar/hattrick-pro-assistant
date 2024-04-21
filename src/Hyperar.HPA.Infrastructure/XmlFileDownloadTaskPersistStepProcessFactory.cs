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

        private readonly Default defaultPersister;

        private readonly HallOfFamePlayers hallOfFamePlayersPersister;

        private readonly ManagerCompendium managerCompendiumPersister;

        private readonly MatchDetails matchDetailsPersister;

        private readonly MatchLineUp matchLineUpPersister;

        private readonly Players playersPersister;

        private readonly TeamDetails teamDetailsPersister;

        private readonly WorldDetails worldDetailsPersister;

        public XmlFileDownloadTaskPersistStepProcessFactory(
            ArenaDetails arenaDetailsPersister,
            Avatars avatarsPersister,
            Default defaultPersister,
            HallOfFamePlayers hallOfFamePlayersPersister,
            ManagerCompendium managerCompendiumPersister,
            MatchDetails matchDetailsPersister,
            MatchLineUp matchLineUpPersister,
            Players playersPersister,
            TeamDetails teamDetailsPersister,
            WorldDetails worldDetailsPersister)
        {
            this.arenaDetailsPersister = arenaDetailsPersister;
            this.avatarsPersister = avatarsPersister;
            this.defaultPersister = defaultPersister;
            this.hallOfFamePlayersPersister = hallOfFamePlayersPersister;
            this.managerCompendiumPersister = managerCompendiumPersister;
            this.matchDetailsPersister = matchDetailsPersister;
            this.matchLineUpPersister = matchLineUpPersister;
            this.playersPersister = playersPersister;
            this.teamDetailsPersister = teamDetailsPersister;
            this.worldDetailsPersister = worldDetailsPersister;
        }

        public IFileDownloadTaskStepProcessStrategy GetPersister(IFileDownloadTask fileDownloadTask)
        {
            if (fileDownloadTask is IXmlFileDownloadTask xmlFileDownloadTask)
            {
                return xmlFileDownloadTask.XmlFileType switch
                {
                    XmlFileType.ArenaDetails => this.arenaDetailsPersister,
                    XmlFileType.Avatars => this.avatarsPersister,
                    XmlFileType.HallOfFamePlayers => this.hallOfFamePlayersPersister,
                    XmlFileType.ManagerCompendium => this.managerCompendiumPersister,
                    XmlFileType.MatchDetails => this.matchDetailsPersister,
                    XmlFileType.MatchLineUp => this.matchLineUpPersister,
                    XmlFileType.Players => this.playersPersister,
                    XmlFileType.TeamDetails => this.teamDetailsPersister,
                    XmlFileType.WorldDetails => this.worldDetailsPersister,
                    _ => this.defaultPersister
                };
            }
            else
            {
                throw new ArgumentException(Globalization.Translations.UnexpectedFileDownloadTaskType);
            }
        }
    }
}