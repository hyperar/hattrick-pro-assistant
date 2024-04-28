namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Application.Interfaces;
    using Shared.Enums;
    using Strategies.FileDownloadTaskStepProcess.Extractor;

    public class XmlFileDownloadTaskExtractStepProcessFactory : IXmlFileDownloadTaskExtractStepProcessFactory
    {
        private readonly ArenaDetails arenaDetailsExtractor;

        private readonly Avatars avatarsExtractor;

        private readonly CheckToken checkTokenExtractor;

        private readonly Default defaultExtractor;

        private readonly ManagerCompendium managerCompendiumExtractor;

        private readonly Matches matchesExtractor;

        private readonly StaffAvatars staffAvatarsExtractor;

        private readonly TeamDetails teamDetailsExtractor;

        private readonly WorldDetails worldDetailsExtractor;

        public XmlFileDownloadTaskExtractStepProcessFactory(
            ArenaDetails arenaDetailsExtractor,
            Avatars avatarsExtractor,
            CheckToken checkTokenExtractor,
            Default defaultExtractor,
            ManagerCompendium managerCompendiumExtractor,
            Matches matchesExtractor,
            StaffAvatars staffAvatarsExtractor,
            TeamDetails teamDetailsExtractor,
            WorldDetails worldDetailsExtractor)
        {
            this.arenaDetailsExtractor = arenaDetailsExtractor;
            this.avatarsExtractor = avatarsExtractor;
            this.checkTokenExtractor = checkTokenExtractor;
            this.defaultExtractor = defaultExtractor;
            this.managerCompendiumExtractor = managerCompendiumExtractor;
            this.matchesExtractor = matchesExtractor;
            this.staffAvatarsExtractor = staffAvatarsExtractor;
            this.teamDetailsExtractor = teamDetailsExtractor;
            this.worldDetailsExtractor = worldDetailsExtractor;
        }

        public IFileDownloadTaskStepProcessStrategy GetExtractor(IFileDownloadTask fileDownloadTask)
        {
            return fileDownloadTask is IXmlFileDownloadTask xmlFileDownloadTask
                 ? (IFileDownloadTaskStepProcessStrategy)(xmlFileDownloadTask.XmlFileType switch
                 {
                     XmlFileType.ArenaDetails => this.arenaDetailsExtractor,
                     XmlFileType.Avatars => this.avatarsExtractor,
                     XmlFileType.CheckToken => this.checkTokenExtractor,
                     XmlFileType.ManagerCompendium => this.managerCompendiumExtractor,
                     XmlFileType.Matches => this.matchesExtractor,
                     XmlFileType.StaffAvatars => this.staffAvatarsExtractor,
                     XmlFileType.TeamDetails => this.teamDetailsExtractor,
                     XmlFileType.WorldDetails => this.worldDetailsExtractor,
                     _ => this.defaultExtractor
                 })
                 : throw new ArgumentException(Globalization.Translations.UnexpectedFileDownloadTaskType);
        }
    }
}