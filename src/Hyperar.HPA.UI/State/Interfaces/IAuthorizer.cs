namespace Hyperar.HPA.UI.State.Interfaces
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Hyperar.HPA.Application.OAuth;

    public interface IAuthorizer
    {
        event PropertyChangedEventHandler? PropertyChanged;

        bool? IsAuthorized { get; }

        bool IsInitialized { get; }

        Domain.User? User { get; }

        GetProtectedResourceRequest BuildProtectedResourseRequest(DownloadTask task);

        Task CheckTokenAsync();

        Task<GetAccessTokenResponse> GetAccessTokenAsync(string verificationCode, string requestToken, string requestTokenSecret);

        Task<GetAuthorizationUrlResponse> GetAuthorizationUrlAsync();

        Task InitializeAsync();

        Task PersistTokenAsync(string accessToken, string accessTokenSecret);

        Task RevokeTokenAsync();
    }
}