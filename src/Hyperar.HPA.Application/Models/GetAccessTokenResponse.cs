namespace Hyperar.HPA.Application.Models
{
    using System;

    public class GetAccessTokenResponse
    {
        public GetAccessTokenResponse(string token, string tokenSecret, DateTime createdOn, DateTime expiresOn)
        {
            this.AccessToken = new OAuthToken(token, tokenSecret);
            this.CreatedOn = createdOn;
            this.ExpiresOn = expiresOn;
        }

        public OAuthToken AccessToken { get; }

        public DateTime CreatedOn { get; }

        public DateTime ExpiresOn { get; }
    }
}