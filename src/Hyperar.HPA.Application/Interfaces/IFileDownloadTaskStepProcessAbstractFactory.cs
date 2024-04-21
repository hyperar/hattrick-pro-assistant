namespace Hyperar.HPA.Application.Interfaces
{
    public interface IFileDownloadTaskStepProcessAbstractFactory
    {
        IFileDownloadTaskStepProcessStrategy GetDownloadTaskStepProcess(IFileDownloadTask fileDownloadTask);
    }
}