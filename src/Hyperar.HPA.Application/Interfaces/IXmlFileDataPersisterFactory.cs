namespace Hyperar.HPA.Application.Interfaces
{
    using Hyperar.HPA.Common.Enums;

    public interface IXmlFileDataPersisterFactory
    {
        IXmlFileDataPersisterStrategy GetPersister(XmlFileType fileType);
    }
}