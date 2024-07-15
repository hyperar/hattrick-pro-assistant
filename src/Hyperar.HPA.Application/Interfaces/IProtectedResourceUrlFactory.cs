namespace Hyperar.HPA.Application.Interfaces
{
    using System.Collections.Specialized;
    using Shared.Enums;

    public interface IProtectedResourceUrlFactory
    {
        string BuildUrl(XmlFileType fileType, NameValueCollection parameters);
    }
}