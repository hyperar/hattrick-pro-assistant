namespace Hyperar.HPA.UI.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Application.Models;
    using UI.Enums;
    using UI.State.Interfaces;
    using UI.ViewModels;
    using UI.ViewModels.Interfaces;

    public class GetAccessTokenCommand : AsyncCommandBase, IDisposable
    {
        private readonly INavigator navigator;

        private readonly AuthorizationViewModel authorizationViewModel;

        private readonly IViewModelFactory viewModelFactory;

        public GetAccessTokenCommand(
            AuthorizationViewModel authorizationViewModel,
            INavigator navigator,
            IViewModelFactory viewModelFactory)
        {
            this.authorizationViewModel = authorizationViewModel;
            this.navigator = navigator;

            this.authorizationViewModel.PropertyChanged += this.AuthorizationViewModel_PropertyChanged;
            this.viewModelFactory = viewModelFactory;
        }

        public override bool CanExecute(object? parameter)
        {
            return this.authorizationViewModel.CanGrantAccess && base.CanExecute(parameter);
        }

        public void Dispose()
        {
            this.authorizationViewModel.PropertyChanged -= this.AuthorizationViewModel_PropertyChanged;
            GC.SuppressFinalize(this);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (parameter is GetAccessTokenRequest request)
            {
                this.navigator.SuspendNavigation();

                GetAccessTokenResponse response = await this.authorizationViewModel.Authorizer.GetAccessTokenAsync(
                    request.VerificationCode,
                    request.RequestToken.Token,
                    request.RequestToken.TokenSecret);

                this.authorizationViewModel.AuthorizationUrl =
                this.authorizationViewModel.RequestToken =
                this.authorizationViewModel.RequestTokenSecret =
                this.authorizationViewModel.VerificationCode = null;

                this.authorizationViewModel.AccessToken = response.AccessToken.Token;
                this.authorizationViewModel.AccessTokenSecret = response.AccessToken.TokenSecret;

                await this.authorizationViewModel.Authorizer.PersistTokenAsync(response.AccessToken.Token, response.AccessToken.TokenSecret);

                this.navigator.ResumeNavigation();

                this.navigator.CurrentViewModel = await this.viewModelFactory.CreateAsyncViewModel(ViewType.Download);
            }
        }

        private void AuthorizationViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AuthorizationViewModel.CanGrantAccess))
            {
                this.OnCanExecuteChanged();
            }
        }
    }
}