namespace Hyperar.HPA.Application
{
    using Shared.Enums;

    public abstract class DownloadTaskBase
    {
        protected DownloadTaskBase(string title)
        {
            this.Title = title;
            this.Status = DownloadTaskStatus.Pending;
        }

        public virtual int CompletedCount
        {
            get
            {
                return this.Status == DownloadTaskStatus.Finished ? 1 : 0;
            }
        }

        public virtual int Count
        {
            get
            {
                return 1;
            }
        }

        public DownloadTaskStatus Status { get; set; }

        public string Title { get; }

        public DownloadTaskType Type { get; protected set; }
    }
}