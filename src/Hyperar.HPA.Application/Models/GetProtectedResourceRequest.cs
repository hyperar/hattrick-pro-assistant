namespace Hyperar.HPA.Application.Models
{
    using System.Collections.Specialized;
    using Shared.Enums;

    public class GetProtectedResourceRequest
    {
        public GetProtectedResourceRequest(string token, string tokenSecret, XmlFileType fileType, NameValueCollection parameters)
        {
            this.AccessToken = new OAuthToken(token, tokenSecret);
            this.FileType = fileType;
            this.Parameters = parameters;
        }

        public OAuthToken AccessToken { get; }

        public XmlFileType FileType { get; }

        public NameValueCollection Parameters { get; }
    }
}