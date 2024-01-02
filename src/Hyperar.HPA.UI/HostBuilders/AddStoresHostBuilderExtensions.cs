namespace Hyperar.HPA.UI.HostBuilders
{
    using Hyperar.HPA.UI.State;
    using Hyperar.HPA.UI.State.Interfaces;
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
                services.AddScoped<ITokenStore, TokenStore>();
            });

            return host;
        }
    }
}