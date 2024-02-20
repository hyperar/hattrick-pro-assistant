namespace Hyperar.HPA.WinUI.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Net;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Application.Models;
    using Application.Services;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using WinUI.State.Interface;

    public partial class AuthorizationViewModel : AsyncViewModelBase
    {
        private readonly IHattrickService hattrickService;

        private readonly IUserService userService;

        [ObservableProperty]
        private string? accessToken;

        [ObservableProperty]
        private DateTime? accessTokenCreatedOn;

        [ObservableProperty]
        private DateTime? accessTokenExpiresOn;

        [ObservableProperty]
        private string? accessTokenSecret;

        private string? authorizationUrl;

        [ObservableProperty]
        private bool canEnterVerificationCode;

        [ObservableProperty]
        private bool canGetAccessToken;

        [ObservableProperty]
        private bool canGetRequestToken;

        [ObservableProperty]
        private bool canRevokeAccessToken;

        private string? requestToken;

        private string? requestTokenSecret;

        private int userId;

        [ObservableProperty]
        private string? verificationCode;

        public AuthorizationViewModel(
            INavigator navigator,
            IHattrickService hattrickService,
            IUserService userService) : base(navigator)
        {
            this.hattrickService = hattrickService;
            this.userService = userService;
        }

        [RelayCommand]
        public async Task GetAccessTokenAsync()
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(this.VerificationCode, nameof(this.VerificationCode));
            ArgumentException.ThrowIfNullOrWhiteSpace(this.requestToken, nameof(this.requestToken));
            ArgumentException.ThrowIfNullOrWhiteSpace(this.requestTokenSecret, nameof(this.requestTokenSecret));

            var response = await this.hattrickService.GetAccessTokenAsync(
                new GetAccessTokenRequest(
                    this.VerificationCode,
                    this.requestToken,
                    this.requestTokenSecret));

            this.AccessToken = response.AccessToken.Token;
            this.AccessTokenSecret = response.AccessToken.TokenSecret;
            this.AccessTokenCreatedOn = response.CreatedOn;
            this.AccessTokenExpiresOn = response.ExpiresOn;

            ArgumentException.ThrowIfNullOrWhiteSpace(this.AccessToken, nameof(this.AccessToken));
            ArgumentException.ThrowIfNullOrWhiteSpace(this.AccessTokenSecret, nameof(this.AccessTokenSecret));

            await this.userService.InsertUserTokenAsync(
                this.AccessToken,
                this.AccessTokenSecret);

            this.authorizationUrl =
            this.requestToken =
            this.requestTokenSecret =
            this.VerificationCode = null;

            this.CanGetRequestToken =
            this.CanEnterVerificationCode = false;
            this.CanRevokeAccessToken = true;

            // Allow navigation once it's authorized.
            this.Navigator.ResumeNavigation();
        }

        [RelayCommand]
        public async Task GetRequestTokenAsync()
        {
            GetAuthorizationUrlResponse result = await this.hattrickService.GetAuthorizationUrlAsync();

            this.authorizationUrl = result.AuthorizationUrl;
            this.requestToken = result.RequestToken.Token;
            this.requestTokenSecret = result.RequestToken.TokenSecret;

            Process.Start(
                new ProcessStartInfo(
                    this.authorizationUrl)
                {
                    UseShellExecute = true
                });

            this.CanEnterVerificationCode = true;
        }

        public override async Task InitializeAsync()
        {
            this.Navigator.SuspendNavigation();

            var user = await this.userService.GetUserAsync();

            ArgumentNullException.ThrowIfNull(user, nameof(user));

            this.userId = user.Id;

            if (user.Token != null)
            {
                try
                {
                    await this.hattrickService.CheckTokenAsync(
                        new OAuthToken(
                            user.Token.Value,
                            user.Token.SecretValue));

                    this.AccessToken = user.Token.Value;
                    this.AccessTokenSecret = user.Token.SecretValue;
                    this.AccessTokenCreatedOn = user.Token.CreatedOn;
                    this.AccessTokenExpiresOn = user.Token.ExpiresOn;
                }
                catch (WebException ex)
                {
                    if (ex.Response is HttpWebResponse response && response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await this.userService.DeleteUserTokenAsync(user.Id);

                        MessageBox.Show(
                            Globalization.Strings.AuthorizationRevokedElsewhereReauthorizeInOrderToKeepUsing,
                            Globalization.Strings.Information,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        Globalization.Strings.Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            this.CanGetRequestToken = string.IsNullOrWhiteSpace(this.AccessToken) && string.IsNullOrWhiteSpace(this.AccessTokenSecret);
            this.CanRevokeAccessToken = !this.CanGetRequestToken;

            await base.InitializeAsync();

            if (this.CanRevokeAccessToken)
            {
                this.Navigator.ResumeNavigation();
            }
            else
            {
                this.Navigator.SuspendNavigation();
            }
        }

        [RelayCommand]
        public async Task RevokeAccessTokenAsync()
        {
            this.Navigator.SuspendNavigation();

            ArgumentException.ThrowIfNullOrWhiteSpace(this.AccessToken, nameof(this.AccessToken));
            ArgumentException.ThrowIfNullOrWhiteSpace(this.AccessTokenSecret, nameof(this.AccessTokenSecret));

            await this.hattrickService.RevokeTokenAsync(
                new OAuthToken(
                    this.AccessToken,
                    this.AccessTokenSecret));

            await this.userService.DeleteUserTokenAsync(this.userId);

            this.AccessToken =
            this.AccessTokenSecret = null;
            this.AccessTokenCreatedOn =
            this.AccessTokenExpiresOn = null;

            this.CanGetRequestToken = true;
            this.CanRevokeAccessToken = false;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.VerificationCode))
            {
                this.CanGetAccessToken = !string.IsNullOrWhiteSpace(this.VerificationCode);
            }

            base.OnPropertyChanged(e);
        }
    }
}