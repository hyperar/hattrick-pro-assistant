namespace Hyperar.HPA.Application.Services
{
    using Hyperar.HPA.Application.OAuth;

    public interface IHattrickService
    {
        Task<string> CheckTokenAsync(OAuthToken token);

        Task<GetAccessTokenResponse> GetAccessTokenAsync(GetAccessTokenRequest request);

        Task<GetAuthorizationUrlResponse> GetAuthorizationUrlAsync();

        Task<string> GetProtectedResourceAsync(GetProtectedResourceRequest request);

        Task<string> RevokeTokenAsync(OAuthToken token);
    }
}