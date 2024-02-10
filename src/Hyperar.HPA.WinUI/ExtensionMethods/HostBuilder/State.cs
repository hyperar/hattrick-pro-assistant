namespace Hyperar.HPA.WinUI.ExtensionMethods.HostBuilder
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using WinUI.State;
    using WinUI.State.Interface;

    public static class State
    {
        public static IHostBuilder RegisterStates(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<ITeamSelector, TeamSelector>();
            });

            return host;
        }
    }
}