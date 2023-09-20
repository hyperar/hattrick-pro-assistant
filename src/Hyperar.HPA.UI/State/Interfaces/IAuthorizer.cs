namespace Hyperar.HPA.UI.State.Interfaces
{
    using System.ComponentModel;
    using Hyperar.HPA.Application.OAuth;

    public interface IAuthorizer
    {
        event PropertyChangedEventHandler? PropertyChanged;

        bool IsAuthorized { get; }

        bool IsInitialized { get; }

        GetProtectedResourceRequest BuildProtectedResourseRequest(DownloadTask task);

        void CheckToken();

        GetAccessTokenResponse GetAccessToken(string verificationCode, string requestToken, string requestTokenSecret);

        GetAuthorizationUrlResponse GetAuthorizationUrl();

        void InitializeToken();

        void PersistToken(string accessToken, string accessTokenSecret);

        void RevokeToken();
    }
}