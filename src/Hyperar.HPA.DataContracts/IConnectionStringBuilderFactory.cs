namespace Hyperar.HPA.DataContracts
{
    public interface IConnectionStringBuilderFactory
    {
        IConnectionStringBuilderStrategy GetConnectionStringBuilder();
    }
}