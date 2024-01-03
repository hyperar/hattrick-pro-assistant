namespace Hyperar.HPA.UI.State
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Hyperar.HPA.Application.OAuth;
    using Hyperar.HPA.Application.Services;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.UI.State.Interfaces;

    public class Authorizer : IAuthorizer, INotifyPropertyChanged
    {
        private readonly IHattrickService hattrickService;

        private readonly IUserService userService;

        public Authorizer(
            IHattrickService hattrickService,
            IUserService userService)
        {
            this.hattrickService = hattrickService;
            this.userService = userService;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool? IsAuthorized
        {
            get
            {
                return !this.IsInitialized
                    ? null
                    : this.User != null && this.User.Token != null;
            }
        }

        public bool IsInitialized { get; private set; } = false;

        public User? User { get; private set; }

        public GetProtectedResourceRequest BuildProtectedResourseRequest(DownloadTask task)
        {
            ArgumentNullException.ThrowIfNull(this.User);

            ArgumentNullException.ThrowIfNull(this.User.Token);

            return new GetProtectedResourceRequest(
                this.User.Token.Value,
                this.User.Token.SecretValue,
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
            this.User = await this.userService.GetUserAsync();

            this.IsInitialized = true;

            this.OnPropertyChanged(nameof(this.IsInitialized));
            this.OnPropertyChanged(nameof(this.IsAuthorized));
        }

        public async Task PersistTokenAsync(string accessToken, string accessTokenSecret)
        {
            ArgumentNullException.ThrowIfNull(this.User);

            await this.userService.InsertUserTokenAsync(accessToken, accessTokenSecret);

            await this.InitializeAsync();
        }

        public async Task RevokeTokenAsync()
        {
            this.ValidateInitialization();

            ArgumentNullException.ThrowIfNull(this.User);

            // This is only here to avoid .NET Core possible null reference message. If IsAuthorized is true, there's a token.
            if (this.User.Token != null)
            {
                await this.hattrickService.RevokeTokenAsync(
                    new OAuthToken(
                        this.User.Token.Value,
                        this.User.Token.SecretValue));

                await this.userService.DeleteUserTokenAsync(this.User.Id);

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