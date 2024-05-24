namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models = Shared.Models.Hattrick;

    public class TeamDetails : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.Senior.HallOfFamePlayer> hallOfFamePlayerRepository;

        private readonly IHattrickRepository<Domain.League> leagueRepository;

        private readonly IHattrickRepository<Domain.Manager> managerRepository;

        private readonly IRepository<Domain.Senior.MatchArena> matchArenaRepository;

        private readonly IRepository<Domain.Senior.MatchEvent> matchEventRepository;

        private readonly IRepository<Domain.Senior.MatchOfficial> matchOfficialRepository;

        private readonly IHattrickRepository<Domain.Senior.Match> matchRepository;

        private readonly IRepository<Domain.Senior.MatchTeamBooking> matchTeamBookingRepository;

        private readonly IRepository<Domain.Senior.MatchTeamGoal> matchTeamGoalRepository;

        private readonly IRepository<Domain.Senior.MatchTeamInjury> matchTeamInjuryRepository;

        private readonly IRepository<Domain.Senior.MatchTeamLineUpPlayer> matchTeamLineUpPlayerRepository;

        private readonly IRepository<Domain.Senior.MatchTeamLineUp> matchTeamLineUpRepository;

        private readonly IRepository<Domain.Senior.MatchTeamLineUpStartingPlayer> matchTeamLineUpStartingPlayerRepository;

        private readonly IRepository<Domain.Senior.MatchTeamLineUpSubstitution> matchTeamLineUpSubstitutionRepository;

        private readonly IRepository<Domain.Senior.MatchTeam> matchTeamRepository;

        private readonly IRepository<Domain.Senior.PlayerMatch> playerMatchRepository;

        private readonly IHattrickRepository<Domain.Senior.Player> playerRepository;

        private readonly IRepository<Domain.Senior.PlayerSkillSet> playerSkillSetRepository;

        private readonly IHattrickRepository<Domain.Region> regionRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        public TeamDetails(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Senior.HallOfFamePlayer> hallOfFamePlayerRepository,
            IHattrickRepository<Domain.League> leagueRepository,
            IHattrickRepository<Domain.Manager> managerRepository,
            IRepository<Domain.Senior.MatchArena> matchArenaRepository,
            IRepository<Domain.Senior.MatchEvent> matchEventRepository,
            IRepository<Domain.Senior.MatchOfficial> matchOfficialRepository,
            IHattrickRepository<Domain.Senior.Match> matchRepository,
            IRepository<Domain.Senior.MatchTeamBooking> matchTeamBookingRepository,
            IRepository<Domain.Senior.MatchTeamGoal> matchTeamGoalRepository,
            IRepository<Domain.Senior.MatchTeamInjury> matchTeamInjuryRepository,
            IRepository<Domain.Senior.MatchTeamLineUpPlayer> matchTeamLineUpPlayerRepository,
            IRepository<Domain.Senior.MatchTeamLineUp> matchTeamLineUpRepository,
            IRepository<Domain.Senior.MatchTeamLineUpStartingPlayer> matchTeamLineUpStartingPlayerRepository,
            IRepository<Domain.Senior.MatchTeamLineUpSubstitution> matchTeamLineUpSubstitutionRepository,
            IRepository<Domain.Senior.MatchTeam> matchTeamRepository,
            IHattrickRepository<Domain.Senior.Player> playerRepository,
            IRepository<Domain.Senior.PlayerMatch> playerMatchRepository,
            IRepository<Domain.Senior.PlayerSkillSet> playerSkillSetRepository,
            IHattrickRepository<Domain.Region> regionRepository,
            IHattrickRepository<Domain.Senior.Team> teamRepository)
        {
            this.databaseContext = databaseContext;
            this.hallOfFamePlayerRepository = hallOfFamePlayerRepository;
            this.leagueRepository = leagueRepository;
            this.managerRepository = managerRepository;
            this.matchArenaRepository = matchArenaRepository;
            this.matchEventRepository = matchEventRepository;
            this.matchOfficialRepository = matchOfficialRepository;
            this.matchRepository = matchRepository;
            this.matchTeamBookingRepository = matchTeamBookingRepository;
            this.matchTeamGoalRepository = matchTeamGoalRepository;
            this.matchTeamInjuryRepository = matchTeamInjuryRepository;
            this.matchTeamLineUpPlayerRepository = matchTeamLineUpPlayerRepository;
            this.matchTeamLineUpRepository = matchTeamLineUpRepository;
            this.matchTeamLineUpStartingPlayerRepository = matchTeamLineUpStartingPlayerRepository;
            this.matchTeamLineUpSubstitutionRepository = matchTeamLineUpSubstitutionRepository;
            this.matchTeamRepository = matchTeamRepository;
            this.playerRepository = playerRepository;
            this.playerMatchRepository = playerMatchRepository;
            this.playerSkillSetRepository = playerSkillSetRepository;
            this.regionRepository = regionRepository;
            this.teamRepository = teamRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.TeamDetails.HattrickData file)
            {
                var manager = await this.managerRepository.GetByHattrickIdAsync(file.UserId);

                ArgumentNullException.ThrowIfNull(manager, nameof(manager));

                // Delete former teams.
                var xmlTeamIds = file.Teams.Select(x => x.TeamId);

                var teamIdsToDelete = await this.teamRepository.Query(x => !xmlTeamIds.Contains(x.HattrickId))
                                                               .Select(x => x.HattrickId)
                                                               .ToListAsync(cancellationToken);

                foreach (var xmlTeamId in teamIdsToDelete)
                {
                    await this.DeleteTeamAsync(xmlTeamId, cancellationToken);
                }

                // Process current teams.
                foreach (var xmlTeam in file.Teams)
                {
                    await this.ProcessTeamAsync(xmlTeam, manager, cancellationToken);
                }
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.TeamDetails.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task DeleteTeamAsync(long teamId, CancellationToken cancellationToken)
        {
            await this.hallOfFamePlayerRepository.DeleteRangeAsync(
                await this.hallOfFamePlayerRepository.Query(x => x.TeamHattrickId == teamId)
                    .Select(x => x.HattrickId)
                    .ToListAsync(cancellationToken));

            await this.playerMatchRepository.DeleteRangeAsync(
                await this.playerMatchRepository.Query(x => x.Player.TeamHattrickId == teamId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken));

            await this.playerSkillSetRepository.DeleteRangeAsync(
                await this.playerSkillSetRepository.Query(x => x.Player.TeamHattrickId == teamId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken));

            await this.playerRepository.DeleteRangeAsync(
                await this.playerRepository.Query(x => x.TeamHattrickId == teamId)
                    .Select(x => x.HattrickId)
                    .ToListAsync(cancellationToken));

            await this.matchTeamLineUpSubstitutionRepository.DeleteRangeAsync(
                await this.matchTeamLineUpSubstitutionRepository.Query(x => x.MatchTeamLineUp.MatchTeam.Match.TeamHattrickId == teamId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken));

            await this.matchTeamLineUpStartingPlayerRepository.DeleteRangeAsync(
                await this.matchTeamLineUpStartingPlayerRepository.Query(x => x.MatchTeamLineUp.MatchTeam.Match.TeamHattrickId == teamId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken));

            await this.matchTeamLineUpPlayerRepository.DeleteRangeAsync(
                await this.matchTeamLineUpPlayerRepository.Query(x => x.MatchTeamLineUp.MatchTeam.Match.TeamHattrickId == teamId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken));

            await this.matchTeamLineUpRepository.DeleteRangeAsync(
                await this.matchTeamLineUpRepository.Query(x => x.MatchTeam.Match.TeamHattrickId == teamId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken));

            await this.matchTeamInjuryRepository.DeleteRangeAsync(
                await this.matchTeamInjuryRepository.Query(x => x.MatchTeam.Match.TeamHattrickId == teamId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken));

            await this.matchTeamGoalRepository.DeleteRangeAsync(
                await this.matchTeamGoalRepository.Query(x => x.MatchTeam.Match.TeamHattrickId == teamId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken));

            await this.matchTeamBookingRepository.DeleteRangeAsync(
                await this.matchTeamBookingRepository.Query(x => x.MatchTeam.Match.TeamHattrickId == teamId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken));

            await this.matchTeamRepository.DeleteRangeAsync(
                await this.matchTeamRepository.Query(x => x.Match.TeamHattrickId == teamId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken));

            await this.matchOfficialRepository.DeleteRangeAsync(
                await this.matchOfficialRepository.Query(x => x.Match.TeamHattrickId == teamId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken));

            await this.matchEventRepository.DeleteRangeAsync(
                await this.matchEventRepository.Query(x => x.Match.TeamHattrickId == teamId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken));

            await this.matchArenaRepository.DeleteRangeAsync(
                await this.matchArenaRepository.Query(x => x.Match.TeamHattrickId == teamId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken));

            await this.matchRepository.DeleteRangeAsync(
                await this.matchRepository.Query(x => x.TeamHattrickId == teamId)
                    .Select(x => x.HattrickId)
                    .ToListAsync(cancellationToken));

            await this.teamRepository.DeleteAsync(teamId);
        }

        private async Task ProcessTeamAsync(Models.TeamDetails.Team xmlTeam, Domain.Manager manager, CancellationToken cancellationToken)
        {
            var team = await this.teamRepository.GetByHattrickIdAsync(xmlTeam.TeamId);

            var league = await this.leagueRepository.GetByHattrickIdAsync(xmlTeam.League.Id);

            ArgumentNullException.ThrowIfNull(league, nameof(league));

            var region = await this.regionRepository.GetByHattrickIdAsync(xmlTeam.Region.Id);

            ArgumentNullException.ThrowIfNull(region, nameof(region));

            byte[]? logoBytes = null;

            if (!string.IsNullOrWhiteSpace(xmlTeam.LogoUrl))
            {
                logoBytes = await GetImageBytesFromCacheAsync(xmlTeam.LogoUrl, cancellationToken);
            }

            byte[] homeMatchKitBytes = await GetImageBytesFromCacheAsync(xmlTeam.DressUri, cancellationToken);

            byte[] awayMatchKitBytes = await GetImageBytesFromCacheAsync(xmlTeam.DressAlternateUri, cancellationToken);

            if (team == null)
            {
                team = Domain.Senior.Team.Create(
                    xmlTeam,
                    logoBytes,
                    homeMatchKitBytes,
                    awayMatchKitBytes,
                    league,
                    manager,
                    region);

                await this.teamRepository.InsertAsync(team);
            }
            else if (team.HasChanged(
                xmlTeam,
                logoBytes,
                homeMatchKitBytes,
                awayMatchKitBytes))
            {
                team.Update(
                    xmlTeam,
                    logoBytes,
                    homeMatchKitBytes,
                    awayMatchKitBytes);

                this.teamRepository.Update(team);
            }

            await this.databaseContext.SaveAsync();
        }
    }
}