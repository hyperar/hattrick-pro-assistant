namespace Hyperar.HPA.UserInterface
{
    using System.IO;
    using System.Windows;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var config = this.SetUpConfiguration();
            var serviceProvider = this.SetUpDependencies(config);

            var window = serviceProvider.GetRequiredService<MainWindow>();

            window.Show();

            base.OnStartup(e);
        }

        private IConfigurationRoot SetUpConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<App>()
#if DEBUG
                .AddJsonFile("appsettings.Debug.json", optional: false);
#else
                .AddJsonFile("appsettings.json", optional: false);
#endif

            return builder.Build();
        }

        private ServiceProvider SetUpDependencies(IConfigurationRoot configuration)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(configuration);

            // Register database objects.
            serviceCollection.AddSingleton<Data.Strategies.ConnectionStringBuilder.AttachDatabaseFile>();
            serviceCollection.AddSingleton<Data.Strategies.ConnectionStringBuilder.UseDatabase>();
            serviceCollection.AddSingleton<DataContracts.IConnectionStringBuilderFactory, Data.Factories.ConnectionStringBuilder>();
            serviceCollection.AddDbContext<DataContracts.IDatabaseContext, Data.DatabaseContext>();
            serviceCollection.AddScoped(typeof(DataContracts.IRepository<>), typeof(Data.Repository<>));
            serviceCollection.AddScoped(typeof(DataContracts.IHattrickRepository<>), typeof(Data.HattrickRepository<>));

            // Register business objects.
            serviceCollection.AddTransient<BusinessContracts.IHattrickClient, Business.HattrickClient>();
            serviceCollection.AddSingleton<BusinessContracts.IProtectedResourceUrlBuilder, Business.ProtectedResourceUrlBuilder>();

            // Register windows.
            serviceCollection.AddTransient(typeof(MainWindow));

            return serviceCollection.BuildServiceProvider();
        }
    }
}
