namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models = Shared.Models.Hattrick;

    public class PlayerDetails : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.Senior.Player> playerRepository;

        private readonly IRepository<Domain.Senior.PlayerSkillSet> playerSkillSetRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        public PlayerDetails(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.Senior.Player> playerRepository,
            IRepository<Domain.Senior.PlayerSkillSet> playerSkillSetRepository,
            IHattrickRepository<Domain.Senior.Team> teamRepository)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.playerRepository = playerRepository;
            this.playerSkillSetRepository = playerSkillSetRepository;
            this.teamRepository = teamRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.PlayerDetails.HattrickData file)
            {
                var team = await this.teamRepository.GetByHattrickIdAsync(file.Player.OwningTeam.TeamId);

                ArgumentNullException.ThrowIfNull(team, nameof(team));

                var player = await this.ProcessPlayerAsync(file.Player, team);

                await this.ProcessPlayerSkillSetAsync(
                    file.Player,
                    team.League.Season,
                    team.League.Week,
                    player);
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.PlayerDetails.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task<Domain.Senior.Player> ProcessPlayerAsync(
            Models.PlayerDetails.Player xmlPlayer,
            Domain.Senior.Team team)
        {
            var player = await this.playerRepository.GetByHattrickIdAsync(xmlPlayer.PlayerId);

            if (player == null)
            {
                var country = await this.countryRepository.GetByHattrickIdAsync(xmlPlayer.NativeCountryId);

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
            Models.PlayerDetails.Player xmlPlayer,
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