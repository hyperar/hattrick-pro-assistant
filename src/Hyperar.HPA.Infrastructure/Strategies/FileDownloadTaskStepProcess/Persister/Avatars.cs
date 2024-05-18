namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Models = Shared.Models.Hattrick;

    public class Avatars : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.Senior.Player> playerRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        public Avatars(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Senior.Player> playerRepository,
            IHattrickRepository<Domain.Senior.Team> teamRepository)
        {
            this.databaseContext = databaseContext;
            this.playerRepository = playerRepository;
            this.teamRepository = teamRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.Avatars.HattrickData file)
            {
                var team = await this.teamRepository.GetByHattrickIdAsync(file.Team.TeamId);

                ArgumentNullException.ThrowIfNull(team, nameof(team));

                foreach (var xmlPlayer in file.Team.Players.Where(x => x.PlayerId != team.TrainerHattrickId))
                {
                    await this.ProcessPlayerAsync(xmlPlayer);
                }
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.Avatars.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessPlayerAsync(Models.Avatars.Player xmlPlayer)
        {
            var player = await this.playerRepository.GetByHattrickIdAsync(xmlPlayer.PlayerId);

            ArgumentNullException.ThrowIfNull(player, nameof(player));

            byte[] avatarBytes = await BuildAvatarFromLayersAsync(xmlPlayer.Avatar);

            player.AvatarBytes = avatarBytes;

            this.playerRepository.Update(player);
        }
    }
}