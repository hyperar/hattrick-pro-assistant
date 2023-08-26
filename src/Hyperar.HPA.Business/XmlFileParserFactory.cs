namespace Hyperar.HPA.Business
{
    using System;
    using Hyperar.HPA.Business.XmlFileParser;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Common.Enums;

    public class XmlFileParserFactory : IXmlFileParserFactory
    {
        public IXmlFileParserStrategy CreateXmlFileParser(XmlFileType fileType)
        {
            switch (fileType)
            {
                case XmlFileType.ManagerCompendium:
                    return new ManagerCompendium();

                case XmlFileType.TeamDetails:
                    return new TeamDetails();
                    break;

                case XmlFileType.WorldDetails:
                    return new WorldDetails();

                default:
                    throw new NotImplementedException($"No implementation for file type: '{fileType}'.");
            }
        }
    }
}
