namespace Hyperar.HPA.Domain.OAuth
{
    public class GetAuthorizationUrlResponse
    {
        public string AuthorizationUrl { get; }

        public OAuthToken RequestToken { get; }

        public GetAuthorizationUrlResponse(string authorizationUrl, string token, string tokenSecret)
        {
            this.AuthorizationUrl = authorizationUrl;
            this.RequestToken = new OAuthToken(token, tokenSecret);
        }
    }
}
