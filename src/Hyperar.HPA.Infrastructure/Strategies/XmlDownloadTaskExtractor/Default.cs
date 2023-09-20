namespace Hyperar.HPA.Infrastructure.Strategies.XmlDownloadTaskExtractor
{
    using System.Collections.Generic;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Application.OAuth;

    public class Default : IXmlDownloadTaskExtractorStrategy
    {
        public List<DownloadTask>? ExtractXmlDownloadTasks(IXmlFile xmlFile)
        {
            return null;
        }
    }
}