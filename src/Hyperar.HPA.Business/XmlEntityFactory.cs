namespace Hyperar.HPA.Business
{
    using System;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Common.Constants;
    using Hyperar.HPA.Domain.Hattrick;

    public class XmlEntityFactory : IXmlEntityFactory
    {
        public XmlFileBase CreateEntity(string fileName)
        {
            switch (fileName)
            {
                case XmlFileName.ManagerCompendium:
                    return new Domain.Hattrick.ManagerCompendium.HattrickData(fileName);

                case XmlFileName.TeamDetails:
                    return new Domain.Hattrick.TeamDetails.HattrickData(fileName);

                case XmlFileName.WorldDetails:
                    return new Domain.Hattrick.WorldDetails.HattrickData(fileName);

                default:
                    throw new NotImplementedException($"No implementation for file name: '{fileName}'.");
            }
        }
    }
}
