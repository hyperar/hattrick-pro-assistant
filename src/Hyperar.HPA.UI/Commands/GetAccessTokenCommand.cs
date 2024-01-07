namespace Hyperar.HPA.UI.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Hyperar.HPA.Application.Models;
    using Hyperar.HPA.UI.Enums;
    using Hyperar.HPA.UI.State.Interfaces;
    using Hyperar.HPA.UI.ViewModels;
    using Hyperar.HPA.UI.ViewModels.Interfaces;

    public class GetAccessTokenCommand : AsyncCommandBase, IDisposable
    {
        private readonly INavigator navigator;

        private readonly PermissionsViewModel permissionsViewModel;

        private readonly IViewModelFactory viewModelFactory;

        public GetAccessTokenCommand(
            PermissionsViewModel permissionsViewModel,
            INavigator navigator,
            IViewModelFactory viewModelFactory)
        {
            this.permissionsViewModel = permissionsViewModel;
            this.navigator = navigator;

            this.permissionsViewModel.PropertyChanged += this.PermissionsViewModel_PropertyChanged;
            this.viewModelFactory = viewModelFactory;
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
                this.navigator.SuspendNavigation();

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

                this.navigator.ResumeNavigation();

                this.navigator.CurrentViewModel = await this.viewModelFactory.CreateAsyncViewModel(ViewType.Download);
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