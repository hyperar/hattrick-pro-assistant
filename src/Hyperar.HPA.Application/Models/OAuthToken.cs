namespace Hyperar.HPA.Application.Models
{
    public class OAuthToken
    {
        public OAuthToken(string token, string tokenSecret)
        {
            this.Token = token;
            this.TokenSecret = tokenSecret;
        }

        public string Token { get; }

        public string TokenSecret { get; }
    }
}