namespace Hyperar.HPA.Infrastructure
{
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Infrastructure.Strategies.XmlDownloadTaskExtractor;

    public class XmlDownloadTaskExtractorFactory : IXmlDownloadTaskExtractorFactory
    {
        private readonly Default defaultExtractor;

        private readonly ManagerCompendium managerCompendiumExtractor;

        private readonly TeamDetails teamDetailsExtractor;

        public XmlDownloadTaskExtractorFactory(
            Default defaultExtractor,
            ManagerCompendium managerCompendiumExtractor,
            TeamDetails teamDetailsExtractor)
        {
            this.defaultExtractor = defaultExtractor;
            this.managerCompendiumExtractor = managerCompendiumExtractor;
            this.teamDetailsExtractor = teamDetailsExtractor;
        }

        public IXmlDownloadTaskExtractorStrategy CreateDownloadTaskExtractor(XmlFileType fileType)
        {
            return fileType switch
            {
                XmlFileType.ManagerCompendium => this.managerCompendiumExtractor,
                XmlFileType.TeamDetails => this.teamDetailsExtractor,
                _ => this.defaultExtractor,
            };
        }
    }
}