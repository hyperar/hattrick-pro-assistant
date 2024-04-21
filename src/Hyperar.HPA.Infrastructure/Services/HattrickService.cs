namespace Hyperar.HPA.Infrastructure.Services
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Application.Services;
    using Hyperar.OauthCore.Consumer;
    using Hyperar.OauthCore.Framework;
    using Microsoft.Extensions.Configuration;

    public class HattrickService : IHattrickService
    {
        private const string AccessTokenKeyName = "OAuth:Urls:Base:AccessToken";

        private const string AuthorizeKeyName = "OAuth:Urls:Base:Authorize";

        private const string CallbackKeyName = "OAuth:Urls:Base:Callback";

        private const string CheckTokenKeyName = "OAuth:Urls:Base:CheckToken";

        private const string ConsumerKeyKeyName = "OAuth:ConsumerKey";

        private const string ConsumerSecretKeyName = "OAuth:ConsumerSecret";

        private const string InvalidateTokenKeyName = "OAuth:Urls:Base:InvalidateToken";

        private const string RequestTokenKeyName = "OAuth:Urls:Base:RequestToken";

        private const string UserAgentKeyName = "OAuth:UserAgent";

        private readonly IConfiguration configuration;

        private readonly IProtectedResourceUrlBuilder protectedResourceUrlBuilder;

        public HattrickService(IConfiguration configuration, IProtectedResourceUrlBuilder protectedResourceUrlBuilder)
        {
            this.configuration = configuration;
            this.protectedResourceUrlBuilder = protectedResourceUrlBuilder;
        }

        public async Task<string> CheckTokenAsync(string token, string tokenSecret, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(token, nameof(token));
            ArgumentException.ThrowIfNullOrWhiteSpace(tokenSecret, nameof(tokenSecret));

            OAuthSession session = this.CreateSignedOAuthSession(token, tokenSecret);

            string? checkTokenUrl = this.configuration[CheckTokenKeyName];

            ArgumentException.ThrowIfNullOrEmpty(checkTokenUrl, nameof(checkTokenUrl));

            Stream responseStream = await GetResponseStreamForUrlAsync(checkTokenUrl, session);

            return await ReadResponseStreamAsync(responseStream, cancellationToken);
        }

        public async Task<string> CheckTokenAsync(string token, string tokenSecret)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(token, nameof(token));
            ArgumentException.ThrowIfNullOrWhiteSpace(tokenSecret, nameof(tokenSecret));

            OAuthSession session = this.CreateSignedOAuthSession(token, tokenSecret);

            string? checkTokenUrl = this.configuration[CheckTokenKeyName];

            ArgumentException.ThrowIfNullOrEmpty(checkTokenUrl, nameof(checkTokenUrl));

            Stream responseStream = await GetResponseStreamForUrlAsync(checkTokenUrl, session);

            return await ReadResponseStreamAsync(responseStream);
        }

        public async Task<GetAccessTokenResponse> GetAccessTokenAsync(GetAccessTokenRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            OAuthSession session = this.CreateOAuthSession();

            ArgumentNullException.ThrowIfNull(session, nameof(session));

            TokenBase requestToken = new TokenBase
            {
                Token = request.RequestToken.Token,
                TokenSecret = request.RequestToken.TokenSecret
            };

            IToken accessToken = await Task.Run(() => session.ExchangeRequestTokenForAccessToken(
                requestToken,
                HttpMethod.Get.ToString(),
                request.VerificationCode));

            return new GetAccessTokenResponse(
                accessToken.Token,
                accessToken.TokenSecret,
                DateTime.Now,
                DateTime.MaxValue);
        }

        public async Task<GetAuthorizationUrlResponse> GetAuthorizationUrlAsync()
        {
            OAuthSession session = this.CreateOAuthSession();

            ArgumentNullException.ThrowIfNull(session, nameof(session));

            IToken requestToken = await Task.Run(() => session.GetRequestToken(HttpMethod.Get.ToString()));

            ArgumentNullException.ThrowIfNull(requestToken, nameof(requestToken));

            string url = await Task.Run(() => session.GetUserAuthorizationUrlForToken(requestToken));

            ArgumentNullException.ThrowIfNull(url, nameof(url));

            return new GetAuthorizationUrlResponse(url, requestToken.Token, requestToken.TokenSecret);
        }

        public async Task<string> GetProtectedResourceAsync(GetProtectedResourceRequest request, CancellationToken cancellationToken)
        {
            string url = this.protectedResourceUrlBuilder.BuildUrl(request.FileType, request.Parameters);

            ArgumentException.ThrowIfNullOrEmpty(url, nameof(url));

            OAuthSession session = this.CreateSignedOAuthSession(request.AccessToken.Token, request.AccessToken.TokenSecret);

            ArgumentNullException.ThrowIfNull(session, nameof(session));

            Stream responseStream = await GetResponseStreamForUrlAsync(url, session);

            return await ReadResponseStreamAsync(responseStream, cancellationToken);
        }

        public async Task<string> RevokeTokenAsync(OAuthToken token)
        {
            ArgumentNullException.ThrowIfNull(token, nameof(token));

            string? invalidateTokenUrl = this.configuration[InvalidateTokenKeyName];

            ArgumentException.ThrowIfNullOrEmpty(invalidateTokenUrl, nameof(invalidateTokenUrl));

            OAuthSession session = this.CreateSignedOAuthSession(token.Token, token.TokenSecret);

            Stream responseStream = await GetResponseStreamForUrlAsync(invalidateTokenUrl, session);

            return await ReadResponseStreamAsync(responseStream);
        }

        private static async Task<Stream> GetResponseStreamForUrlAsync(string url, OAuthSession session)
        {
            ArgumentException.ThrowIfNullOrEmpty(url, nameof(url));

            ArgumentNullException.ThrowIfNull(session, nameof(session));

            HttpWebRequest webRequest = session.Request()
                .ForUrl(url)
                .ForMethod(HttpMethod.Get.ToString())
                .ToWebRequest();

            WebResponse response = await webRequest.GetResponseAsync();

            return response.GetResponseStream();
        }

        private static async Task<string> ReadResponseStreamAsync(Stream responseStream, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(responseStream, nameof(responseStream));

            string result;

            using (StreamReader reader = new StreamReader(responseStream))
            {
                result = await reader.ReadToEndAsync(cancellationToken);
            }

            return result;
        }

        private static async Task<string> ReadResponseStreamAsync(Stream responseStream)
        {
            ArgumentNullException.ThrowIfNull(responseStream, nameof(responseStream));

            string result;

            using (StreamReader reader = new StreamReader(responseStream))
            {
                result = await reader.ReadToEndAsync();
            }

            return result;
        }

        private OAuthSession CreateOAuthSession()
        {
            string? accessTokenUrl = this.configuration[AccessTokenKeyName];
            string? authorizeUrl = this.configuration[AuthorizeKeyName];
            string? callbackUrl = this.configuration[CallbackKeyName];
            string? consumerKey = this.configuration[ConsumerKeyKeyName];
            string? consumerSecret = this.configuration[ConsumerSecretKeyName];
            string? requestTokenUrl = this.configuration[RequestTokenKeyName];
            string? userAgent = this.configuration[UserAgentKeyName];

            ArgumentException.ThrowIfNullOrEmpty(accessTokenUrl, nameof(accessTokenUrl));
            ArgumentException.ThrowIfNullOrEmpty(authorizeUrl, nameof(authorizeUrl));
            ArgumentException.ThrowIfNullOrEmpty(callbackUrl, nameof(callbackUrl));
            ArgumentException.ThrowIfNullOrEmpty(consumerKey, nameof(consumerKey));
            ArgumentException.ThrowIfNullOrEmpty(consumerSecret, nameof(consumerSecret));
            ArgumentException.ThrowIfNullOrEmpty(requestTokenUrl, nameof(requestTokenUrl));
            ArgumentException.ThrowIfNullOrEmpty(userAgent, nameof(userAgent));

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

        private OAuthSession CreateSignedOAuthSession(string token, string tokenSecret)
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