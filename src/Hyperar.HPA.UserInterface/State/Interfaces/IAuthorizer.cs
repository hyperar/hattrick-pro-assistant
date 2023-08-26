namespace Hyperar.HPA.UserInterface.State.Interfaces
{
    using System.ComponentModel;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.OAuth;

    public interface IAuthorizer
    {
        bool IsAuthorized { get; }

        bool IsInitialized { get; }

        event PropertyChangedEventHandler? PropertyChanged;

        void PersistToken(string accessToken, string accessTokenSecret);

        void InitializeToken();

        void RevokeToken();

        void CheckToken();

        GetAuthorizationUrlResponse GetAuthorizationUrl();

        GetAccessTokenResponse GetAccessToken(string verificationCode, string requestToken, string requestTokenSecret);

        GetProtectedResourceRequest BuildProtectedResourseRequest(DownloadTask task);
    }
}
