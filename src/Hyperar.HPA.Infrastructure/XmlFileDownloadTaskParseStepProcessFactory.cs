namespace Hyperar.HPA.Infrastructure
{
    using Application.Interfaces;
    using Shared.Enums;
    using Strategies.FileDownloadTaskStepProcess.Parser;

    public class XmlFileDownloadTaskParseStepProcessFactory : IXmlFileDownloadTaskParseStepProcessFactory
    {
        private readonly ArenaDetails arenaDetailsParser;

        private readonly Avatars avatarsParser;

        private readonly CheckToken checkTokenParser;

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

        public XmlFileDownloadTaskParseStepProcessFactory(
            ArenaDetails arenaDetailsParser,
            Avatars avatarsParser,
            CheckToken checkTokenParser,
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
            this.checkTokenParser = checkTokenParser;
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

        public IFileDownloadTaskStepProcessStrategy GetParser(IFileDownloadTask fileDownloadTask)
        {
            if (fileDownloadTask is IXmlFileDownloadTask xmlFileDownloadTask)
            {
                return xmlFileDownloadTask.XmlFileType switch
                {
                    XmlFileType.ArenaDetails => this.arenaDetailsParser,
                    XmlFileType.Avatars => this.avatarsParser,
                    XmlFileType.CheckToken => this.checkTokenParser,
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
                    _ => throw new ArgumentOutOfRangeException(nameof(fileDownloadTask), xmlFileDownloadTask.XmlFileType.ToString(), nameof(xmlFileDownloadTask.XmlFileType))
                };
            }
            else
            {
                throw new ArgumentException(Globalization.Translations.UnexpectedFileDownloadTaskType);
            }
        }
    }
}