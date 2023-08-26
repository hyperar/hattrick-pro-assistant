namespace Hyperar.HPA.BusinessContracts
{
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Hattrick;

    public interface IXmlFileService
    {
        XmlFileBase ParseFile(Stream fileStream);

        XmlFileBase ParseFile(string fileContent);

        List<DownloadTask>? GetChildDownloadTaskList(XmlFileBase xmlFile);
    }
}
