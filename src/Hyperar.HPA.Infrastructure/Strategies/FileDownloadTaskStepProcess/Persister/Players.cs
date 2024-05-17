namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models = Shared.Models.Hattrick;

    public class Players : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.Senior.PlayerMatch> playerMatchRepository;

        private readonly IHattrickRepository<Domain.Senior.Player> playerRepository;

        private readonly IRepository<Domain.Senior.PlayerSkillSet> playerSkillSetRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        public Players(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.Senior.Player> playerRepository,
            IRepository<Domain.Senior.PlayerMatch> playerMatchRepository,
            IRepository<Domain.Senior.PlayerSkillSet> playerSkillSetRepository,
            IHattrickRepository<Domain.Senior.Team> teamRepository)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.playerRepository = playerRepository;
            this.playerMatchRepository = playerMatchRepository;
            this.playerSkillSetRepository = playerSkillSetRepository;
            this.teamRepository = teamRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.Players.HattrickData file)
            {
                var team = await this.teamRepository.GetByHattrickIdAsync(file.Team.TeamId);

                ArgumentNullException.ThrowIfNull(team, nameof(team));

                var xmlPlayerIds = file.Team.PlayerList.Select(x => x.PlayerId);

                var playerIdsToDelete = await this.playerRepository.Query(x => !xmlPlayerIds.Contains(x.HattrickId)
                                                                            && x.TeamHattrickId == file.Team.TeamId)
                                                                   .Select(x => x.HattrickId)
                                                                   .ToListAsync(cancellationToken);

                // Delete former players.
                if (playerIdsToDelete.Count > 0)
                {
                    var playerSkillSetIdsToDelete = await this.playerSkillSetRepository.Query(x => playerIdsToDelete.Contains(x.PlayerHattrickId))
                                                                                       .Select(x => x.Id)
                                                                                       .ToListAsync(cancellationToken);

                    var playerMatchIdsToDelete = await this.playerMatchRepository.Query(x => playerIdsToDelete.Contains(x.PlayerHattrickId))
                                                                                 .Select(x => x.Id)
                                                                                 .ToListAsync(cancellationToken);

                    await this.playerMatchRepository.DeleteRangeAsync(playerSkillSetIdsToDelete);
                    await this.playerSkillSetRepository.DeleteRangeAsync(playerSkillSetIdsToDelete);
                    await this.playerRepository.DeleteRangeAsync(playerIdsToDelete);
                }

                // Process current players.
                foreach (var xmlPlayer in file.Team.PlayerList)
                {
                    var player = await this.ProcessPlayerAsync(xmlPlayer, team);

                    await this.ProcessPlayerSkillSetAsync(
                        xmlPlayer,
                        team.League.Season,
                        team.League.Week,
                        player);
                }
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.Players.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task<Domain.Senior.Player> ProcessPlayerAsync(
            Models.Players.Player xmlPlayer,
            Domain.Senior.Team team)
        {
            var player = await this.playerRepository.GetByHattrickIdAsync(xmlPlayer.PlayerId);

            if (player == null)
            {
                var country = await this.countryRepository.GetByHattrickIdAsync(xmlPlayer.CountryId);

                ArgumentNullException.ThrowIfNull(country, nameof(country));

                player = await this.playerRepository.InsertAsync(
                    Domain.Senior.Player.Create(
                        xmlPlayer,
                        country,
                        team));
            }
            else if (player.HasChanged(xmlPlayer))
            {
                player.Update(xmlPlayer);

                this.playerRepository.Update(player);
            }

            return player;
        }

        private async Task ProcessPlayerSkillSetAsync(
            Models.Players.Player xmlPlayer,
            int season,
            int week,
            Domain.Senior.Player player)
        {
            var playerSkillSet = await this.playerSkillSetRepository.Query(x => x.PlayerHattrickId == xmlPlayer.PlayerId
                                                                             && x.Season == season
                                                                             && x.Week == week)
                                                                    .SingleOrDefaultAsync();

            if (playerSkillSet == null)
            {
                await this.playerSkillSetRepository.InsertAsync(
                    Domain.Senior.PlayerSkillSet.Create(
                        xmlPlayer,
                        player,
                        season,
                        week));
            }
            else if (playerSkillSet.HasChanged(xmlPlayer))
            {
                playerSkillSet.Update(xmlPlayer);

                this.playerSkillSetRepository.Update(playerSkillSet);
            }
        }
    }
}