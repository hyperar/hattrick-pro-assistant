namespace Hyperar.HPA.Infrastructure
{
    using Application.Interfaces;
    using Common.Enums;
    using Infrastructure.Strategies.XmlDownloadTaskExtractor;

    public class XmlDownloadTaskExtractorFactory : IXmlDownloadTaskExtractorFactory
    {
        private readonly Default defaultExtractor;

        private readonly ManagerCompendium managerCompendiumExtractor;

        private readonly Matches matchesExtractor;

        private readonly TeamDetails teamDetailsExtractor;

        public XmlDownloadTaskExtractorFactory(
            Default defaultExtractor,
            ManagerCompendium managerCompendiumExtractor,
            Matches matchesExtractor,
            TeamDetails teamDetailsExtractor)
        {
            this.defaultExtractor = defaultExtractor;
            this.managerCompendiumExtractor = managerCompendiumExtractor;
            this.matchesExtractor = matchesExtractor;
            this.teamDetailsExtractor = teamDetailsExtractor;
        }

        public IXmlDownloadTaskExtractorStrategy CreateDownloadTaskExtractor(XmlFileType fileType)
        {
            return fileType switch
            {
                XmlFileType.ManagerCompendium => this.managerCompendiumExtractor,
                XmlFileType.Matches => this.matchesExtractor,
                XmlFileType.TeamDetails => this.teamDetailsExtractor,
                _ => this.defaultExtractor,
            };
        }
    }
}