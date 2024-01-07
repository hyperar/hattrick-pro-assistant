namespace Hyperar.HPA.UI.HostBuilders
{
    using UI.State.Interfaces;
    using UI.ViewModels;
    using UI.ViewModels.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton(s =>
                {
                    var scope = s.CreateScope();

                    var viewModel = new MainViewModel(
                            s.GetRequiredService<INavigator>(),
                            s.GetRequiredService<IViewModelFactory>(),
                            scope.ServiceProvider.GetRequiredService<IAuthorizer>());

                    viewModel.InitializeAsync().ConfigureAwait(false);

                    return new MainWindow(
                        s.GetRequiredService<IConfiguration>(),
                        viewModel);
                });
            });

            return host;
        }
    }
}