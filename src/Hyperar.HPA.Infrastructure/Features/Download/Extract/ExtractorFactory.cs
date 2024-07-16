namespace Hyperar.HPA.Infrastructure.Features.Download.Extract
{
    using Application;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Extract.Strategies;
    using Shared.Enums;

    public class ExtractorFactory : IExtractorFactory
    {
        private readonly Avatars avatarsExtractor;

        private readonly CheckToken checkTokenExtractor;

        private readonly Default defaultExtractor;

        private readonly ManagerCompendium managerCompendiumExtractor;

        private readonly MatchArchive matchArchiveExtractor;

        private readonly Matches matchesExtractor;

        private readonly Players playersExtractor;

        private readonly TeamDetails teamDetailsExtractor;

        private readonly WorldDetails worldDetailsExtractor;

        private readonly YouthAvatars youthAvatarsExtractor;

        private readonly YouthPlayerList youthPlayerListExtractor;

        private readonly YouthTeamDetails youthTeamDetailsExtractor;

        public ExtractorFactory(
            Avatars avatarsExtractor,
            CheckToken checkTokenExtractor,
            Default defaultExtractor,
            ManagerCompendium managerCompendiumExtractor,
            MatchArchive matchArchiveExtractor,
            Matches matchesExtractor,
            Players playersExtractor,
            TeamDetails teamDetailsExtractor,
            WorldDetails worldDetailsExtractor,
            YouthAvatars youthAvatarsExtractor,
            YouthPlayerList youthPlayerListExtractor,
            YouthTeamDetails youthTeamDetailsExtractor)
        {
            this.avatarsExtractor = avatarsExtractor;
            this.checkTokenExtractor = checkTokenExtractor;
            this.defaultExtractor = defaultExtractor;
            this.managerCompendiumExtractor = managerCompendiumExtractor;
            this.matchArchiveExtractor = matchArchiveExtractor;
            this.matchesExtractor = matchesExtractor;
            this.playersExtractor = playersExtractor;
            this.teamDetailsExtractor = teamDetailsExtractor;
            this.worldDetailsExtractor = worldDetailsExtractor;
            this.youthAvatarsExtractor = youthAvatarsExtractor;
            this.youthPlayerListExtractor = youthPlayerListExtractor;
            this.youthTeamDetailsExtractor = youthTeamDetailsExtractor;
        }

        public IExtractorStrategy GetExtractor(XmlDownloadTask task)
        {
            return task.FileType switch
            {
                XmlFileType.Avatars => this.avatarsExtractor,
                XmlFileType.CheckToken => this.checkTokenExtractor,
                XmlFileType.ManagerCompendium => this.managerCompendiumExtractor,
                XmlFileType.MatchArchive => this.matchArchiveExtractor,
                XmlFileType.Matches => this.matchesExtractor,
                XmlFileType.Players => this.playersExtractor,
                XmlFileType.TeamDetails => this.teamDetailsExtractor,
                XmlFileType.WorldDetails => this.worldDetailsExtractor,
                XmlFileType.YouthAvatars => this.youthAvatarsExtractor,
                XmlFileType.YouthPlayerList => this.youthPlayerListExtractor,
                XmlFileType.YouthTeamDetails => this.youthTeamDetailsExtractor,
                _ => this.defaultExtractor
            };
        }
    }
}