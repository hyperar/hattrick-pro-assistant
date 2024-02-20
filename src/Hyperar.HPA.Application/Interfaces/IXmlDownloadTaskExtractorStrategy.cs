namespace Hyperar.HPA.Application.Interfaces
{
    using Application.Hattrick.Interfaces;
    using Application.Models;

    public interface IXmlDownloadTaskExtractorStrategy
    {
        DownloadTask[] ExtractXmlDownloadTasks(IXmlFile xmlFile);
    }
}