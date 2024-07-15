namespace Hyperar.HPA.Application.Interfaces
{
    using MediatR;
    using Shared.Models.UI.Download;

    public interface IDownloadRequest<TTask> : IRequest where TTask : DownloadTaskBase
    {
        IProgress<ProcessReport> Progress { get; }

        TTask Task { get; }

        IList<XmlDownloadTask> TaskList { get; }
    }
}