namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Models = Shared.Models.Hattrick;

    public class Avatars : DownloadTaskStrategyBase, IPersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Senior.Player> playerRepository;

        public Avatars(IHattrickRepository<Domain.Senior.Player> playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            if (task.XmlFile is Models.Avatars.HattrickData file)
            {
                foreach (var xmlPlayer in file.Team.Players)
                {
                    await this.ProcessPlayerAsync(xmlPlayer, cancellationToken);
                }
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }

        private async Task ProcessPlayerAsync(Models.Avatars.Player xmlPlayer, CancellationToken cancellationToken)
        {
            var player = await this.playerRepository.GetByHattrickIdAsync(xmlPlayer.PlayerId);

            if (player == null)
            {
                return;
            }

            byte[] avatarBytes = await BuildAvatarFromLayersAsync(xmlPlayer.Avatar);

            player.AvatarBytes = avatarBytes;

            this.playerRepository.Update(player);
        }
    }
}