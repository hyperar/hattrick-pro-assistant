namespace Hyperar.HPA.Domain.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
