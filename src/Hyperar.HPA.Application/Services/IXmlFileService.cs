namespace Hyperar.HPA.Application.Services
{
    using Application.Hattrick.Interfaces;
    using Application.Models;

    public interface IXmlFileService
    {
        Task BeginPersistSession();

        Task EndPersistSession();

        List<DownloadTask>? ExtractXmlDownloadTasks(IXmlFile xmlFile);

        Task<IXmlFile> ParseFileAsync(Stream fileStream);

        Task<IXmlFile> ParseFileAsync(string fileContent);

        Task PersistFileAsync(IXmlFile xmlFile);
    }
}