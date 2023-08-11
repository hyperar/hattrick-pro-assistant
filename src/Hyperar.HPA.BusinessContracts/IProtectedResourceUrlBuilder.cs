namespace Hyperar.HPA.BusinessContracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HPA.Common.Enums;

    public interface IProtectedResourceUrlBuilder
    {
        string BuildUrl(XmlFileType fileType, Dictionary<string, string> parameters);
    }
}
