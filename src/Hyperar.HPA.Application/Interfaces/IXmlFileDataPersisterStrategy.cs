namespace Hyperar.HPA.Application.Interfaces
{
    using Application.Hattrick.Interfaces;

    public interface IXmlFileDataPersisterStrategy
    {
        Task PersistDataAsync(IXmlFile file);
    }
}