namespace Hyperar.HPA.UI.HostBuilders
{
    using UI.State;
    using UI.State.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<INavigator, Navigator>();
                services.AddScoped<IAuthorizer, Authorizer>();
            });

            return host;
        }
    }
}