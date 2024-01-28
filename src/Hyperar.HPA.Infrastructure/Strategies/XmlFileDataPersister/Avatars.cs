namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Hattrick = Application.Hattrick.Avatars;

    public class Avatars : XmlFileDataPersisterBase, IXmlFileDataPersisterStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.SeniorPlayerAvatarLayer> seniorPlayerAvatarLayerRepository;

        private readonly IHattrickRepository<Domain.SeniorPlayer> seniorPlayerRepository;

        public Avatars(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.SeniorPlayer> seniorPlayerRepository,
            IRepository<Domain.SeniorPlayerAvatarLayer> seniorPlayerAvatarLayerRepository)
        {
            this.databaseContext = databaseContext;
            this.seniorPlayerRepository = seniorPlayerRepository;
            this.seniorPlayerAvatarLayerRepository = seniorPlayerAvatarLayerRepository;
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

        private static void ProcessSeniorPlayerAvatar(Hattrick.Avatar avatar, Domain.SeniorPlayer seniorPlayer)
        {
            uint layerIndex = 1;

            seniorPlayer.AvatarLayers.Add(new Domain.SeniorPlayerAvatarLayer
            {
                Index = layerIndex,
                XCoordinate = 0,
                YCoordinate = 0,
                ImageUrl = NormalizeUrl(avatar.BackgroundImage),
            });

            foreach (var curLayer in avatar.Layers)
            {
                layerIndex++;

                seniorPlayer.AvatarLayers.Add(new Domain.SeniorPlayerAvatarLayer
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
            foreach (var curPlayer in xmlEntity.Team.Players)
            {
                var seniorPlayer = await this.seniorPlayerRepository.GetByHattrickIdAsync(curPlayer.PlayerId);

                ArgumentNullException.ThrowIfNull(seniorPlayer, nameof(seniorPlayer));

                bool mustDeleteAvatar = false;

                var xmlAvatarLayers = new List<string>(curPlayer.Avatar.Layers.Select(x => NormalizeUrl(x.Image)).ToArray())
                {
                    NormalizeUrl(curPlayer.Avatar.BackgroundImage),
                };

                mustDeleteAvatar = seniorPlayer.AvatarLayers.Select(x => x.ImageUrl)
                                                            .Except(xmlAvatarLayers)
                                                            .Any();

                if (mustDeleteAvatar)
                {
                    var layerIdsToDelete = seniorPlayer.AvatarLayers.Select(x => x.Id).ToList();

                    await this.seniorPlayerAvatarLayerRepository.DeleteRangeAsync(layerIdsToDelete);

                    seniorPlayer.Avatar = Array.Empty<byte>();

                    await this.databaseContext.SaveAsync();
                }

                if (seniorPlayer.AvatarLayers.Count == 0)
                {
                    ProcessSeniorPlayerAvatar(curPlayer.Avatar, seniorPlayer);

                    seniorPlayer.Avatar = await BuildAvatarFromLayers(seniorPlayer.AvatarLayers);
                }
            }

            await this.databaseContext.SaveAsync();
        }
    }
}