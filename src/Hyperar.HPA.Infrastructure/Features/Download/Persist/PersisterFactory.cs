namespace Hyperar.HPA.Infrastructure.Features.Download.Persist
{
    using Application;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Persist.Strategies;
    using Shared.Enums;

    public class PersisterFactory : IPersisterFactory
    {
        private readonly Avatars avatarsPersister;

        private readonly Default defaultPersister;

        private readonly LeagueDetails leagueDetailsPersister;

        private readonly ManagerCompendium managerCompendiumPersister;

        private readonly MatchDetails matchDetailsPersister;

        private readonly Matches matchesPersister;

        private readonly MatchLineUp matchLineUpPersister;

        private readonly PlayerDetails playerDetailsPersister;

        private readonly Players playersPersister;

        private readonly TeamDetails teamDetailsPersister;

        private readonly WorldDetails worldDetailsPersister;

        private readonly YouthAvatars youthAvatarsPersister;

        private readonly YouthLeagueDetails youthLeagueDetailsPersister;

        private readonly YouthPlayerDetails youthPlayerDetailsPersister;

        private readonly YouthPlayerList youthPlayerListPersister;

        private readonly YouthTeamDetails youthTeamDetailsPersister;

        public PersisterFactory(
            Avatars avatarsPersister,
            Default defaultParser,
            LeagueDetails leagueDetailsPersister,
            ManagerCompendium managerCompendiumPersister,
            MatchDetails matchDetailsPersister,
            Matches matchesPersister,
            MatchLineUp matchLineUpPersister,
            PlayerDetails playerDetailsPersister,
            Players playersPersister,
            TeamDetails teamDetailsPersister,
            WorldDetails worldDetailsPersister,
            YouthAvatars youthAvatarsPersister,
            YouthLeagueDetails youthLeagueDetailsPersister,
            YouthPlayerDetails youthPlayerDetailsPersister,
            YouthPlayerList youthPlayerListPersister,
            YouthTeamDetails youthTeamDetailsPersister)
        {
            this.avatarsPersister = avatarsPersister;
            this.defaultPersister = defaultParser;
            this.leagueDetailsPersister = leagueDetailsPersister;
            this.managerCompendiumPersister = managerCompendiumPersister;
            this.matchDetailsPersister = matchDetailsPersister;
            this.matchesPersister = matchesPersister;
            this.matchLineUpPersister = matchLineUpPersister;
            this.playerDetailsPersister = playerDetailsPersister;
            this.playersPersister = playersPersister;
            this.teamDetailsPersister = teamDetailsPersister;
            this.worldDetailsPersister = worldDetailsPersister;
            this.youthAvatarsPersister = youthAvatarsPersister;
            this.youthLeagueDetailsPersister = youthLeagueDetailsPersister;
            this.youthPlayerDetailsPersister = youthPlayerDetailsPersister;
            this.youthPlayerListPersister = youthPlayerListPersister;
            this.youthTeamDetailsPersister = youthTeamDetailsPersister;
        }

        public IPersisterStrategy GetPersister(XmlDownloadTask task)
        {
            return task.FileType switch
            {
                XmlFileType.Avatars => this.avatarsPersister,
                XmlFileType.LeagueDetails => this.leagueDetailsPersister,
                XmlFileType.ManagerCompendium => this.managerCompendiumPersister,
                XmlFileType.MatchDetails => this.matchDetailsPersister,
                XmlFileType.Matches => this.matchesPersister,
                XmlFileType.MatchLineUp => this.matchLineUpPersister,
                XmlFileType.PlayerDetails => this.playerDetailsPersister,
                XmlFileType.Players => this.playersPersister,
                XmlFileType.TeamDetails => this.teamDetailsPersister,
                XmlFileType.WorldDetails => this.worldDetailsPersister,
                XmlFileType.YouthAvatars => this.youthAvatarsPersister,
                XmlFileType.YouthLeagueDetails => this.youthLeagueDetailsPersister,
                XmlFileType.YouthPlayerDetails => this.youthPlayerDetailsPersister,
                XmlFileType.YouthPlayerList => this.youthPlayerListPersister,
                XmlFileType.YouthTeamDetails => this.youthTeamDetailsPersister,
                _ => this.defaultPersister
            };
        }
    }
}