namespace Hyperar.HPA.UI.Commands
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Application.Models;
    using UI.State.Interfaces;
    using UI.ViewModels;

    public class GetRequestTokenCommand : AsyncCommandBase
    {
        private readonly INavigator navigator;

        private readonly AuthorizationViewModel authorizationViewModel;

        public GetRequestTokenCommand(AuthorizationViewModel authorizationViewModel, INavigator navigator)
        {
            this.authorizationViewModel = authorizationViewModel;
            this.navigator = navigator;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            this.navigator.SuspendNavigation();

            GetAuthorizationUrlResponse result = await this.authorizationViewModel.Authorizer.GetAuthorizationUrlAsync();

            if (result != null)
            {
                this.authorizationViewModel.AuthorizationUrl = result.AuthorizationUrl;
                this.authorizationViewModel.RequestToken = result.RequestToken.Token;
                this.authorizationViewModel.RequestTokenSecret = result.RequestToken.TokenSecret;

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