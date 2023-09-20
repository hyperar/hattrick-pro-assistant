namespace Hyperar.HPA.Application.Services
{
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.OAuth;

    public interface IXmlFileService
    {
        List<DownloadTask>? ExtractXmlDownloadTasks(IXmlFile xmlFile);

        IXmlFile ParseFile(Stream fileStream);

        IXmlFile ParseFile(string fileContent);

        void Persist(IXmlFile xmlFile);
    }
}