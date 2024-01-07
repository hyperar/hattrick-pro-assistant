namespace Hyperar.HPA.Application.Interfaces
{
    using Application.Hattrick.Interfaces;

    public interface IXmlEntityFactory
    {
        IXmlFile CreateEntity(string fileName);
    }
}