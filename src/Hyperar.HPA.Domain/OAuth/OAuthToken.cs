namespace Hyperar.HPA.Domain.OAuth
{
    public class OAuthToken
    {
        public string Token { get; }

        public string TokenSecret { get; }

        public OAuthToken(string token, string tokenSecret)
        {
            this.Token = token;
            this.TokenSecret = tokenSecret;
        }
    }
}
