namespace Hyperar.HPA.HattrickClient
{
    using System.Collections.Specialized;
    using Hyperar.OauthCore.Consumer;
    using Hyperar.OauthCore.Framework;

    public class ApiClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChppManager"/> class.
        /// </summary>
        public ApiClient()
        {
        }

        /// <summary>
        /// Checks the specified Access Token validity.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <returns>An IXmlEntity objects with the Hattrick response.</returns>
        public string CheckToken(string token, string tokenSecreet)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (string.IsNullOrEmpty(tokenSecreet))
            {
                throw new ArgumentNullException(nameof(tokenSecreet));
            }

            var session = this.CreateOAuthSession();

            return this.ReadResponseStream(
                                          this.GetResponseContentForUrl(
                                                   Constants.HattrickUrl.CheckToken,
                                                   session));
        }

        /// <summary>
        /// Downloads Resource file.
        /// </summary>
        /// <param name="url">Resource's URL.</param>
        /// <returns>File content bytes.</returns>
        public byte[] DownloadResourceFile(string url)
        {
            byte[] result;

            using (var webClient = new HttpClient())
            {
                var getTask = webClient.GetAsync(url);

                getTask.Wait();

                var readTask = getTask.Result.Content.ReadAsByteArrayAsync();

                readTask.Wait();

                result = readTask.Result;
            }

            return result;
        }

        /// <summary>
        /// Gets the Access Token from Hattrick.
        /// </summary>
        /// <param name="request">Request token and verification code.</param>
        /// <returns>Access Token.</returns>
        public NameValueCollection GetAccessToken(string token, string tokenSecret, string verifier)
        {
            var result = new NameValueCollection();

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (string.IsNullOrWhiteSpace(tokenSecret))
            {
                throw new ArgumentNullException(nameof(tokenSecret));
            }

            if (string.IsNullOrWhiteSpace(verifier))
            {
                throw new ArgumentNullException(nameof(verifier));
            }

            var session = this.CreateOAuthSession();

            var requestToken = new TokenBase
            {
                Token = token,
                TokenSecret = tokenSecret
            };

            var accessToken = session.ExchangeRequestTokenForAccessToken(
                                          requestToken,
                                          HttpMethod.Get.ToString(),
                                          verifier);

            result.Add("oauth_token", accessToken.Token);
            result.Add("oauth_token_secret", accessToken.TokenSecret);
            result.Add("oauth_token_created", DateTime.Now.ToString());
            result.Add("oauth_token_expires", DateTime.MaxValue.ToString());

            return result;
        }

        /// <summary>
        /// Gets a request token and the authorization URL.
        /// </summary>
        /// <returns>Request token and Authorization URL.</returns>
        public NameValueCollection GetAuthorizationUrl()
        {
            var result = new NameValueCollection();

            var session = this.CreateOAuthSession();

            var requestToken = session.GetRequestToken(HttpMethod.Get.ToString());

            string url = session.GetUserAuthorizationUrlForToken(requestToken);

            result.Add("url", url);
            result.Add("oauth_token", requestToken.Token);
            result.Add("oauth_token_secret", requestToken.TokenSecret);

            return result;
        }

        ///// <summary>
        ///// Access the specified protected resource file with the specified parameters.
        ///// </summary>
        ///// <param name="accessToken">Access token.</param>
        ///// <param name="file">File to fetch.</param>
        ///// <param name="parameters">File fetch parameters.</param>
        ///// <returns>IXmlEntity object with the Hattrick response.</returns>
        //public string GetProtectedResource(string token, string tokenSecret, XmlFile file, params KeyValuePair<string, string>[] parameters)
        //{
        //    string url = this.chppUrlBuilder.GetUrlFor(file, parameters);
        //    var session = this.CreateOAuthSession(token, tokenSecret);

        //    return this.ReadResponseStream(
        //        this.GetResponseContentForUrl(
        //            url,
        //            session));
        //}

        /// <summary>
        /// Revokes the specified Access Token.
        /// </summary>
        /// <param name="accessToken">Access token to revoke.</param>
        /// <returns>A string object with the Hattrick response.</returns>
        public string RevokeToken(string token, string tokenSecret)
        {
            var session = this.CreateOAuthSession(token, tokenSecret);

            return this.ReadResponseStream(
                            this.GetResponseContentForUrl(
                                     Constants.HattrickUrl.InvalidateToken,
                                     session));
        }

        /// <summary>
        /// Creates an unauthorized OAuthSession.
        /// </summary>
        /// <returns>Unauthorized OAuthSession object.</returns>
        private OAuthSession CreateOAuthSession()
        {
            return new OAuthSession(
                       new OAuthConsumerContext
                       {
                           ConsumerKey = Constants.Consumer.ConsumerKey,
                           ConsumerSecret = Constants.Consumer.ConsumerSecret,
                           SignatureMethod = SignatureMethod.HmacSha1,
                           UserAgent = Constants.Consumer.UserAgent
                       },
                       Constants.HattrickUrl.RequestToken,
                       Constants.HattrickUrl.Authorize,
                       Constants.HattrickUrl.AccessToken,
                       Constants.HattrickUrl.Callback);
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
        private Stream GetResponseContentForUrl(string url, OAuthSession session)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (session.AccessToken == null)
            {
                throw new ArgumentNullException(nameof(session.AccessToken));
            }

            if (string.IsNullOrWhiteSpace(session.AccessToken.Token))
            {
                throw new ArgumentNullException(nameof(session.AccessToken.Token));
            }

            if (string.IsNullOrWhiteSpace(session.AccessToken.TokenSecret))
            {
                throw new ArgumentNullException(nameof(session.AccessToken.TokenSecret));
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
        private string ReadResponseStream(Stream stream)
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