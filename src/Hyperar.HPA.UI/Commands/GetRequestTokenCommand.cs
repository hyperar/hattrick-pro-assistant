namespace Hyperar.HPA.UI.Commands
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Hyperar.HPA.UI.ViewModels;

    public class GetRequestTokenCommand : AsyncCommandBase
    {
        private readonly PermissionsViewModel permissionsViewModel;

        public GetRequestTokenCommand(PermissionsViewModel permissionsViewModel)
        {
            this.permissionsViewModel = permissionsViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Application.OAuth.GetAuthorizationUrlResponse result = await Task.Run(() => this.permissionsViewModel.Authorizer.GetAuthorizationUrl());

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
        }
    }
}