namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Shared.ExtensionMethods;
    using Models = Shared.Models.Hattrick;

    public class ManagerCompendium : DownloadTaskStrategyBase, IPersisterStrategy
    {
        private const string LeagueFlagImageUrlMask = "/Img/flags/{0}.png";

        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IHattrickRepository<Domain.Manager> managerRepository;

        private readonly IRepository<Domain.User> userRepository;

        public ManagerCompendium(
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.Manager> managerRepository,
            IRepository<Domain.User> userRepository)
        {
            this.countryRepository = countryRepository;
            this.managerRepository = managerRepository;
            this.userRepository = userRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            if (task.XmlFile is Models.ManagerCompendium.HattrickData file)
            {
                await this.ProcessManagerAsync(file.Manager, cancellationToken);
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }

        private async Task ProcessManagerAsync(Models.ManagerCompendium.Manager xmlManager, CancellationToken cancellationToken)
        {
            var manager = await this.managerRepository.GetByHattrickIdAsync(xmlManager.UserId);

            ArgumentNullException.ThrowIfNull(xmlManager.Avatar, nameof(xmlManager.Avatar));

            var avatarBytes = await BuildAvatarFromLayersAsync(xmlManager.Avatar);

            ArgumentNullException.ThrowIfNull(avatarBytes, nameof(avatarBytes));

            if (manager == null)
            {
                var country = await this.countryRepository.GetByHattrickIdAsync(xmlManager.Country.Id);

                ArgumentNullException.ThrowIfNull(country, nameof(country));

                var user = await this.userRepository.Query().SingleAsync();

                await this.managerRepository.InsertAsync(
                    new Domain.Manager
                    {
                        HattrickId = xmlManager.UserId,
                        UserName = xmlManager.LoginName,
                        SupporterTier = xmlManager.SupporterTier.ToSupporterTier(),
                        CurrencyName = xmlManager.Currency.CurrencyName,
                        CurrencyRate = xmlManager.Currency.CurrencyRate,
                        AvatarBytes = avatarBytes,
                        Country = country,
                        User = user
                    });
            }
            else
            {
                manager.UserName = xmlManager.LoginName;
                manager.SupporterTier = xmlManager.SupporterTier.ToSupporterTier();
                manager.CurrencyName = xmlManager.Currency.CurrencyName;
                manager.CurrencyRate = xmlManager.Currency.CurrencyRate;
                manager.AvatarBytes = avatarBytes;

                this.managerRepository.Update(manager);
            }
        }
    }
}