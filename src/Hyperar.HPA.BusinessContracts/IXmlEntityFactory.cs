namespace Hyperar.HPA.BusinessContracts
{
    using Hyperar.HPA.Domain.Hattrick;

    public interface IXmlEntityFactory
    {
        XmlFileBase CreateEntity(string fileName);
    }
}
