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
                if (file is Hattrick.HattrickData xmlEntity)
                {
                    await this.ProcessManagerCompendiumAsync(xmlEntity);
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

        private static void ProcessManagerAvatar(Hattrick.Avatar avatar, Domain.Manager manager)
        {
            uint layerIndex = 1;

            manager.AvatarLayers.Add(new Domain.ManagerAvatarLayer
            {
                Index = layerIndex,
                XCoordinate = 0,
                YCoordinate = 0,
                ImageUrl = NormalizeUrl(avatar.BackgroundImage),
            });

            foreach (var curLayer in avatar.Layers)
            {
                layerIndex++;

                manager.AvatarLayers.Add(new Domain.ManagerAvatarLayer
                {
                    Index = layerIndex,
                    XCoordinate = curLayer.X,
                    YCoordinate = curLayer.Y,
                    ImageUrl = NormalizeUrl(curLayer.Image)
                });
            }
        }

        private async Task ProcessManagerCompendiumAsync(Hattrick.HattrickData xmlEntity)
        {
            var country = await this.countryRepository.GetByHattrickIdAsync(xmlEntity.Manager.Country.CountryId);

            ArgumentNullException.ThrowIfNull(country, nameof(country));

            var manager = await this.managerRepository.GetByHattrickIdAsync(xmlEntity.Manager.UserId);

            if (manager == null)
            {
                var user = await this.userRepository.Query().SingleAsync();

                ArgumentNullException.ThrowIfNull(user, nameof(user));

                manager = new Domain.Manager
                {
                    HattrickId = xmlEntity.Manager.UserId,
                    SupporterTier = xmlEntity.Manager.SupporterTier,
                    UserName = xmlEntity.Manager.LoginName,
                    CurrencyName = xmlEntity.Manager.Currency.CurrencyName,
                    CurrencyRate = xmlEntity.Manager.Currency.CurrencyRate,
                    Country = country,
                    User = user
                };

                if (xmlEntity.Manager.Avatar != null)
                {
                    ProcessManagerAvatar(xmlEntity.Manager.Avatar, manager);

                    manager.AvatarBytes = await BuildAvatarFromLayers(manager.AvatarLayers);
                }

                await this.managerRepository.InsertAsync(manager);
            }
            else
            {
                bool mustDeleteAvatar = false;

                if (xmlEntity.Manager.Avatar == null)
                {
                    mustDeleteAvatar = manager.AvatarLayers.Count > 0;
                }
                else
                {
                    var xmlAvatarLayers = new List<string>(xmlEntity.Manager.Avatar.Layers.Select(x => NormalizeUrl(x.Image)).ToArray())
                    {
                        NormalizeUrl(xmlEntity.Manager.Avatar.BackgroundImage),
                    };

                    mustDeleteAvatar = manager.AvatarLayers.Select(x => x.ImageUrl)
                                                           .Except(xmlAvatarLayers)
                                                           .Any();
                }

                if (mustDeleteAvatar)
                {
                    var layerIdsToDelete = manager.AvatarLayers.Select(x => x.Id).ToList();

                    await this.managerAvatarLayerRepository.DeleteRangeAsync(layerIdsToDelete);

                    manager.AvatarBytes = null;

                    await this.databaseContext.SaveAsync();
                }

                if (xmlEntity.Manager.Avatar != null && manager.AvatarLayers.Count == 0)
                {
                    ProcessManagerAvatar(xmlEntity.Manager.Avatar, manager);

                    manager.AvatarBytes = await BuildAvatarFromLayers(manager.AvatarLayers);
                }

                manager.SupporterTier = xmlEntity.Manager.SupporterTier;
                manager.UserName = xmlEntity.Manager.LoginName;
                manager.CurrencyName = xmlEntity.Manager.Currency.CurrencyName;
                manager.CurrencyRate = xmlEntity.Manager.Currency.CurrencyRate;

                this.managerRepository.Update(manager);
            }

            await this.databaseContext.SaveAsync();
        }
    }
}