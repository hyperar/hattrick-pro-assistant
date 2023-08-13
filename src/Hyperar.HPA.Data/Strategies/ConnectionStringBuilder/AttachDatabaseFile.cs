namespace Hyperar.HPA.Data.Strategies.ConnectionStringBuilder
{
    using System;
    using Hyperar.HPA.DataContracts;

    public class AttachDatabaseFile : IConnectionStringBuilderStrategy
    {
        public string GetConnectionString()
        {
            return $"Data Source=(localdb)\\HPA;AttachDbFilename={this.GetDatabaseFolder()}\\HPADB.mdf;Initial Catalog=HPADB;Integrated Security=True;";
        }

        private string GetDatabaseFolder()
        {
            string databaseFolder = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                Common.Constants.Folders.AppName,
                Common.Constants.Folders.Database);

            if (!Directory.Exists(databaseFolder))
            {
                Directory.CreateDirectory(databaseFolder);
            }

            return databaseFolder;
        }
    }
}