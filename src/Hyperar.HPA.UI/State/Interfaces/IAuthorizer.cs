namespace Hyperar.HPA.UI.State.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HPA.Application.OAuth;

    public interface IAuthorizer
    {
        event PropertyChangedEventHandler? PropertyChanged;

        bool? IsAuthorized { get; }

        bool IsInitialized { get; }

        GetProtectedResourceRequest BuildProtectedResourseRequest(DownloadTask task);

        Task CheckTokenAsync();

        Task<GetAccessTokenResponse> GetAccessTokenAsync(string verificationCode, string requestToken, string requestTokenSecret);

        Task<GetAuthorizationUrlResponse> GetAuthorizationUrlAsync();

        Task InitializeAsync();

        Task PersistTokenAsync(string accessToken, string accessTokenSecret);

        Task RevokeTokenAsync();
    }
}