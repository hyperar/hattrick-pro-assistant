namespace Hyperar.HPA.UI.ViewModels
{
    using System;
    using System.ComponentModel;
    using Hyperar.HPA.UI.State;
    using Hyperar.HPA.UI.State.Interfaces;
    using Hyperar.HPA.UI.ViewModels.Interfaces;

    public class AuthorizedViewModelBase : ViewModelBase, IViewModel, IAuthorizedViewModel, IDisposable
    {
        public AuthorizedViewModelBase(IAuthorizer authorizer)
        {
            this.Authorizer = authorizer;

            this.Authorizer.PropertyChanged += this.Authorizer_PropertyChanged;

            this.InitializeToken();
        }

        public IAuthorizer Authorizer { get; set; }

        public bool IsAuthorized
        {
            get
            {
                return this.Authorizer.IsAuthorized;
            }
        }

        public bool IsInitialized
        {
            get
            {
                return this.Authorizer.IsInitialized;
            }
        }

        public bool IsNotAuthorized
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

        public void InitializeToken()
        {
            if (!this.IsInitialized)
            {
                this.Authorizer.InitializeToken();
            }
        }

        private void Authorizer_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TokenStore.CurrentToken))
            {
                this.OnPropertyChanged(nameof(this.IsAuthorized));
                this.OnPropertyChanged(nameof(this.IsNotAuthorized));
            }
        }
    }
}