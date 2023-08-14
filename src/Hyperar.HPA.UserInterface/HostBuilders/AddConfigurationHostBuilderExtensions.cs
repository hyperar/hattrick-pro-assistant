namespace Hyperar.HPA.UserInterface.HostBuilders
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public static class AddConfigurationHostBuilderExtensions
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration(c =>
            {
                c.AddUserSecrets<App>();
#if DEBUG
                c.AddJsonFile("appSettings.json");
#else
                c.AddJsonFile("appSettings.Production.json");
#endif
            });

            return host;
        }
    }
}
