namespace Hyperar.HPA.Application.Services
{
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.OAuth;

    public interface IXmlFileService
    {
        List<DownloadTask>? ExtractXmlDownloadTasks(IXmlFile xmlFile);

        Task<IXmlFile> ParseFileAsync(Stream fileStream);

        Task<IXmlFile> ParseFileAsync(string fileContent);

        Task PersistAsync(IXmlFile xmlFile);
    }
}