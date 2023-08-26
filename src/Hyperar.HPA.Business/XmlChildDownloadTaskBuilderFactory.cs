namespace Hyperar.HPA.Business
{
    using Hyperar.HPA.Business.XmlChildDownloadTaskBuilder;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Common.Enums;

    public class XmlChildDownloadTaskBuilderFactory : IXmlChildDownloadTaskBuilderFactory
    {
        public IXmlChildDownloadTaskBuilderStrategy CreateChildTaskBuilder(XmlFileType fileType)
        {
            switch (fileType)
            {
                case XmlFileType.ManagerCompendium:
                    return new ManagerCompendium();

                default:
                    return new Default();
            }
        }
    }
}
