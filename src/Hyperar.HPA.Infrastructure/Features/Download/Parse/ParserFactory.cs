namespace Hyperar.HPA.Infrastructure.Features.Download.Parse
{
    using Application;
    using Application.Interfaces;
    using Shared.Enums;
    using Strategies;

    public class ParserFactory : IParserFactory
    {
        private readonly Avatars avatarsParser;

        private readonly CheckToken checkTokenParser;

        private readonly Default defaultParser;

        private readonly LeagueDetails leagueDetailsParser;

        private readonly ManagerCompendium managerComendiumParser;

        private readonly MatchArchive matchArchiveParser;

        private readonly MatchDetails matchDetailsParser;

        private readonly Matches matchesParser;

        private readonly MatchLineUp matchLineUpParser;

        private readonly PlayerDetails playerDetailsParser;

        private readonly Players playersParser;

        private readonly StaffAvatars staffAvatarsParser;

        private readonly StaffList staffListParser;

        private readonly TeamDetails teamDetailsParser;

        private readonly WorldDetails worldDetailsParser;

        private readonly YouthAvatars youthAvatarsParser;

        private readonly YouthLeagueDetails youthLeagueDetailsParser;

        private readonly YouthPlayerDetails youthPlayerDetailsParser;

        private readonly YouthPlayerList youthPlayerListParser;

        private readonly YouthTeamDetails youthTeamDetailsParser;

        public ParserFactory(
            Avatars avatarsParser,
            CheckToken checkTokenParser,
            Default defaultParser,
            LeagueDetails leagueDetailsParser,
            ManagerCompendium managerComendiumParser,
            MatchArchive matchArchiveParser,
            MatchDetails matchDetailsParser,
            Matches matchesParser,
            MatchLineUp matchLineUpParser,
            PlayerDetails playerDetailsParser,
            Players playersParser,
            StaffAvatars staffAvatarsParser,
            StaffList staffListParser,
            TeamDetails teamDetailsParser,
            WorldDetails worldDetailsParser,
            YouthAvatars youthAvatarsParser,
            YouthLeagueDetails youthLeagueDetailsParser,
            YouthPlayerDetails youthPlayerDetailsParser,
            YouthPlayerList youthPlayerListParser,
            YouthTeamDetails youthTeamDetailsParser)
        {
            this.avatarsParser = avatarsParser;
            this.checkTokenParser = checkTokenParser;
            this.defaultParser = defaultParser;
            this.leagueDetailsParser = leagueDetailsParser;
            this.managerComendiumParser = managerComendiumParser;
            this.matchArchiveParser = matchArchiveParser;
            this.matchDetailsParser = matchDetailsParser;
            this.matchesParser = matchesParser;
            this.matchLineUpParser = matchLineUpParser;
            this.playerDetailsParser = playerDetailsParser;
            this.playersParser = playersParser;
            this.staffAvatarsParser = staffAvatarsParser;
            this.staffListParser = staffListParser;
            this.teamDetailsParser = teamDetailsParser;
            this.worldDetailsParser = worldDetailsParser;
            this.youthAvatarsParser = youthAvatarsParser;
            this.youthLeagueDetailsParser = youthLeagueDetailsParser;
            this.youthPlayerDetailsParser = youthPlayerDetailsParser;
            this.youthPlayerListParser = youthPlayerListParser;
            this.youthTeamDetailsParser = youthTeamDetailsParser;
        }

        public IParserStrategy GetParser(XmlDownloadTask task)
        {
            return task.FileType switch
            {
                XmlFileType.Avatars => this.avatarsParser,
                XmlFileType.CheckToken => this.checkTokenParser,
                XmlFileType.LeagueDetails => this.leagueDetailsParser,
                XmlFileType.ManagerCompendium => this.managerComendiumParser,
                XmlFileType.MatchArchive => this.matchArchiveParser,
                XmlFileType.MatchDetails => this.matchDetailsParser,
                XmlFileType.Matches => this.matchesParser,
                XmlFileType.MatchLineUp => this.matchLineUpParser,
                XmlFileType.PlayerDetails => this.playerDetailsParser,
                XmlFileType.Players => this.playersParser,
                XmlFileType.StaffAvatars => this.staffAvatarsParser,
                XmlFileType.StaffList => this.staffListParser,
                XmlFileType.TeamDetails => this.teamDetailsParser,
                XmlFileType.WorldDetails => this.worldDetailsParser,
                XmlFileType.YouthAvatars => this.youthAvatarsParser,
                XmlFileType.YouthLeagueDetails => this.youthLeagueDetailsParser,
                XmlFileType.YouthPlayerDetails => this.youthPlayerDetailsParser,
                XmlFileType.YouthPlayerList => this.youthPlayerListParser,
                XmlFileType.YouthTeamDetails => this.youthTeamDetailsParser,
                _ => this.defaultParser
            };
        }
    }
}