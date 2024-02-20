namespace Hyperar.HPA.Application.Interfaces
{
    using Application.Hattrick.Interfaces;

    public interface IXmlFileDataPersisterStrategy
    {
        Task PersistDataAsync(IXmlFile file);

        Task PersistDataWithContextAsync(IXmlFile file, uint contextId);
    }
}