namespace Hyperar.HPA.UserInterface.ViewModels
{
    using System;
    using System.ComponentModel;
    using Hyperar.HPA.UserInterface.State;
    using Hyperar.HPA.UserInterface.State.Interfaces;
    using Hyperar.HPA.UserInterface.ViewModels.Interfaces;

    public class AuthorizedViewModelBase : ViewModelBase, IViewModel, IAuthorizedViewModel, IDisposable
    {
        public IAuthorizer Authorizer { get; set; }

        public AuthorizedViewModelBase(IAuthorizer authorizer)
        {
            this.Authorizer = authorizer;

            this.Authorizer.PropertyChanged += this.Authorizer_PropertyChanged;
        }

        public bool IsAuthorized
        {
            get
            {
                return this.Authorizer.IsAuthorized;
            }
        }

        public bool IsNotAuthorized
        {
            get
            {
                return !this.IsAuthorized;
            }
        }


        public bool IsInitialized
        {
            get
            {
                return this.Authorizer.IsInitialized;
            }
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

        public override void Dispose()
        {
            this.Authorizer.PropertyChanged -= this.Authorizer_PropertyChanged;

            base.Dispose();
        }
    }
}