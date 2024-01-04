namespace Hyperar.HPA.Application.Models
{
    public class GetAuthorizationUrlResponse
    {
        public GetAuthorizationUrlResponse(string authorizationUrl, string token, string tokenSecret)
        {
            this.AuthorizationUrl = authorizationUrl;
            this.RequestToken = new OAuthToken(token, tokenSecret);
        }

        public string AuthorizationUrl { get; }

        public OAuthToken RequestToken { get; }
    }
}