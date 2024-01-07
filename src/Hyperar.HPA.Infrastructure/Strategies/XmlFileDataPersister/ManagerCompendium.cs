namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using Application.Hattrick.Interfaces;
    using Hattrick = Application.Hattrick.ManagerCompendium;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ManagerCompendium : IXmlFileDataPersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.Manager> managerRepository;

        private readonly IRepository<Domain.User> userRepository;

        public ManagerCompendium(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.Manager> managerRepository,
            IRepository<Domain.User> userRepository)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.managerRepository = managerRepository;
            this.userRepository = userRepository;
        }

        public async Task PersistDataAsync(IXmlFile file)
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

                await this.managerRepository.InsertAsync(manager);
            }
            else
            {
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