namespace Hyperar.HPA.UI.HostBuilders
{
    using Hyperar.HPA.Application.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using UI.State.Interfaces;
    using UI.ViewModels;
    using UI.ViewModels.Interfaces;

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
                            scope.ServiceProvider.GetRequiredService<IAuthorizer>(),
                            s.GetRequiredService<INavigator>(),
                            viewModelFactory: s.GetRequiredService<IViewModelFactory>());

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