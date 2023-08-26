namespace Hyperar.HPA.Business.XmlChildDownloadTaskBuilder
{
    using System.Collections.Generic;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Hattrick;

    public class Default : IXmlChildDownloadTaskBuilderStrategy
    {
        public List<DownloadTask>? BuildChildDownloadTaskList(XmlFileBase xmlFile)
        {
            return null;
        }
    }
}
