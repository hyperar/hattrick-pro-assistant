namespace Hyperar.HPA.UserInterface.State
{
    using System;
    using System.ComponentModel;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Domain.OAuth;
    using Hyperar.HPA.UserInterface.State.Interfaces;

    public class Authorizer : IAuthorizer, INotifyPropertyChanged, IDisposable
    {
        private readonly ITokenStore tokenStore;

        private readonly IHattrickClient hattrickClient;

        private readonly ITokenService tokenService;

        private bool isInitialized;

        public Authorizer(ITokenStore tokenStore, IHattrickClient hattrickClient, ITokenService tokenService)
        {
            this.tokenStore = tokenStore;
            this.hattrickClient = hattrickClient;
            this.tokenService = tokenService;

            this.tokenStore.PropertyChanged += this.Token_PropertyChanged;
        }

        public bool IsAuthorized
        {
            get
            {
                return this.tokenStore.CurrentToken != null;
            }
        }

        public bool IsInitialized
        {
            get
            {
                return this.isInitialized;
            }
            private set
            {
                this.isInitialized = value;
                this.OnPropertyChanged(nameof(this.IsInitialized));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void CheckToken()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.tokenStore.PropertyChanged -= this.Token_PropertyChanged;
        }

        public GetAccessTokenResponse GetAccessToken(string verificationCode, string token, string tokenSecret)
        {
            return this.hattrickClient.GetAccessToken(
                new GetAccessTokenRequest(verificationCode, token, tokenSecret));
        }

        public GetAuthorizationUrlResponse GetAuthorizationUrl()
        {
            return this.hattrickClient.GetAuthorizationUrl();
        }

        public void InitializeToken()
        {
            var token = this.tokenService.GetToken();

            this.tokenStore.SetCurrentToken(token);

            this.IsInitialized = true;
        }

        public void PersistToken(string accessToken, string accessTokenSecret)
        {
            this.tokenService.InsertToken(accessToken, accessTokenSecret);

            this.InitializeToken();
        }

        public void RevokeToken()
        {
            if (!this.IsInitialized)
            {
                throw new InvalidOperationException("Can't revoke Token before initialization.");
            }

            if (!this.IsAuthorized)
            {
                throw new InvalidOperationException("Can't revoke Token if not authorized.");
            }

            // This is only here to avoid .NET Core possible null reference message. If IsAuthorized is true, there's a token.
            if (this.tokenStore.CurrentToken != null)
            {
                this.hattrickClient.RevokeToken(new OAuthToken(this.tokenStore.CurrentToken.TokenValue, this.tokenStore.CurrentToken.TokenSecretValue));
                this.tokenService.DeleteToken(this.tokenStore.CurrentToken.TokenValue, this.tokenStore.CurrentToken.TokenSecretValue);

                this.InitializeToken();
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Token_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TokenStore.CurrentToken))
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
            }
        }
    }
}
