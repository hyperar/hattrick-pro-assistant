namespace Hyperar.HPA.UserInterface.State
{
    using System;
    using System.ComponentModel;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.OAuth;
    using Hyperar.HPA.UserInterface.State.Interfaces;

    public class Authorizer : IAuthorizer, INotifyPropertyChanged, IDisposable
    {
        private readonly ITokenStore tokenStore;

        private readonly IHattrickService hattrickService;

        private readonly ITokenService tokenService;

        private bool isInitialized;

        public Authorizer(ITokenStore tokenStore, IHattrickService hattrickService, ITokenService tokenService)
        {
            this.tokenStore = tokenStore;
            this.hattrickService = hattrickService;
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

        public GetProtectedResourceRequest BuildProtectedResourseRequest(DownloadTask task)
        {
            if (this.tokenStore.CurrentToken == null)
            {
                throw new Exception($"Can't create download request because there is no Access Token.");
            }

            return new GetProtectedResourceRequest(
                this.tokenStore.CurrentToken.TokenValue,
                this.tokenStore.CurrentToken.TokenSecretValue,
                task.FileType,
                task.Parameters);
        }

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
            return this.hattrickService.GetAccessToken(
                new GetAccessTokenRequest(verificationCode, token, tokenSecret));
        }

        public GetAuthorizationUrlResponse GetAuthorizationUrl()
        {
            return this.hattrickService.GetAuthorizationUrl();
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
                this.hattrickService.RevokeToken(new OAuthToken(this.tokenStore.CurrentToken.TokenValue, this.tokenStore.CurrentToken.TokenSecretValue));
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
