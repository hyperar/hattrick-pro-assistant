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
                c.AddJsonFile("appSettings.Debug.json");
#else
                c.AddJsonFile("appSettings.json");
#endif
            });

            return host;
        }
    }
}
