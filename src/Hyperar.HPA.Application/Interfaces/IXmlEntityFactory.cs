namespace Hyperar.HPA.Application.Interfaces
{
    using Shared.Models.Hattrick.Interfaces;

    public interface IXmlEntityFactory
    {
        IXmlFile CreateEntity(string fileName);
    }
}