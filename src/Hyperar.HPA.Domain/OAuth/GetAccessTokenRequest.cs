namespace Hyperar.HPA.Domain.OAuth
{
    public class GetAccessTokenRequest
    {
        public string VerificationCode { get; }

        public OAuthToken RequestToken { get; }

        public GetAccessTokenRequest(string verificationCode, string token, string tokenSecret)
        {
            this.VerificationCode = verificationCode;
            this.RequestToken = new OAuthToken(token, tokenSecret);
        }
    }
}
