namespace Hyperar.HPA.Application.Interfaces
{
    using System.Collections.Generic;
    using Common.Enums;

    public interface IProtectedResourceUrlBuilder
    {
        string BuildUrl(XmlFileType fileType, Dictionary<string, string>? parameters);
    }
}