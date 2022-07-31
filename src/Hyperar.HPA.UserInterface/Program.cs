namespace Hyperar.HPA.UserInterface
{
    using Microsoft.Extensions.DependencyInjection;

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var serviceProvider = RegisterDependencies();

            var context = serviceProvider.GetRequiredService<Data.DatabaseContext>();

            var managerRepository = serviceProvider.GetRequiredService<Data.Interfaces.IHattrickRepository<Domain.Manager>>();

            if (managerRepository.GetById(1) == null)
            {
                managerRepository.Insert(new Domain.Manager
                {
                    HattrickId = 123456,
                    UserName = "test"
                });

                context.Save();
            }

            Application.Run(serviceProvider.GetRequiredService<Forms.FormMain>());
        }

        private static ServiceProvider RegisterDependencies()
        {
            var serviceCollection = new ServiceCollection();

            // Register database objects.
            serviceCollection.AddSingleton<Data.Strategies.ConnectionStringBuilder.AttachDatabaseFile>();
            serviceCollection.AddSingleton<Data.Strategies.ConnectionStringBuilder.UseDatabase>();

            serviceCollection.AddSingleton<Data.Interfaces.IConnectionStringBuilderFactory, Data.Factories.ConnectionStringBuilder>();

            serviceCollection.AddDbContext<Data.Interfaces.IDatabaseContext, Data.DatabaseContext>();

            serviceCollection.AddScoped(typeof(Data.Interfaces.IRepository<>), typeof(Data.Repository<>));
            serviceCollection.AddScoped(typeof(Data.Interfaces.IHattrickRepository<>), typeof(Data.HattrickRepository<>));

            // Registers forms.
            serviceCollection.AddTransient<Forms.FormMain>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}