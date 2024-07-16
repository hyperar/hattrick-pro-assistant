namespace Hyperar.HPA.Application
{
    using System.Collections.Specialized;
    using Shared.Enums;
    using Shared.Models.Hattrick.Interfaces;

    public class XmlDownloadTask : DownloadTaskBase
    {
        public XmlDownloadTask(XmlFileType fileType, long? contextId = null, NameValueCollection? parameters = null)
            : base(
                  Globalization.Translations.ResourceManager.GetString(
                      $"XmlFileType_{fileType}") ?? $"%XmlFileType_{fileType}%")
        {
            this.FileType = fileType;
            this.ChildImageTaskList = new List<ImageDownloadTask>();
            this.ContextId = contextId;
            this.Parameters = parameters ?? new NameValueCollection();
            this.Type = DownloadTaskType.XmlFile;
        }

        public ICollection<ImageDownloadTask> ChildImageTaskList { get; set; }

        public override int CompletedCount
        {
            get
            {
                return base.CompletedCount + this.ChildImageTaskList.Sum(x => x.CompletedCount);
            }
        }

        public long? ContextId { get; set; }

        public override int Count
        {
            get
            {
                return base.Count + this.ChildImageTaskList.Sum(x => x.Count);
            }
        }

        public XmlFileType FileType { get; set; }

        public IList<DownloadTaskBase> FlattenedTaskList
        {
            get
            {
                return new List<DownloadTaskBase> { this }
                    .Concat(this.ChildImageTaskList)
                    .ToList();
            }
        }

        public bool HasPendingChildTasks
        {
            get
            {
                return this.ChildImageTaskList.Any(x => x.Status != DownloadTaskStatus.Finished);
            }
        }

        public NameValueCollection Parameters { get; set; }

        public string? Response { get; set; }

        public IXmlFile? XmlFile { get; set; }
    }
}