namespace Hyperar.HPA.BusinessContracts
{
    using Hyperar.HPA.Domain.OAuth;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IHattrickClient
    {
        string CheckToken(OAuthToken token);

        GetAccessTokenResponse GetAccessToken(GetAccessTokenRequest request);

        string GetProtectedResource(GetProtectedResourceRequest request);

        GetAuthorizationUrlResponse GetAuthorizationUrl();

        string RevokeToken(OAuthToken token);
    }
}
