﻿namespace Hyperar.HPA.Application.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Enums;

    public class GetProtectedResourceRequest
    {
        public GetProtectedResourceRequest(string token, string tokenSecret, XmlFileType fileType, Dictionary<string, string>? parameters)
        {
            this.AccessToken = new OAuthToken(token, tokenSecret);
            this.FileType = fileType;
            this.Parameters = parameters;
        }

        public GetProtectedResourceRequest(string token, string tokenSecret, XmlFileType fileType, params KeyValuePair<string, string>[]? parameters)
        {
            this.AccessToken = new OAuthToken(token, tokenSecret);
            this.FileType = fileType;
            this.Parameters = parameters?.ToDictionary(x => x.Key, y => y.Value);
        }

        public OAuthToken AccessToken { get; }

        public XmlFileType FileType { get; }

        public Dictionary<string, string>? Parameters { get; }
    }
}