namespace Hyperar.HPA.Application.Interfaces
{
    using Hyperar.HPA.Application.Hattrick.Interfaces;

    public interface IXmlEntityFactory
    {
        IXmlFile CreateEntity(string fileName);
    }
}