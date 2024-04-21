namespace Hyperar.HPA.Application.Interfaces
{
    using System.Collections.Specialized;
    using Shared.Enums;
    using Shared.Models.Hattrick.Interfaces;

    public interface IXmlFileDownloadTask : IFileDownloadTask
    {
        long? ContextId { get; }

        NameValueCollection Parameters { get; }

        string? Response { get; set; }

        IXmlFile? XmlFile { get; set; }

        XmlFileType XmlFileType { get; }
    }
}