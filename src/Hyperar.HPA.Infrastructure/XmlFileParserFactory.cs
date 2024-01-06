namespace Hyperar.HPA.Infrastructure
{
    using System;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Infrastructure.Strategies.XmlFileParser;

    public class XmlFileParserFactory : IXmlFileParserFactory
    {
        private readonly ArenaDetails arenaDetailsParser;

        private readonly ManagerCompendium managerCompendiumParser;

        private readonly Matches matchesParser;

        private readonly Players playersParser;

        private readonly TeamDetails teamDetailsParser;

        private readonly WorldDetails worldDetailsParser;

        public XmlFileParserFactory(
            ArenaDetails arenaDetailsParser,
            ManagerCompendium managerCompendiumParser,
            Matches matchesParser,
            Players playersParser,
            TeamDetails teamDetailsParser,
            WorldDetails worldDetailsParser)
        {
            this.arenaDetailsParser = arenaDetailsParser;
            this.managerCompendiumParser = managerCompendiumParser;
            this.matchesParser = matchesParser;
            this.playersParser = playersParser;
            this.teamDetailsParser = teamDetailsParser;
            this.worldDetailsParser = worldDetailsParser;
        }

        public IXmlFileParserStrategy CreateXmlFileParser(XmlFileType fileType)
        {
            return fileType switch
            {
                XmlFileType.ArenaDetails => this.arenaDetailsParser,
                XmlFileType.ManagerCompendium => this.managerCompendiumParser,
                XmlFileType.Matches => this.matchesParser,
                XmlFileType.Players => this.playersParser,
                XmlFileType.TeamDetails => this.teamDetailsParser,
                XmlFileType.WorldDetails => this.worldDetailsParser,
                _ => throw new ArgumentOutOfRangeException(nameof(fileType))
            };
        }
    }
}