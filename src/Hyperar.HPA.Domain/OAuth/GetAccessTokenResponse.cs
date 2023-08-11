namespace Hyperar.HPA.Domain.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
