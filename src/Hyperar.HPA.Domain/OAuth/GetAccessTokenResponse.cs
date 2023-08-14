namespace Hyperar.HPA.Domain.OAuth
{
    using System;

    public class GetAccessTokenResponse
    {
        public OAuthToken AccessToken { get; }

        public DateTime CreatedOn { get; }

        public DateTime ExpiresOn { get; }

        public GetAccessTokenResponse(string token, string tokenSecret, DateTime createdOn, DateTime expiresOn)
        {
            this.AccessToken = new OAuthToken(token, tokenSecret);
            this.CreatedOn = createdOn;
            this.ExpiresOn = expiresOn;
        }
    }
}
