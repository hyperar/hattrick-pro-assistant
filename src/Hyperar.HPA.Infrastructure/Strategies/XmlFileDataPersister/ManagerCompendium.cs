namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Hattrick.ManagerCompendium;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Domain.Interfaces;

    public class ManagerCompendium : IXmlFileDataPersisterStrategy
    {
        private readonly IDatabaseContext context;

        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IHattrickRepository<Domain.Manager> managerRepository;

        public ManagerCompendium(
            IDatabaseContext context,
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.Manager> managerRepository)
        {
            this.context = context;
            this.countryRepository = countryRepository;
            this.managerRepository = managerRepository;
        }

        public async Task PersistDataAsync(IXmlFile file)
        {
            var entity = (HattrickData)file;

            await this.ProcessManagerCompendiumAsync(entity);
        }

        private async Task ProcessManagerCompendiumAsync(HattrickData entity)
        {
            var manager = await this.managerRepository.GetByHattrickIdAsync(entity.Manager.UserId);
            var country = await this.countryRepository.GetByHattrickIdAsync(entity.Manager.Country.CountryId);

            if (country != null)
            {
                if (manager == null)
                {
                    manager = new Domain.Manager
                    {
                        HattrickId = entity.Manager.UserId,
                        SupporterTier = entity.Manager.SupporterTier,
                        UserName = entity.Manager.LoginName,
                        CurrencyName = entity.Manager.Currency.CurrencyName,
                        CurrencyRate = entity.Manager.Currency.CurrencyRate,
                        Country = country
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

                await this.context.SaveAsync();
            }
            else
            {
                throw new Exception($"Country with Hattrick ID \"{entity.Manager.Country.CountryId}\" not found.");
            }
        }
    }
}