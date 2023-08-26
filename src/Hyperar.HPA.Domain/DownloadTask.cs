namespace Hyperar.HPA.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Domain.Hattrick;

    public class DownloadTask : INotifyPropertyChanged
    {
        private DownloadTaskStatus status;

        public DownloadTask(XmlFileType fileType, Dictionary<string, string>? parameters = null)
        {
            this.FileType = fileType;
            this.Parameters = parameters;
            this.Status = DownloadTaskStatus.Pending;
        }

        public XmlFileType FileType { get; }

        public string FileTypeString
        {
            get
            {
                return this.FileType.ToString();
            }
        }

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
                this.OnPropertyChanged(nameof(this.StatusString));
            }
        }

        public string StatusString
        {
            get
            {
                return this.Status.ToString();
            }
        }

        public string? Response { get; set; }

        public XmlFileBase? ParsedEntity { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
