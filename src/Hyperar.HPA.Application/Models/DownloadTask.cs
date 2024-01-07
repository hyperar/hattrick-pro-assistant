namespace Hyperar.HPA.Application.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using Application.Hattrick.Interfaces;
    using Common.Enums;

    public class DownloadTask : INotifyPropertyChanged
    {
        private DownloadTaskStatus status;

        public DownloadTask(XmlFileType fileType, Dictionary<string, string>? parameters = null)
        {
            this.FileType = fileType;
            this.Parameters = parameters;
            this.Status = DownloadTaskStatus.Pending;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public XmlFileType FileType { get; }

        public string FileTypeString
        {
            get
            {
                return this.FileType.ToString();
            }
        }

        public Dictionary<string, string>? Parameters { get; set; }

        public string ParametersString
        {
            get
            {
                if (this.Parameters == null || this.Parameters.Count == 0)
                {
                    return string.Empty;
                }

                var stringBuilder = new StringBuilder();

                foreach (var curParameter in this.Parameters)
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append(", ");
                    }

                    stringBuilder.AppendFormat($"{curParameter.Key} = \"{curParameter.Value}\"");
                }

                return $"({stringBuilder})";
            }
        }

        public IXmlFile? ParsedEntity { get; set; }

        public string? Response { get; set; }

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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}