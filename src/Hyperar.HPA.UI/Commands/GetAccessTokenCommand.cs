namespace Hyperar.HPA.UI.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Hyperar.HPA.Application.OAuth;
    using Hyperar.HPA.UI.ViewModels;

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

        public void Dispose()
        {
            this.permissionsViewModel.PropertyChanged -= this.PermissionsViewModel_PropertyChanged;
            GC.SuppressFinalize(this);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (parameter is GetAccessTokenRequest request)
            {
                GetAccessTokenResponse response = await this.permissionsViewModel.Authorizer.GetAccessTokenAsync(
                    request.VerificationCode,
                    request.RequestToken.Token,
                    request.RequestToken.TokenSecret);

                this.permissionsViewModel.AuthorizationUrl =
                this.permissionsViewModel.RequestToken =
                this.permissionsViewModel.RequestTokenSecret =
                this.permissionsViewModel.VerificationCode = null;

                this.permissionsViewModel.AccessToken = response.AccessToken.Token;
                this.permissionsViewModel.AccessTokenSecret = response.AccessToken.TokenSecret;

                await this.permissionsViewModel.Authorizer.PersistTokenAsync(response.AccessToken.Token, response.AccessToken.TokenSecret);
            }
        }

        private void PermissionsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PermissionsViewModel.CanGrantAccess))
            {
                this.OnCanExecuteChanged();
            }
        }
    }
}