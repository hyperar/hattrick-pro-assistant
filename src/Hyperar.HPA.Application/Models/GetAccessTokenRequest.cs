namespace Hyperar.HPA.Application.Models
{
    public class GetAccessTokenRequest
    {
        public GetAccessTokenRequest(string verificationCode, string token, string tokenSecret)
        {
            this.VerificationCode = verificationCode;
            this.RequestToken = new OAuthToken(token, tokenSecret);
        }

        public OAuthToken RequestToken { get; }

        public string VerificationCode { get; }
    }
}