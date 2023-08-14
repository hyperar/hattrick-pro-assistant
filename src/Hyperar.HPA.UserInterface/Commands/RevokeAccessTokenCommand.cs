namespace Hyperar.HPA.UserInterface.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Hyperar.HPA.UserInterface.ViewModels;

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

        public override async Task ExecuteAsync(object? parameter)
        {
            await Task.Run(() => this.permissionsViewModel.Authorizer.RevokeToken());

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

        public void Dispose()
        {
            this.permissionsViewModel.PropertyChanged -= this.PermissionsViewModel_PropertyChanged;
        }
    }
}
