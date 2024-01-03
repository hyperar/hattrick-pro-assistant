namespace Hyperar.HPA.UI.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Hyperar.HPA.UI.ViewModels;

    public class RevokeAccessTokenCommand : AsyncCommandBase, IDisposable
    {
        private readonly PermissionsViewModel permissionsViewModel;

        public RevokeAccessTokenCommand(PermissionsViewModel permissionsViewModel)
        {
            this.permissionsViewModel = permissionsViewModel;
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
            await this.permissionsViewModel.Authorizer.RevokeTokenAsync();

            this.permissionsViewModel.AccessToken = null;
            this.permissionsViewModel.AccessTokenSecret = null;
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