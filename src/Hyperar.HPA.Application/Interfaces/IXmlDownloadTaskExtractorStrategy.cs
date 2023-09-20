namespace Hyperar.HPA.Application.Interfaces
{
    using System.Collections.Generic;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.OAuth;

    public interface IXmlDownloadTaskExtractorStrategy
    {
        List<DownloadTask>? ExtractXmlDownloadTasks(IXmlFile xmlFile);
    }
}