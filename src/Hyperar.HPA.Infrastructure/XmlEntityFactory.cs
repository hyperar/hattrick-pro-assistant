namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Common.Constants;

    public class XmlEntityFactory : IXmlEntityFactory
    {
        public IXmlFile CreateEntity(string fileName)
        {
            return fileName switch
            {
                XmlFileName.ArenaDetails => new Application.Hattrick.ArenaDetails.HattrickData(fileName),
                XmlFileName.ManagerCompendium => new Application.Hattrick.ManagerCompendium.HattrickData(fileName),
                XmlFileName.Matches => new Application.Hattrick.Matches.HattrickData(fileName),
                XmlFileName.Players => new Application.Hattrick.Players.HattrickData(fileName),
                XmlFileName.TeamDetails => new Application.Hattrick.TeamDetails.HattrickData(fileName),
                XmlFileName.WorldDetails => new Application.Hattrick.WorldDetails.HattrickData(fileName),
                _ => throw new ArgumentOutOfRangeException(nameof(fileName))
            };
        }
    }
}