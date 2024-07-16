namespace Hyperar.HPA.Infrastructure
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using System.Web;
    using Application.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Shared.Enums;

    public class ProtectedResourceUrlFactory : IProtectedResourceUrlFactory
    {
        private const string FileAndVersionKey = "OAuth:Urls:ProtectedResources:{0}:FileAndVersion";

        private const string ParametersKey = "OAuth:Urls:ProtectedResources:{0}:Parameters";

        private const string ProtectedResourcesKey = "OAuth:Urls:Base:ProtectedResources";

        private readonly IConfiguration configuration;

        public ProtectedResourceUrlFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string BuildUrl(XmlFileType fileType, NameValueCollection parameters)
        {
            this.ValidateParameters(fileType, parameters);

            string? protectedResourcesUrl = this.configuration[ProtectedResourcesKey];

            ArgumentNullException.ThrowIfNull(protectedResourcesUrl, nameof(protectedResourcesUrl));

            UriBuilder uriBuilder = new UriBuilder(protectedResourcesUrl)
            {
                Query = this.BuildQueryString(fileType, parameters)
            };

            return uriBuilder.ToString();
        }

        private string BuildQueryString(XmlFileType fileType, NameValueCollection parameters)
        {
            string fileTypeString = fileType.ToString();

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(this.configuration[string.Format(FileAndVersionKey, fileTypeString)]);

            if (parameters != null && parameters.Count > 0)
            {
                foreach (string key in parameters.Keys)
                {
                    stringBuilder.AppendFormat("&{0}={1}", key, HttpUtility.UrlEncode(parameters[key]));
                }
            }

            return stringBuilder.ToString();
        }

        private void ValidateParameters(XmlFileType fileType, NameValueCollection parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return;
            }

            string[] allowedParametersKeys = this.configuration.GetSection(string.Format(ParametersKey, fileType.ToString()))
                .Get<string[]>() ?? Array.Empty<string>();

            string[] specifiedParametersKeys = parameters.Keys.Cast<string>().ToArray();

            string[] unrecognizedParameters = specifiedParametersKeys.Select(x => x.ToLower())
                                                                     .Except(allowedParametersKeys.Select(x => x.ToLower()))
                                                                     .ToArray();

            if (unrecognizedParameters.Length != 0)
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.AppendLine($"The following parameters are not allowed for File Type '{fileType.ToString()}':");

                foreach (string? key in unrecognizedParameters)
                {
                    stringBuilder.AppendLine($"- {key}.");
                }

                throw new ArgumentOutOfRangeException(nameof(parameters));
            }
        }
    }
}