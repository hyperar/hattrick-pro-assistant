namespace Hyperar.HPA.UI.HostBuilders
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public static class AddConfigurationHostBuilderExtensions
    {
#if DEBUG
        private const string configurationFile = "appSettings.debug.json";

#else
        private const string configurationFile = "appSettings.json";
#endif

        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                configurationBuilder.AddUserSecrets<App>();
                configurationBuilder.AddJsonFile(configurationFile);
            });

            return host;
        }
    }
}