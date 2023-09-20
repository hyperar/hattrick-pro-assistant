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

        private readonly Players playersParser;

        private readonly TeamDetails teamDetailsParser;

        private readonly WorldDetails worldDetailsParser;

        public XmlFileParserFactory(
            ArenaDetails arenaDetailsParser,
            ManagerCompendium managerCompendiumParser,
            Players playersParser,
            TeamDetails teamDetailsParser,
            WorldDetails worldDetailsParser)
        {
            this.arenaDetailsParser = arenaDetailsParser;
            this.managerCompendiumParser = managerCompendiumParser;
            this.playersParser = playersParser;
            this.teamDetailsParser = teamDetailsParser;
            this.worldDetailsParser = worldDetailsParser;
        }

        public IXmlFileParserStrategy CreateXmlFileParser(XmlFileType fileType)
        {
            switch (fileType)
            {
                case XmlFileType.ArenaDetails:
                    return this.arenaDetailsParser;

                case XmlFileType.ManagerCompendium:
                    return this.managerCompendiumParser;

                case XmlFileType.Players:
                    return this.playersParser;

                case XmlFileType.TeamDetails:
                    return this.teamDetailsParser;

                case XmlFileType.WorldDetails:
                    return this.worldDetailsParser;

                default:
                    throw new NotImplementedException($"No implementation for file type: '{fileType}'.");
            }
        }
    }
}