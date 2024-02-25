namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Hattrick = Application.Hattrick.Avatars;

    public class Avatars : XmlFileDataPersisterBase, IXmlFileDataPersisterStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.Senior.PlayerAvatarLayer> playerAvatarLayerRepository;

        private readonly IHattrickRepository<Domain.Senior.Player> playerRepository;

        public Avatars(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Senior.Player> playerRepository,
            IRepository<Domain.Senior.PlayerAvatarLayer> playerAvatarLayerRepository)
        {
            this.databaseContext = databaseContext;
            this.playerRepository = playerRepository;
            this.playerAvatarLayerRepository = playerAvatarLayerRepository;
        }

        public override async Task PersistDataAsync(IXmlFile file)
        {
            try
            {
                if (file is Hattrick.HattrickData xmlEntity)
                {
                    await this.ProcessAvatarsAsync(xmlEntity);
                }
                else
                {
                    throw new ArgumentException(file.GetType().FullName, nameof(file));
                }
            }
            catch
            {
                this.databaseContext.Cancel();

                throw;
            }
        }

        private static void ProcessPlayerAvatar(Hattrick.Avatar avatar, Domain.Senior.Player player)
        {
            uint layerIndex = 1;

            player.AvatarLayers.Add(new Domain.Senior.PlayerAvatarLayer
            {
                Index = layerIndex,
                XCoordinate = 0,
                YCoordinate = 0,
                ImageUrl = NormalizeUrl(avatar.BackgroundImage),
            });

            foreach (Hattrick.Layer curLayer in avatar.Layers)
            {
                layerIndex++;

                player.AvatarLayers.Add(new Domain.Senior.PlayerAvatarLayer
                {
                    Index = layerIndex,
                    XCoordinate = curLayer.X,
                    YCoordinate = curLayer.Y,
                    ImageUrl = NormalizeUrl(curLayer.Image)
                });
            }
        }

        private async Task ProcessAvatarsAsync(Hattrick.HattrickData xmlEntity)
        {
            foreach (Hattrick.Player curPlayer in xmlEntity.Team.Players)
            {
                Domain.Senior.Player? player = await this.playerRepository.GetByHattrickIdAsync(curPlayer.PlayerId);

                ArgumentNullException.ThrowIfNull(player, nameof(player));

                bool mustDeleteAvatar = false;

                List<string> xmlAvatarLayers = new List<string>(curPlayer.Avatar.Layers.Select(x => NormalizeUrl(x.Image)).ToArray())
                {
                    NormalizeUrl(curPlayer.Avatar.BackgroundImage),
                };

                mustDeleteAvatar = player.AvatarLayers.Select(x => x.ImageUrl)
                                                      .Except(xmlAvatarLayers)
                                                      .Any();

                if (mustDeleteAvatar)
                {
                    List<int> layerIdsToDelete = player.AvatarLayers.Select(x => x.Id).ToList();

                    await this.playerAvatarLayerRepository.DeleteRangeAsync(layerIdsToDelete);

                    player.AvatarBytes = Array.Empty<byte>();

                    await this.databaseContext.SaveAsync();
                }

                if (player.AvatarLayers.Count == 0)
                {
                    ProcessPlayerAvatar(curPlayer.Avatar, player);

                    player.AvatarBytes = await BuildAvatarFromLayers(player.AvatarLayers);
                }
            }

            await this.databaseContext.SaveAsync();
        }
    }
}