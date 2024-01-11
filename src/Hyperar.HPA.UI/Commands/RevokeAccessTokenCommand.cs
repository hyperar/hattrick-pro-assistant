namespace Hyperar.HPA.UI.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using UI.State.Interfaces;
    using UI.ViewModels;

    public class RevokeAccessTokenCommand : AsyncCommandBase, IDisposable
    {
        private readonly INavigator navigator;

        private readonly AuthorizationViewModel authorizationViewModel;

        public RevokeAccessTokenCommand(
            AuthorizationViewModel authorizationViewModel,
            INavigator navigator)
        {
            this.authorizationViewModel = authorizationViewModel;
            this.navigator = navigator;

            this.authorizationViewModel.PropertyChanged += this.AuthorizationViewModel_PropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        public void Dispose()
        {
            this.authorizationViewModel.PropertyChanged -= this.AuthorizationViewModel_PropertyChanged;
            GC.SuppressFinalize(this);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            this.navigator.SuspendNavigation();

            await this.authorizationViewModel.Authorizer.RevokeTokenAsync();

            this.authorizationViewModel.AccessToken = null;
            this.authorizationViewModel.AccessTokenSecret = null;

            this.navigator.ResumeNavigation();
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