namespace Hyperar.HPA.Application.Interfaces
{
    using Hyperar.HPA.Application.Hattrick.Interfaces;

    public interface IXmlFileDataPersisterStrategy
    {
        void PersistData(IXmlFile file);
    }
}