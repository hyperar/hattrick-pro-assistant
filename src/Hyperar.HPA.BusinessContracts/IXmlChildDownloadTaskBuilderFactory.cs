namespace Hyperar.HPA.BusinessContracts
{
    using Hyperar.HPA.Common.Enums;

    public interface IXmlChildDownloadTaskBuilderFactory
    {
        IXmlChildDownloadTaskBuilderStrategy CreateChildTaskBuilder(XmlFileType fileType);
    }
}
