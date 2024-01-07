namespace Hyperar.HPA.Application.Interfaces
{
    using Common.Enums;

    public interface IXmlFileDataPersisterFactory
    {
        IXmlFileDataPersisterStrategy GetPersister(XmlFileType fileType);
    }
}