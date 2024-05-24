namespace Hyperar.HPA.Application.Interfaces
{
    public interface IXmlFileDownloadTaskPersistStepProcessFactory
    {
        IFileDownloadTaskStepProcessStrategy GetPersister(IFileDownloadTask fileDownloadTask);
    }
}