namespace Hyperar.HPA.Application.Interfaces
{
    using Shared.Models.UI.Download;

    public interface IExtractorStrategy
    {
        Task ExtractAsync(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            CancellationToken cancellationToken);
    }
}