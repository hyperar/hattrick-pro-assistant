namespace Hyperar.HPA.Application.Interfaces
{
    using System.Collections.Generic;
    using Application.Hattrick.Interfaces;
    using Application.Models;

    public interface IXmlDownloadTaskExtractorStrategy
    {
        List<DownloadTask>? ExtractXmlDownloadTasks(IXmlFile xmlFile);
    }
}