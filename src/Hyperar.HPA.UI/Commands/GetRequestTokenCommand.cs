namespace Hyperar.HPA.UI.Commands
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Hyperar.HPA.Application.Models;
    using Hyperar.HPA.UI.State.Interfaces;
    using Hyperar.HPA.UI.ViewModels;

    public class GetRequestTokenCommand : AsyncCommandBase
    {
        private readonly INavigator navigator;

        private readonly PermissionsViewModel permissionsViewModel;

        public GetRequestTokenCommand(PermissionsViewModel permissionsViewModel, INavigator navigator)
        {
            this.permissionsViewModel = permissionsViewModel;
            this.navigator = navigator;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            this.navigator.SuspendNavigation();

            GetAuthorizationUrlResponse result = await this.permissionsViewModel.Authorizer.GetAuthorizationUrlAsync();

            if (result != null)
            {
                this.permissionsViewModel.AuthorizationUrl = result.AuthorizationUrl;
                this.permissionsViewModel.RequestToken = result.RequestToken.Token;
                this.permissionsViewModel.RequestTokenSecret = result.RequestToken.TokenSecret;

                Process.Start(
                    new ProcessStartInfo(
                        result.AuthorizationUrl)
                    {
                        UseShellExecute = true
                    });
            }

            this.navigator.ResumeNavigation();
        }
    }
}