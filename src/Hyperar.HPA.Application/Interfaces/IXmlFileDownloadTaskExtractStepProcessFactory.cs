namespace Hyperar.HPA.Application.Interfaces
{
    public interface IXmlFileDownloadTaskExtractStepProcessFactory
    {
        IFileDownloadTaskStepProcessStrategy GetExtractor(IFileDownloadTask fileDownloadTask);
    }
}