namespace Hyperar.HPA.Business
{
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Domain.OAuth;
    using Hyperar.OauthCore.Consumer;
    using Hyperar.OauthCore.Framework;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Specialized;

    public class HattrickClient : IHattrickClient
    {
        private const string ConsumerKeyKey = "OAUTH_CONSUMER_KEY";
        private const string ConsumerSecretKey = "OAUTH_CONSUMER_SECRET";
        private const string UserAgentKey = "OAuth:UserAgent";
        private const string AccessTokenKey = "OAuth:Urls:Base:AccessToken";
        private const string AuthorizeKey = "OAuth:Urls:Base:Authorize";
        private const string CallbackKry = "OAuth:Urls:Base:Callback";
        private const string CheckTokenKey = "OAuth:Urls:Base:CheckToken";
        private const string InvalidateTokenKey = "OAuth:Urls:Base:InvalidateToken";
        private const string RequestTokenKey = "OAuth:Urls:Base:RequestToken";

        private readonly IConfiguration configuration;

        private readonly IProtectedResourceUrlBuilder protectedResourceUrlBuilder;
        public HattrickClient(IConfiguration configuration, IProtectedResourceUrlBuilder protectedResourceUrlBuilder)
        {
            this.configuration = configuration;
            this.protectedResourceUrlBuilder = protectedResourceUrlBuilder;
        }

        /// <summary>
        /// Checks the specified Access Token validity.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <returns>An IXmlEntity objects with the Hattrick response.</returns>
        public string CheckToken(OAuthToken token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            var session = this.CreateOAuthSession(token.Token, token.TokenSecret);

            string? checkTokenUrl = this.configuration[CheckTokenKey];

            if (checkTokenUrl == null)
            {
                throw new NullReferenceException(nameof(checkTokenUrl));
            }

            return ReadResponseStream(
                GetResponseContentForUrl(
                    checkTokenUrl,
                    session));
        }

        /// <summary>
        /// Gets the Access Token from Hattrick.
        /// </summary>
        /// <param name="request">Request token and verification code.</param>
        /// <returns>Access Token.</returns>
        public GetAccessTokenResponse GetAccessToken(GetAccessTokenRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var session = this.CreateOAuthSession();

            var requestToken = new TokenBase
            {
                Token = request.RequestToken.Token,
                TokenSecret = request.RequestToken.TokenSecret
            };

            var accessToken = session.ExchangeRequestTokenForAccessToken(
                                          requestToken,
                                          HttpMethod.Get.ToString(),
                                          request.VerificationCode);

            var result = new GetAccessTokenResponse(
                accessToken.Token,
                accessToken.TokenSecret,
                DateTime.Now,
                DateTime.MaxValue);

            return result;
        }

        /// <summary>
        /// Gets a request token and the authorization URL.
        /// </summary>
        /// <returns>Request token and Authorization URL.</returns>
        public GetAuthorizationUrlResponse GetAuthorizationUrl()
        {
            var session = this.CreateOAuthSession();

            var requestToken = session.GetRequestToken(HttpMethod.Get.ToString());

            string url = session.GetUserAuthorizationUrlForToken(requestToken);

            var result = new GetAuthorizationUrlResponse(url, requestToken.Token, requestToken.TokenSecret);

            return result;
        }

        /// <summary>
        /// Access the specified protected resource file with the specified parameters.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <param name="file">File to fetch.</param>
        /// <param name="parameters">File fetch parameters.</param>
        /// <returns>IXmlEntity object with the Hattrick response.</returns>
        public string GetProtectedResource(GetProtectedResourceRequest request)
        {
            string url = this.protectedResourceUrlBuilder.BuildUrl(request.FileType, request.Parameters);

            var session = this.CreateOAuthSession(request.AccessToken.Token, request.AccessToken.TokenSecret);

            return ReadResponseStream(
                GetResponseContentForUrl(
                    url,
                    session));
        }

        /// <summary>
        /// Revokes the specified Access Token.
        /// </summary>
        /// <param name="accessToken">Access token to revoke.</param>
        /// <returns>A string object with the Hattrick response.</returns>
        public string RevokeToken(OAuthToken token)
        {
            var session = this.CreateOAuthSession(token.Token, token.TokenSecret);

            string? invalidateTokenUrl = this.configuration[InvalidateTokenKey];

            if (invalidateTokenUrl == null)
            {
                throw new NullReferenceException(nameof(invalidateTokenUrl));
            }

            return ReadResponseStream(
                GetResponseContentForUrl(
                    invalidateTokenUrl,
                    session));
        }

        /// <summary>
        /// Creates an unauthorized OAuthSession.
        /// </summary>
        /// <returns>Unauthorized OAuthSession object.</returns>
        private OAuthSession CreateOAuthSession()
        {
            string? consumerKey = this.configuration[ConsumerKeyKey];

            if (consumerKey == null)
            {
                throw new NullReferenceException(nameof(consumerKey));
            }
            
            string? consumerSecret = this.configuration[ConsumerSecretKey];

            if (consumerSecret == null)
            {
                throw new NullReferenceException(nameof(consumerSecret));
            }

            string? requestTokenUrl = this.configuration[RequestTokenKey];

            if (requestTokenUrl == null)
            {
                throw new NullReferenceException(nameof(requestTokenUrl));
            }

            string? authorizeUrl = this.configuration[AuthorizeKey];

            if (authorizeUrl == null)
            {
                throw new NullReferenceException(nameof(authorizeUrl));
            }

            string? accessTokenUrl = this.configuration[AccessTokenKey];

            if (accessTokenUrl == null)
            {
                throw new NullReferenceException(nameof(accessTokenUrl));
            }
            string? callbackUrl = this.configuration[CallbackKry];

            if (callbackUrl == null)
            {
                throw new NullReferenceException(nameof(callbackUrl));
            }

            string? userAgent = this.configuration[UserAgentKey];

            if (userAgent == null)
            {
                throw new NullReferenceException(nameof(callbackUrl));
            }

            return new OAuthSession(
                       new OAuthConsumerContext
                       {
                           ConsumerKey = consumerKey,
                           ConsumerSecret = consumerSecret,
                           SignatureMethod = SignatureMethod.HmacSha1,
                           UserAgent = userAgent
                       },
                       requestTokenUrl,
                       authorizeUrl,
                       accessTokenUrl,
                       callbackUrl);
        }

        /// <summary>
        /// Creates an authorized OAuthSession.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <returns>Authorized OAuthSession object.</returns>
        private OAuthSession CreateOAuthSession(string token, string tokenSecret)
        {
            var session = this.CreateOAuthSession();

            session.AccessToken = new TokenBase
            {
                Token = token,
                TokenSecret = tokenSecret
            };

            return session;
        }

        /// <summary>
        /// Makes an OAuth request to the specified URL and returns the response in a string object.
        /// </summary>
        /// <param name="url">URL to make the request to.</param>
        /// <param name="session">Authorized OAuth session.</param>
        /// <returns>Response content.</returns>
        private static Stream GetResponseContentForUrl(string url, OAuthSession session)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (session == null || session.AccessToken == null)
            {
                throw new ArgumentException("Incorrect OAuthSession configuration.", nameof(session));
            }

            return session.Request()
                          .ForUrl(url)
                          .ForMethod(HttpMethod.Get.ToString())
                          .ToWebResponse()
                          .GetResponseStream();
        }

        /// <summary>
        /// Reads a response stream into a string.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <returns>A string object with the response content.</returns>
        private static string ReadResponseStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            string result;

            using (var reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}
