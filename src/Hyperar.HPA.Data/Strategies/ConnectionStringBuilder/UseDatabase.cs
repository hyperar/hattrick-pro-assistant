namespace Hyperar.HPA.Data.Strategies.ConnectionStringBuilder
{
    using Hyperar.HPA.DataContracts;

    public class UseDatabase : IConnectionStringBuilderStrategy
    {
        public string GetConnectionString()
        {
            return $"Data Source=(localdb)\\HPA;Initial Catalog=HPADB;Integrated Security=True;";
        }
    }
}