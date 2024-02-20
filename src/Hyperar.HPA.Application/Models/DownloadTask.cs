namespace Hyperar.HPA.Application.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Common.Enums;

    public class DownloadTask : INotifyPropertyChanged
    {
        private DownloadTaskStatus status;

        public DownloadTask(XmlFileType fileType)
        {
            this.FileType = fileType;
            this.Status = DownloadTaskStatus.Pending;
        }

        public DownloadTask(XmlFileType fileType, Dictionary<string, string>? parameters = null)
        {
            this.FileType = fileType;
            this.Parameters = parameters;
            this.Status = DownloadTaskStatus.Pending;
        }

        public DownloadTask(XmlFileType fileType, uint contextId, Dictionary<string, string>? parameters = null)
        {
            this.FileType = fileType;
            this.ContextId = contextId;
            this.Parameters = parameters;
            this.Status = DownloadTaskStatus.Pending;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public uint? ContextId { get; }

        public XmlFileType FileType { get; }

        public Dictionary<string, string>? Parameters { get; set; }

        public DownloadTaskStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;

                this.PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(
                        nameof(
                            this.Status)));
            }
        }
    }
}