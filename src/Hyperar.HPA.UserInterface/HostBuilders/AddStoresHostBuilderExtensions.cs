namespace Hyperar.HPA.UserInterface.HostBuilders
{
    using Hyperar.HPA.UserInterface.State;
    using Hyperar.HPA.UserInterface.State.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<IAuthorizer, Authorizer>();
                services.AddSingleton<ITokenStore, TokenStore>();
            });

            return host;
        }
    }
}