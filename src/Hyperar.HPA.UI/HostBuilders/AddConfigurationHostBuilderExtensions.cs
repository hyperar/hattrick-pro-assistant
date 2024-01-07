namespace Hyperar.HPA.UI.HostBuilders
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public static class AddConfigurationHostBuilderExtensions
    {
        private const string baseConfigurationFile = "appSettings.json";

        private const string developmentConfigurationFile = "appSettings.Development.json";

        private const string productionConfigurationFile = "appSettings.Production.json";

        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                configurationBuilder.AddUserSecrets<App>();
                configurationBuilder.AddJsonFile(baseConfigurationFile);

                if (context.HostingEnvironment.IsDevelopment())
                {
                    configurationBuilder.AddJsonFile(developmentConfigurationFile);
                }
                else if (context.HostingEnvironment.IsProduction())
                {
                    configurationBuilder.AddJsonFile(productionConfigurationFile);
                }
            });

            return host;
        }
    }
}