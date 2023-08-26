namespace Hyperar.HPA.Common.ExtensionMethods
{
    using System;
    using Hyperar.HPA.Common.Enums;

    public static class XmlFileTypeExtensionMethods
    {
        public static XmlFileType ToXmlFileType(this string value)
        {
            switch (value)
            {
                case Constants.XmlFileName.ManagerCompendium:
                    return XmlFileType.ManagerCompendium;

                case Constants.XmlFileName.TeamDetails:
                    return XmlFileType.TeamDetails;

                case Constants.XmlFileName.WorldDetails:
                    return XmlFileType.WorldDetails;

                default:
                    throw new ArgumentException($"Unrecognized XmlFileName value: '{value}'.");
            }
        }
    }
}
