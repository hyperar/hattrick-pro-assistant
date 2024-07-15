namespace Hyperar.HPA.Application.Interfaces
{
    using Shared.Models.UI.Download;

    public interface IDownloadRequestFactory
    {
        IDownloadRequest<DownloadTaskBase> Create(
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress);
    }
}