namespace Hyperar.HPA.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Common.Enums;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualBasic.FileIO;

    public class ProtectedResourceUrlBuilder : IProtectedResourceUrlBuilder
    {
        private const string ProtectedResourcesKey = "OAuth:Urls:Base:ProtectedResources";
        private const string FileAndVersionKey = "OAuth:Urls:ProtectedResources:{0}:FileAndVersion";
        private const string ParametersKey = "OAuth:Urls:ProtectedResources:{0}:Parameters";

        private readonly IConfigurationRoot configuration;

        public ProtectedResourceUrlBuilder(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        public string BuildUrl(XmlFileType fileType, Dictionary<string, string> parameters)
        {
            this.ValidateParameters(fileType, parameters);

            string? protectedResourcesUrl = this.configuration[ProtectedResourcesKey];

            if (protectedResourcesUrl == null)
            {
                throw new NullReferenceException(nameof(protectedResourcesUrl));
            }

            UriBuilder uriBuilder = new UriBuilder(protectedResourcesUrl)
            {
                Query = this.BuildQueryString(fileType, parameters)
            };

            return uriBuilder.ToString();
        }

        private void ValidateParameters(XmlFileType fileType, Dictionary<string, string> parameters)
        {
            string[] allowedParametersKeys = this.configuration[string.Format(ParametersKey, fileType.ToString())]?
                .Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

            string[] specifiedParametersKeys = parameters.Keys.ToArray();

            var unrecognizedParameters = specifiedParametersKeys.Except(allowedParametersKeys).ToArray();


            if (unrecognizedParameters.Any())
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.AppendLine($"The following parameters are not allowed for File Type '{fileType.ToString()}':");

                foreach (var key in unrecognizedParameters)
                {
                    stringBuilder.AppendLine($"- {key}");
                }

                throw new ArgumentOutOfRangeException(nameof(parameters), stringBuilder.ToString());
            }
        }

        private string BuildQueryString(XmlFileType fileType, Dictionary<string, string> parameters)
        {
            string fileTypeString = fileType.ToString();

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(this.configuration[string.Format(FileAndVersionKey, fileTypeString)]);

            foreach (var key in parameters.Keys)
            {
                stringBuilder.AppendFormat("&{0}={1}", key, HttpUtility.UrlEncode(parameters[key]));
            }

            return stringBuilder.ToString();
        }
    }
}
