namespace Hyperar.HPA.Application.Interfaces
{
    public interface IXmlFileDownloadTaskParseStepProcessFactory
    {
        IFileDownloadTaskStepProcessStrategy GetParser(IFileDownloadTask fileDownloadTask);
    }
}