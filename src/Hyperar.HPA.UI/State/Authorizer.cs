namespace Hyperar.HPA.UI.State
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Hyperar.HPA.Application.OAuth;
    using Hyperar.HPA.Application.Services;
    using Hyperar.HPA.UI.State.Interfaces;

    public class Authorizer : IAuthorizer, INotifyPropertyChanged
    {
        private readonly IHattrickService hattrickService;

        private readonly ITokenService tokenService;

        private Domain.Token? currentToken;

        public Authorizer(
            IHattrickService hattrickService,
            ITokenService tokenService)
        {
            this.hattrickService = hattrickService;
            this.tokenService = tokenService;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool? IsAuthorized
        {
            get
            {
                return !this.IsInitialized
                    ? null
                    : this.currentToken != null;
            }
        }

        public bool IsInitialized { get; private set; } = false;

        public GetProtectedResourceRequest BuildProtectedResourseRequest(DownloadTask task)
        {
            ArgumentNullException.ThrowIfNull(this.currentToken);

            return new GetProtectedResourceRequest(
                this.currentToken.TokenValue,
                this.currentToken.TokenSecretValue,
                task.FileType,
                task.Parameters);
        }

        public Task CheckTokenAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GetAccessTokenResponse> GetAccessTokenAsync(string verificationCode, string requestToken, string requestTokenSecret)
        {
            return await this.hattrickService.GetAccessTokenAsync(
                new GetAccessTokenRequest(
                    verificationCode,
                    requestToken,
                    requestTokenSecret));
        }

        public async Task<GetAuthorizationUrlResponse> GetAuthorizationUrlAsync()
        {
            return await this.hattrickService.GetAuthorizationUrlAsync();
        }

        public async Task InitializeAsync()
        {
            this.currentToken = await this.tokenService.GetTokenAsync();

            this.IsInitialized = true;

            this.OnPropertyChanged(nameof(this.IsInitialized));
            this.OnPropertyChanged(nameof(this.IsAuthorized));
        }

        public async Task PersistTokenAsync(string accessToken, string accessTokenSecret)
        {
            await this.tokenService.InsertTokenAsync(accessToken, accessTokenSecret);

            await this.InitializeAsync();
        }

        public async Task RevokeTokenAsync()
        {
            this.ValidateInitialization();

            // This is only here to avoid .NET Core possible null reference message. If IsAuthorized is true, there's a token.
            if (this.currentToken != null)
            {
                await this.hattrickService.RevokeTokenAsync(
                    new OAuthToken(
                        this.currentToken.TokenValue,
                        this.currentToken.TokenSecretValue));

                await this.tokenService.DeleteTokenAsync(
                    this.currentToken.TokenValue,
                    this.currentToken.TokenSecretValue);

                await this.InitializeAsync();
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ValidateInitialization()
        {
            if (!this.IsInitialized)
            {
                throw new InvalidOperationException(nameof(this.IsInitialized));
            }
        }
    }
}