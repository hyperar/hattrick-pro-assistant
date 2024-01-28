namespace Hyperar.HPA.UI.ViewModels
{
    using System.Windows.Input;
    using Application.Models;
    using UI.Commands;
    using UI.State.Interfaces;
    using UI.ViewModels.Interfaces;

    public class AuthorizationViewModel : AuthorizedViewModelBase
    {
        private string? accessToken;

        private string? accessTokenSecret;

        private string? authorizationUrl;

        private string? requestToken;

        private string? requestTokenSecret;

        private string? verificationCode;

        public AuthorizationViewModel(
            IAuthorizer authorizer,
            INavigator navigator,
            IViewModelFactory viewModelFactory) : base(authorizer)
        {
            this.GetRequestTokenCommand = new GetRequestTokenCommand(this, navigator);
            this.GetAccessTokenCommand = new GetAccessTokenCommand(this, navigator, viewModelFactory);
            this.RevokeAccessTokenCommand = new RevokeAccessTokenCommand(this, navigator);
        }

        public string? AccessToken
        {
            get
            {
                return this.accessToken;
            }
            set
            {
                this.accessToken = value;
                this.OnPropertyChanged(nameof(this.AccessToken));
                this.OnPropertyChanged(nameof(this.CanGrantAccess));
                this.OnPropertyChanged(nameof(this.GetAccessTokenRequest));
            }
        }

        public string? AccessTokenSecret
        {
            get
            {
                return this.accessTokenSecret;
            }
            set
            {
                this.accessTokenSecret = value;
                this.OnPropertyChanged(nameof(this.AccessTokenSecret));
                this.OnPropertyChanged(nameof(this.CanGrantAccess));
                this.OnPropertyChanged(nameof(this.GetAccessTokenRequest));
            }
        }

        public string? AuthorizationUrl
        {
            get
            {
                return this.authorizationUrl;
            }
            set
            {
                this.authorizationUrl = value;
                this.OnPropertyChanged(nameof(this.AuthorizationUrl));
                this.OnPropertyChanged(nameof(this.CanEnterVerificationCode));
            }
        }

        public bool CanEnterVerificationCode
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.RequestToken) &&
                       !string.IsNullOrWhiteSpace(this.RequestTokenSecret);
            }
        }

        public bool CanGrantAccess
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.VerificationCode) &&
                       !string.IsNullOrWhiteSpace(this.RequestToken) &&
                       !string.IsNullOrWhiteSpace(this.RequestTokenSecret);
            }
        }

        public ICommand? GetAccessTokenCommand { get; }

        public GetAccessTokenRequest? GetAccessTokenRequest
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.verificationCode) &&
                       !string.IsNullOrWhiteSpace(this.requestToken) &&
                       !string.IsNullOrWhiteSpace(this.requestTokenSecret)
                     ? new GetAccessTokenRequest(this.verificationCode, this.requestToken, this.requestTokenSecret)
                     : null;
            }
        }

        public ICommand? GetRequestTokenCommand { get; }

        public string? RequestToken
        {
            get
            {
                return this.requestToken;
            }
            set
            {
                this.requestToken = value;
                this.OnPropertyChanged(nameof(this.RequestToken));
                this.OnPropertyChanged(nameof(this.CanEnterVerificationCode));
            }
        }

        public string? RequestTokenSecret
        {
            get
            {
                return this.requestTokenSecret;
            }
            set
            {
                this.requestTokenSecret = value;
                this.OnPropertyChanged(nameof(this.RequestTokenSecret));
                this.OnPropertyChanged(nameof(this.CanEnterVerificationCode));
            }
        }

        public ICommand? RevokeAccessTokenCommand { get; }

        public string? VerificationCode
        {
            get
            {
                return this.verificationCode;
            }
            set
            {
                this.verificationCode = value;
                this.OnPropertyChanged(nameof(this.VerificationCode));
                this.OnPropertyChanged(nameof(this.CanGrantAccess));
                this.OnPropertyChanged(nameof(this.GetAccessTokenRequest));
            }
        }
    }
}