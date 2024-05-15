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
        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.Senior.PlayerMatch> playerMatchRepository;

        private readonly IHattrickRepository<Domain.Senior.Player> playerRepository;

        public PlayerDetails(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Senior.Player> playerRepository,
            IRepository<Domain.Senior.PlayerMatch> playerMatchRepository)
        {
            this.databaseContext = databaseContext;
            this.playerRepository = playerRepository;
            this.playerMatchRepository = playerMatchRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.PlayerDetails.HattrickData file)
            {
                var player = await this.playerRepository.GetByHattrickIdAsync(file.Player.PlayerId);

                ArgumentNullException.ThrowIfNull(player, nameof(player));

                if (file.Player.LastMatch != null)
                {
                    await this.ProcessPlayerMatchAsync(file.Player.LastMatch, player);
                }
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

        private async Task ProcessPlayerMatchAsync(
            Models.PlayerDetails.LastMatch xmlMatch,
            Domain.Senior.Player player)
        {
            var playerMatch = await this.playerMatchRepository.Query(x => x.PlayerHattrickId == player.HattrickId
                                                                       && x.MatchHattrickId == xmlMatch.MatchId)
                                                              .SingleOrDefaultAsync();

            //if (playerMatch == null)
            //{
            //    await this.playerMatchRepository.InsertAsync(
            //        Domain.Senior.PlayerMatch.Create(
            //            xmlMatch,
            //            player));
            //}
        }
    }
}