namespace Hyperar.HPA.Domain.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
