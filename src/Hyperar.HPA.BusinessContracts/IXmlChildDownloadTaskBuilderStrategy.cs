namespace Hyperar.HPA.BusinessContracts
{
    using System.Collections.Generic;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Hattrick;

    public interface IXmlChildDownloadTaskBuilderStrategy
    {
        List<DownloadTask>? BuildChildDownloadTaskList(XmlFileBase xmlFile);
    }
}
