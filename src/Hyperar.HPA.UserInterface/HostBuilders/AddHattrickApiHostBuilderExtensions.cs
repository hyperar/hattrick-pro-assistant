namespace Hyperar.HPA.UserInterface.HostBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddHattrickApiHostBuilderExtensions
    {
        public static IHostBuilder AddHattrickApi(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                services.AddSingleton<BusinessContracts.IHattrickClient, Business.HattrickClient>();
                services.AddSingleton<BusinessContracts.IProtectedResourceUrlBuilder, Business.ProtectedResourceUrlBuilder>();
            });

            return host;
        }
    }
}
