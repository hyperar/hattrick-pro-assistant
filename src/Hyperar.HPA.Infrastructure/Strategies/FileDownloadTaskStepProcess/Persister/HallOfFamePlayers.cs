namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Models = Shared.Models.Hattrick;

    public class HallOfFamePlayers : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.Senior.HallOfFamePlayer> hallOfFamePlayerRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        public HallOfFamePlayers(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.Senior.HallOfFamePlayer> hallOfFamePlayer,
            IHattrickRepository<Domain.Senior.Team> teamRepository)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.hallOfFamePlayerRepository = hallOfFamePlayer;
            this.teamRepository = teamRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.HallOfFamePlayers.HattrickData file)
            {
                ArgumentNullException.ThrowIfNull(fileDownloadTask.ContextId, nameof(fileDownloadTask.ContextId));

                // TODO: Delete former Hall of Fame players.

                var team = await this.teamRepository.GetByHattrickIdAsync(fileDownloadTask.ContextId.Value);

                ArgumentNullException.ThrowIfNull(team, nameof(team));

                foreach (var xmlPlayer in file.PlayerList)
                {
                    await this.ProcessPlayerAsync(xmlPlayer, team);
                }
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.HallOfFamePlayers.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessPlayerAsync(Models.HallOfFamePlayers.Player xmlPlayer, Domain.Senior.Team team)
        {
            var hallOfFamePlayer = await this.hallOfFamePlayerRepository.GetByHattrickIdAsync(xmlPlayer.PlayerId);

            if (hallOfFamePlayer == null)
            {
                var country = await this.countryRepository.GetByHattrickIdAsync(xmlPlayer.CountryId);

                ArgumentNullException.ThrowIfNull(country, nameof(country));

                await this.hallOfFamePlayerRepository.InsertAsync(
                    Domain.Senior.HallOfFamePlayer.Create(
                        xmlPlayer,
                        country,
                        team));
            }
            else if (hallOfFamePlayer.HasChanged(xmlPlayer))
            {
                hallOfFamePlayer.Update(xmlPlayer);

                this.hallOfFamePlayerRepository.Update(hallOfFamePlayer);
            }
        }
    }
}