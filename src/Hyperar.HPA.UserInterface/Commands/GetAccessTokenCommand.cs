namespace Hyperar.HPA.UserInterface.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Hyperar.HPA.Domain.OAuth;
    using Hyperar.HPA.UserInterface.ViewModels;

    public class GetAccessTokenCommand : AsyncCommandBase, IDisposable
    {
        private readonly PermissionsViewModel permissionsViewModel;

        public GetAccessTokenCommand(PermissionsViewModel permissionsViewModel)
        {
            this.permissionsViewModel = permissionsViewModel;
            this.permissionsViewModel.PropertyChanged += this.PermissionsViewModel_PropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return this.permissionsViewModel.CanGrantAccess && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (parameter is GetAccessTokenRequest request)
            {
                var response = await Task.Run(() => this.permissionsViewModel.Authorizer.GetAccessToken(
                    request.VerificationCode,
                    request.RequestToken.Token,
                    request.RequestToken.TokenSecret));

                this.permissionsViewModel.AuthorizationUrl =
                this.permissionsViewModel.RequestToken =
                this.permissionsViewModel.RequestTokenSecret =
                this.permissionsViewModel.VerificationCode = null;

                this.permissionsViewModel.AccessToken = response.AccessToken.Token;
                this.permissionsViewModel.AccessTokenSecret = response.AccessToken.TokenSecret;

                this.permissionsViewModel.Authorizer.PersistToken(response.AccessToken.Token, response.AccessToken.TokenSecret);
            }
        }

        private void PermissionsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PermissionsViewModel.CanGrantAccess))
            {
                this.OnCanExecuteChanged();
            }
        }

        public void Dispose()
        {
            this.permissionsViewModel.PropertyChanged -= this.PermissionsViewModel_PropertyChanged;
        }
    }
}
