namespace Hyperar.HPA.Application.Interfaces
{
    public interface IFileDownloadTaskStepAdvancerFactory
    {
        IFileDownloadTaskStepAdvancerStrategy GetAdvancer(IFileDownloadTask fileDownloadTask);
    }
}