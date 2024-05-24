namespace Hyperar.HPA.Application.Interfaces
{
    using System.Collections.Specialized;
    using Shared.Enums;

    public interface IProtectedResourceUrlBuilder
    {
        string BuildUrl(XmlFileType fileType, NameValueCollection parameters);
    }
}