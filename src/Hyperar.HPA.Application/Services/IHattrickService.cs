namespace Hyperar.HPA.Application.Services
{
    using System.Threading;
    using Application.Models;

    public interface IHattrickService
    {
        Task<string> CheckTokenAsync(string token, string tokenSecret, CancellationToken cancellationToken);

        Task<string> CheckTokenAsync(string token, string tokenSecret);

        Task<GetAccessTokenResponse> GetAccessTokenAsync(GetAccessTokenRequest request);

        Task<GetAuthorizationUrlResponse> GetAuthorizationUrlAsync();

        Task<string> GetProtectedResourceAsync(GetProtectedResourceRequest request, CancellationToken cancellationToken);

        Task<string> RevokeTokenAsync(OAuthToken token);
    }
}