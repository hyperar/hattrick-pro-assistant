namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Common.Constants;

    public class XmlEntityFactory : IXmlEntityFactory
    {
        public IXmlFile CreateEntity(string fileName)
        {
            switch (fileName)
            {
                case XmlFileName.ArenaDetails:
                    return new Application.Hattrick.ArenaDetails.HattrickData(fileName);

                case XmlFileName.ManagerCompendium:
                    return new Application.Hattrick.ManagerCompendium.HattrickData(fileName);

                case XmlFileName.Players:
                    return new Application.Hattrick.Players.HattrickData(fileName);

                case XmlFileName.TeamDetails:
                    return new Application.Hattrick.TeamDetails.HattrickData(fileName);

                case XmlFileName.WorldDetails:
                    return new Application.Hattrick.WorldDetails.HattrickData(fileName);

                default:
                    throw new NotImplementedException($"No implementation for file name: '{fileName}'.");
            }
        }
    }
}