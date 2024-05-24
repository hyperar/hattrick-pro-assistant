namespace Hyperar.HPA.WinUI.ExtensionMethods.HostBuilder
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public static class Configuration
    {
        private const string baseConfigurationFile = "appSettings.json";

        private const string developmentConfigurationFile = "appSettings.Development.json";

        private const string productionConfigurationFile = "appSettings.Production.json";

        public static IHostBuilder RegisterConfiguration(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                configurationBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                configurationBuilder.AddJsonFile(baseConfigurationFile);
                configurationBuilder.AddUserSecrets<App>();

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