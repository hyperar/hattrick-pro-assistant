namespace Hyperar.HPA.Application.Models
{
    using System.Collections.Specialized;
    using Application.Interfaces;
    using Shared.Enums;
    using Shared.Models.Hattrick.Interfaces;

    public class XmlFileDownloadTask : FileDownloadTaskBase, IFileDownloadTask, IXmlFileDownloadTask
    {
        public XmlFileDownloadTask(XmlFileType xmlFileType, long? contextId = null, NameValueCollection? parameters = null)
            : base(
                  DownloadTaskType.XmlFile,
                  Globalization.Translations.ResourceManager.GetString(
                      $"{xmlFileType.GetType().Name}_{xmlFileType}") ?? string.Empty)
        {
            this.XmlFileType = xmlFileType;
            this.ContextId = contextId;
            this.Parameters = parameters ?? new NameValueCollection();
        }

        public long? ContextId { get; }

        public NameValueCollection Parameters { get; set; }

        public string? Response { get; set; }

        public IXmlFile? XmlFile { get; set; }

        public XmlFileType XmlFileType { get; }
    }
}