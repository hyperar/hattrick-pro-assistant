namespace Hyperar.HPA.Application.Interfaces
{
    public interface IFileDownloadTaskStepAdvancerStrategy
    {
        void AdvanceTaskStatus(IFileDownloadTask fileDownloadTask);
    }
}