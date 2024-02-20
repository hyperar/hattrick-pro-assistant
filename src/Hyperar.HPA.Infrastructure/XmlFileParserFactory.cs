namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Application.Interfaces;
    using Common.Enums;
    using Infrastructure.Strategies.XmlFileParser;

    public class XmlFileParserFactory : IXmlFileParserFactory
    {
        private readonly ArenaDetails arenaDetailsParser;

        private readonly Avatars avatarsParser;

        private readonly HallOfFamePlayers hallOfFamePlayersParser;

        private readonly ManagerCompendium managerCompendiumParser;

        private readonly MatchDetails matchDetailsParser;

        private readonly Matches matchesParser;

        private readonly MatchLineUp matchLineUpParser;

        private readonly Players playersParser;

        private readonly StaffAvatars staffAvatarsParser;

        private readonly StaffList staffListParser;

        private readonly TeamDetails teamDetailsParser;

        private readonly WorldDetails worldDetailsParser;

        public XmlFileParserFactory(
            ArenaDetails arenaDetailsParser,
            Avatars avatarsParser,
            HallOfFamePlayers hallOfFamePlayersParser,
            ManagerCompendium managerCompendiumParser,
            MatchDetails matchDetailsParser,
            Matches matchesParser,
            MatchLineUp matchLineUpParser,
            Players playersParser,
            StaffAvatars staffAvatarsParser,
            StaffList staffListParser,
            TeamDetails teamDetailsParser,
            WorldDetails worldDetailsParser)

        {
            this.arenaDetailsParser = arenaDetailsParser;
            this.avatarsParser = avatarsParser;
            this.hallOfFamePlayersParser = hallOfFamePlayersParser;
            this.managerCompendiumParser = managerCompendiumParser;
            this.matchDetailsParser = matchDetailsParser;
            this.matchesParser = matchesParser;
            this.matchLineUpParser = matchLineUpParser;
            this.playersParser = playersParser;
            this.staffAvatarsParser = staffAvatarsParser;
            this.staffListParser = staffListParser;
            this.teamDetailsParser = teamDetailsParser;
            this.worldDetailsParser = worldDetailsParser;
        }

        public IXmlFileParserStrategy CreateXmlFileParser(XmlFileType fileType)
        {
            return fileType switch
            {
                XmlFileType.ArenaDetails => this.arenaDetailsParser,
                XmlFileType.Avatars => this.avatarsParser,
                XmlFileType.HallOfFamePlayers => this.hallOfFamePlayersParser,
                XmlFileType.ManagerCompendium => this.managerCompendiumParser,
                XmlFileType.MatchDetails => this.matchDetailsParser,
                XmlFileType.Matches => this.matchesParser,
                XmlFileType.MatchLineUp => this.matchLineUpParser,
                XmlFileType.Players => this.playersParser,
                XmlFileType.StaffAvatars => this.staffAvatarsParser,
                XmlFileType.StaffList => this.staffListParser,
                XmlFileType.TeamDetails => this.teamDetailsParser,
                XmlFileType.WorldDetails => this.worldDetailsParser,
                _ => throw new ArgumentOutOfRangeException(nameof(fileType))
            };
        }
    }
}