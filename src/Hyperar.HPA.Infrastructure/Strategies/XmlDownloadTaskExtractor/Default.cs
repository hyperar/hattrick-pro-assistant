namespace Hyperar.HPA.Infrastructure.Strategies.XmlDownloadTaskExtractor
{
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Application.Models;

    public class Default : IXmlDownloadTaskExtractorStrategy
    {
        public DownloadTask[] ExtractXmlDownloadTasks(IXmlFile xmlFile)
        {
            return Array.Empty<DownloadTask>();
        }
    }
}