namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Application.Interfaces;
    using Shared.Constants;
    using Shared.Models.Hattrick.Interfaces;
    using Models = Hyperar.HPA.Shared.Models.Hattrick;

    public class XmlEntityFactory : IXmlEntityFactory
    {
        public IXmlFile CreateEntity(string fileName)
        {
            return fileName.ToLower() switch
            {
                XmlFileName.ArenaDetails => new Models.ArenaDetails.HattrickData(fileName),
                XmlFileName.Avatars => new Models.Avatars.HattrickData(fileName),
                XmlFileName.CheckToken => new Models.CheckToken.HattrickData(fileName),
                XmlFileName.Club => new Models.Club.HattrickData(fileName),
                XmlFileName.HallOfFamePlayers => new Models.HallOfFamePlayers.HattrickData(fileName),
                XmlFileName.LeagueDetails => new Models.LeagueDetails.HattrickData(fileName),
                XmlFileName.ManagerCompendium => new Models.ManagerCompendium.HattrickData(fileName),
                XmlFileName.MatchArchive => new Models.MatchArchive.HattrickData(fileName),
                XmlFileName.MatchDetails => new Models.MatchDetails.HattrickData(fileName),
                XmlFileName.Matches => new Models.Matches.HattrickData(fileName),
                XmlFileName.MatchLineUp => new Models.MatchLineUp.HattrickData(fileName),
                XmlFileName.Players => new Models.Players.HattrickData(fileName),
                XmlFileName.PlayerDeails => new Models.PlayerDetails.HattrickData(fileName),
                XmlFileName.StaffAvatars => new Models.StaffAvatars.HattrickData(fileName),
                XmlFileName.StaffList => new Models.StaffList.HattrickData(fileName),
                XmlFileName.TeamDetails => new Models.TeamDetails.HattrickData(fileName),
                XmlFileName.WorldDetails => new Models.WorldDetails.HattrickData(fileName),
                XmlFileName.YouthAvatars => new Models.YouthAvatars.HattrickData(fileName),
                XmlFileName.YouthLeagueDetails => new Models.YouthLeagueDetails.HattrickData(fileName),
                XmlFileName.YouthPlayerDetails => new Models.YouthPlayerDetails.HattrickData(fileName),
                XmlFileName.YouthPlayerList => new Models.YouthPlayerList.HattrickData(fileName),
                XmlFileName.YouthTeamDetails => new Models.YouthTeamDetails.HattrickData(fileName),
                _ => throw new ArgumentOutOfRangeException(nameof(fileName))
            };
        }
    }
}