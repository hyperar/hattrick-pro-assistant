namespace Hyperar.HPA.UI.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using UI.State.Interfaces;
    using UI.ViewModels.Interfaces;

    public class AuthorizedViewModelBase : ViewModelBase, IViewModel, IAuthorizedViewModel, IDisposable
    {
        public AuthorizedViewModelBase(IAuthorizer authorizer)
        {
            this.IsInitialized = false;

            this.Authorizer = authorizer;

            this.Authorizer.PropertyChanged += this.Authorizer_PropertyChanged;
        }

        public IAuthorizer Authorizer { get; private set; }

        public bool? IsAuthorized
        {
            get
            {
                return this.Authorizer.IsAuthorized;
            }
        }

        public bool? IsNotAuthorized
        {
            get
            {
                return !this.IsAuthorized;
            }
        }

        public override void Dispose()
        {
            this.Authorizer.PropertyChanged -= this.Authorizer_PropertyChanged;

            base.Dispose();
        }

        public override async Task InitializeAsync()
        {
            await this.Authorizer.InitializeAsync();
        }

        private void Authorizer_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(nameof(this.IsAuthorized));
            this.OnPropertyChanged(nameof(this.IsNotAuthorized));
        }
    }
}