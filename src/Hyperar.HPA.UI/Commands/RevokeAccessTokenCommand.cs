namespace Hyperar.HPA.UI.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Hyperar.HPA.UI.State.Interfaces;
    using Hyperar.HPA.UI.ViewModels;

    public class RevokeAccessTokenCommand : AsyncCommandBase, IDisposable
    {
        private readonly INavigator navigator;

        private readonly PermissionsViewModel permissionsViewModel;

        public RevokeAccessTokenCommand(
            PermissionsViewModel permissionsViewModel,
            INavigator navigator)
        {
            this.permissionsViewModel = permissionsViewModel;
            this.navigator = navigator;

            this.permissionsViewModel.PropertyChanged += this.PermissionsViewModel_PropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        public void Dispose()
        {
            this.permissionsViewModel.PropertyChanged -= this.PermissionsViewModel_PropertyChanged;
            GC.SuppressFinalize(this);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            this.navigator.SuspendNavigation();

            await this.permissionsViewModel.Authorizer.RevokeTokenAsync();

            this.permissionsViewModel.AccessToken = null;
            this.permissionsViewModel.AccessTokenSecret = null;

            this.navigator.ResumeNavigation();
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