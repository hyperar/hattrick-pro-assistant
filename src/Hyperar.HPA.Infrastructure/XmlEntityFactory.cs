namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Application.Interfaces;
    using Shared.Constants;

    using Shared.Models.Hattrick.Interfaces;

    public class XmlEntityFactory : IXmlEntityFactory
    {
        public IXmlFile CreateEntity(string fileName)
        {
            return fileName.ToLower() switch
            {
                XmlFileName.ArenaDetails => new Shared.Models.Hattrick.ArenaDetails.HattrickData(fileName),
                XmlFileName.Avatars => new Shared.Models.Hattrick.Avatars.HattrickData(fileName),
                XmlFileName.CheckToken => new Shared.Models.Hattrick.CheckToken.HattrickData(fileName),
                XmlFileName.HallOfFamePlayers => new Shared.Models.Hattrick.HallOfFamePlayers.HattrickData(fileName),
                XmlFileName.ManagerCompendium => new Shared.Models.Hattrick.ManagerCompendium.HattrickData(fileName),
                XmlFileName.MatchArchive => new Shared.Models.Hattrick.MatchArchive.HattrickData(fileName),
                XmlFileName.MatchDetails => new Shared.Models.Hattrick.MatchDetails.HattrickData(fileName),
                XmlFileName.Matches => new Shared.Models.Hattrick.Matches.HattrickData(fileName),
                XmlFileName.MatchLineUp => new Shared.Models.Hattrick.MatchLineUp.HattrickData(fileName),
                XmlFileName.Players => new Shared.Models.Hattrick.Players.HattrickData(fileName),
                XmlFileName.PlayerDeails => new Shared.Models.Hattrick.PlayerDetails.HattrickData(fileName),
                XmlFileName.StaffAvatars => new Shared.Models.Hattrick.StaffAvatars.HattrickData(fileName),
                XmlFileName.StaffList => new Shared.Models.Hattrick.StaffList.HattrickData(fileName),
                XmlFileName.TeamDetails => new Shared.Models.Hattrick.TeamDetails.HattrickData(fileName),
                XmlFileName.WorldDetails => new Shared.Models.Hattrick.WorldDetails.HattrickData(fileName),
                _ => throw new ArgumentOutOfRangeException(nameof(fileName))
            };
        }
    }
}