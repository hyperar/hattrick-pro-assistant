namespace Hyperar.HPA.UI.HostBuilders
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using UI.State;
    using UI.State.Interfaces;

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