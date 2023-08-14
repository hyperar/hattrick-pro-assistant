namespace Hyperar.HPA.BusinessContracts
{
    using Hyperar.HPA.Domain.OAuth;

    public interface IHattrickClient
    {
        string CheckToken(OAuthToken token);

        GetAccessTokenResponse GetAccessToken(GetAccessTokenRequest request);

        string GetProtectedResource(GetProtectedResourceRequest request);

        GetAuthorizationUrlResponse GetAuthorizationUrl();

        string RevokeToken(OAuthToken token);
    }
}
