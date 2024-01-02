namespace Hyperar.HPA.Infrastructure.Services
{
    using System;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Application.OAuth;
    using Hyperar.HPA.Application.Services;
    using Hyperar.OauthCore.Consumer;
    using Hyperar.OauthCore.Framework;
    using Microsoft.Extensions.Configuration;

    public class HattrickService : IHattrickService
    {
        private const string AccessTokenKey = "OAuth:Urls:Base:AccessToken";

        private const string AuthorizeKey = "OAuth:Urls:Base:Authorize";

        private const string CallbackKry = "OAuth:Urls:Base:Callback";

        private const string CheckTokenKey = "OAuth:Urls:Base:CheckToken";

        private const string ConsumerKeyKey = "OAuth:ConsumerKey";

        private const string ConsumerSecretKey = "OAuth:ConsumerSecret";

        private const string InvalidateTokenKey = "OAuth:Urls:Base:InvalidateToken";

        private const string RequestTokenKey = "OAuth:Urls:Base:RequestToken";

        private const string UserAgentKey = "OAuth:UserAgent";

        private readonly IConfiguration configuration;

        private readonly IProtectedResourceUrlBuilder protectedResourceUrlBuilder;

        public HattrickService(IConfiguration configuration, IProtectedResourceUrlBuilder protectedResourceUrlBuilder)
        {
            this.configuration = configuration;
            this.protectedResourceUrlBuilder = protectedResourceUrlBuilder;
        }

        public string CheckToken(OAuthToken token)
        {
            ArgumentNullException.ThrowIfNull(token);

            OAuthSession session = this.CreateOAuthSession(token.Token, token.TokenSecret);

            string? checkTokenUrl = this.configuration[CheckTokenKey];

            return checkTokenUrl == null
                ? throw new NullReferenceException(nameof(checkTokenUrl))
                : ReadResponseStream(
                GetResponseContentForUrl(
                    checkTokenUrl,
                    session));
        }

        public GetAccessTokenResponse GetAccessToken(GetAccessTokenRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            OAuthSession session = this.CreateOAuthSession();

            var requestToken = new TokenBase()
            {
                Token = request.RequestToken.Token,
                TokenSecret = request.RequestToken.TokenSecret
            };

            IToken accessToken = session.ExchangeRequestTokenForAccessToken(
                                          requestToken,
                                          HttpMethod.Get.ToString(),
                                          request.VerificationCode);

            GetAccessTokenResponse result = new(
                accessToken.Token,
                accessToken.TokenSecret,
                DateTime.Now,
                DateTime.MaxValue);

            return result;
        }

        public GetAuthorizationUrlResponse GetAuthorizationUrl()
        {
            OAuthSession session = this.CreateOAuthSession();

            IToken requestToken = session.GetRequestToken(HttpMethod.Get.ToString());

            string url = session.GetUserAuthorizationUrlForToken(requestToken);

            GetAuthorizationUrlResponse result = new(url, requestToken.Token, requestToken.TokenSecret);

            return result;
        }

        public string GetProtectedResource(GetProtectedResourceRequest request)
        {
            string url = this.protectedResourceUrlBuilder.BuildUrl(request.FileType, request.Parameters);

            OAuthSession session = this.CreateOAuthSession(request.AccessToken.Token, request.AccessToken.TokenSecret);

            return ReadResponseStream(
                GetResponseContentForUrl(
                    url,
                    session));
        }

        public string RevokeToken(OAuthToken token)
        {
            OAuthSession session = this.CreateOAuthSession(token.Token, token.TokenSecret);

            string? invalidateTokenUrl = this.configuration[InvalidateTokenKey];

            return invalidateTokenUrl == null
                ? throw new NullReferenceException(nameof(invalidateTokenUrl))
                : ReadResponseStream(
                GetResponseContentForUrl(
                    invalidateTokenUrl,
                    session));
        }

        private static Stream GetResponseContentForUrl(string url, OAuthSession session)
        {
            return string.IsNullOrWhiteSpace(url)
                ? throw new ArgumentNullException(nameof(url))
                : session == null || session.AccessToken == null
                ? throw new ArgumentException("Incorrect OAuthSession configuration.", nameof(session))
                : session.Request()
                          .ForUrl(url)
                          .ForMethod(HttpMethod.Get.ToString())
                          .ToWebResponse()
                          .GetResponseStream();
        }

        private static string ReadResponseStream(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);

            string result;

            using (StreamReader reader = new(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

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

            return userAgent == null
                ? throw new NullReferenceException(nameof(callbackUrl))
                : new OAuthSession(
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

        private OAuthSession CreateOAuthSession(string token, string tokenSecret)
        {
            OAuthSession session = this.CreateOAuthSession();

            session.AccessToken = new TokenBase
            {
                Token = token,
                TokenSecret = tokenSecret
            };

            return session;
        }
    }
}