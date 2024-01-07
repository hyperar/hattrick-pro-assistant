namespace Hyperar.HPA.Infrastructure.Strategies.XmlDownloadTaskExtractor
{
    using System.Collections.Generic;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Application.Models;

    public class Default : IXmlDownloadTaskExtractorStrategy
    {
        public List<DownloadTask>? ExtractXmlDownloadTasks(IXmlFile xmlFile)
        {
            return null;
        }
    }
}