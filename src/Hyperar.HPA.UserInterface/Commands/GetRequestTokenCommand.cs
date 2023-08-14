namespace Hyperar.HPA.UserInterface.Commands
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Hyperar.HPA.UserInterface.ViewModels;

    public class GetRequestTokenCommand : AsyncCommandBase
    {
        private readonly PermissionsViewModel permissionsViewModel;

        public GetRequestTokenCommand(PermissionsViewModel permissionsViewModel)
        {
            this.permissionsViewModel = permissionsViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var result = await Task.Run(() => this.permissionsViewModel.Authorizer.GetAuthorizationUrl());

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
