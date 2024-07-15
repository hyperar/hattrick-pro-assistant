namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Models = Shared.Models.Hattrick;

    public class YouthAvatars : DownloadTaskStrategyBase, IPersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Junior.Player> juniorPlayerRepository;

        public YouthAvatars(IHattrickRepository<Domain.Junior.Player> juniorPlayerRepository)
        {
            this.juniorPlayerRepository = juniorPlayerRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            if (task.XmlFile is Models.YouthAvatars.HattrickData file)
            {
                foreach (var xmlPlayer in file.YouthTeam.YouthPlayers)
                {
                    await this.ProcessYouthPlayerAsync(xmlPlayer, cancellationToken);
                }
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }

        private async Task ProcessYouthPlayerAsync(Models.YouthAvatars.YouthPlayer xmlPlayer, CancellationToken cancellationToken)
        {
            var player = await this.juniorPlayerRepository.GetByHattrickIdAsync(xmlPlayer.PlayerId);

            if (player == null)
            {
                return;
            }

            byte[] avatarBytes = await BuildAvatarFromLayersAsync(xmlPlayer.Avatar);

            player.AvatarBytes = avatarBytes;

            this.juniorPlayerRepository.Update(player);
        }
    }
}