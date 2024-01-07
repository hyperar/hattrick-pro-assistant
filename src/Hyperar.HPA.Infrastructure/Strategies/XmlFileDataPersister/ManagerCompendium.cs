namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Hattrick = Application.Hattrick.ManagerCompendium;

    public class ManagerCompendium : XmlFileDataPersisterBase, IXmlFileDataPersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.ManagerAvatarLayer> managerAvatarLayerRepository;

        private readonly IHattrickRepository<Domain.Manager> managerRepository;

        private readonly IRepository<Domain.User> userRepository;

        private uint layerIndex = 1;

        public ManagerCompendium(
                    IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Country> countryRepository,
            IRepository<Domain.ManagerAvatarLayer> managerAvatarLayerRepository,
            IHattrickRepository<Domain.Manager> managerRepository,
            IRepository<Domain.User> userRepository)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.managerAvatarLayerRepository = managerAvatarLayerRepository;
            this.managerRepository = managerRepository;
            this.userRepository = userRepository;
        }

        public override async Task PersistDataAsync(IXmlFile file)
        {
            try
            {
                if (file is Hattrick.HattrickData entity)
                {
                    await this.ProcessManagerCompendiumAsync(entity);
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

        private async Task ProcessManagerAvatar(Hattrick.Avatar avatar, Domain.Manager manager)
        {
            manager.AvatarLayers.Add(new Domain.ManagerAvatarLayer
            {
                Index = layerIndex,
                XCoordinate = 0,
                YCoordinate = 0,
                ImageUrl = NormalizeUrl(avatar.BackgroundImage),
                Image = await DownloadWebResource(avatar.BackgroundImage)
            });

            foreach (var curLayer in avatar.Layers)
            {
                layerIndex++;

                manager.AvatarLayers.Add(new Domain.ManagerAvatarLayer
                {
                    Index = layerIndex,
                    XCoordinate = curLayer.X,
                    YCoordinate = curLayer.Y,
                    ImageUrl = NormalizeUrl(curLayer.Image),
                    Image = await DownloadWebResource(curLayer.Image)
                });
            }
        }

        private async Task ProcessManagerCompendiumAsync(Hattrick.HattrickData entity)
        {
            var manager = await this.managerRepository.GetByHattrickIdAsync(entity.Manager.UserId);
            var country = await this.countryRepository.GetByHattrickIdAsync(entity.Manager.Country.CountryId);

            ArgumentNullException.ThrowIfNull(country, nameof(country));

            if (manager == null)
            {
                var user = await this.userRepository.Query().SingleAsync();

                manager = new Domain.Manager
                {
                    HattrickId = entity.Manager.UserId,
                    SupporterTier = entity.Manager.SupporterTier,
                    UserName = entity.Manager.LoginName,
                    CurrencyName = entity.Manager.Currency.CurrencyName,
                    CurrencyRate = entity.Manager.Currency.CurrencyRate,
                    Country = country,
                    User = user
                };

                if (entity.Manager.Avatar != null)
                {
                    await this.ProcessManagerAvatar(entity.Manager.Avatar, manager);

                    manager.Avatar = BuildAvatarFromLayers(manager.AvatarLayers);
                }

                await this.managerRepository.InsertAsync(manager);
            }
            else
            {
                bool avatarPresentInXml = entity.Manager.Avatar != null;
                bool mustDeleteExistingAvatar = false;
                bool mustBuildAvatar = avatarPresentInXml && manager.AvatarLayers.Count == 0;

                if (avatarPresentInXml)
                {
                    // Just to avoid possible NullReferenceException warning, we just checked this.
                    ArgumentNullException.ThrowIfNull(entity.Manager.Avatar, nameof(entity.Manager.Avatar));

                    var xmlAvatarLayers = new List<string>(entity.Manager.Avatar.Layers.Select(x => NormalizeUrl(x.Image)).ToArray())
                    {
                        NormalizeUrl(entity.Manager.Avatar.BackgroundImage),
                    };

                    mustDeleteExistingAvatar = manager.AvatarLayers.Select(x => x.ImageUrl)
                                                                   .Except(xmlAvatarLayers)
                                                                   .Any();
                    mustBuildAvatar = true;
                }
                else
                {
                    mustDeleteExistingAvatar = manager.AvatarLayers.Count > 0;
                }

                if (mustDeleteExistingAvatar)
                {
                    var layerIdsToDelete = manager.AvatarLayers.Select(x => x.Id).ToList();

                    foreach (var layerId in layerIdsToDelete)
                    {
                        await this.managerAvatarLayerRepository.DeleteAsync(layerId);
                    }

                    manager.Avatar = null;
                }

                if (mustBuildAvatar)
                {
                    // Just to avoid possible NullReferenceException warning, we just checked this.
                    ArgumentNullException.ThrowIfNull(entity.Manager.Avatar, nameof(entity.Manager.Avatar));

                    await this.ProcessManagerAvatar(entity.Manager.Avatar, manager);

                    manager.Avatar = BuildAvatarFromLayers(manager.AvatarLayers);
                }

                manager.SupporterTier = entity.Manager.SupporterTier;
                manager.UserName = entity.Manager.LoginName;
                manager.CurrencyName = entity.Manager.Currency.CurrencyName;
                manager.CurrencyRate = entity.Manager.Currency.CurrencyRate;

                this.managerRepository.Update(manager);
            }

            await this.databaseContext.SaveAsync();
        }
    }
}