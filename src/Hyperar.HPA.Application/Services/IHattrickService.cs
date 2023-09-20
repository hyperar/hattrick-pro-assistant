namespace Hyperar.HPA.Application.Services
{
    using Hyperar.HPA.Application.OAuth;

    public interface IHattrickService
    {
        string CheckToken(OAuthToken token);

        GetAccessTokenResponse GetAccessToken(GetAccessTokenRequest request);

        GetAuthorizationUrlResponse GetAuthorizationUrl();

        string GetProtectedResource(GetProtectedResourceRequest request);

        string RevokeToken(OAuthToken token);
    }
}