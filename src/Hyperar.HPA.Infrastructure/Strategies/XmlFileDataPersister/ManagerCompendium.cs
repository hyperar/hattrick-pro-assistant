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

        public void PersistData(IXmlFile file)
        {
            var entity = (HattrickData)file;

            this.ProcessManagerCompendium(entity);
        }

        private void ProcessManagerCompendium(HattrickData entity)
        {
            var manager = this.managerRepository.GetByHattrickId(entity.Manager.UserId);
            var country = this.countryRepository.GetByHattrickId(entity.Manager.Country.CountryId);

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

                    this.managerRepository.Insert(manager);
                }
                else
                {
                    manager.SupporterTier = entity.Manager.SupporterTier;
                    manager.UserName = entity.Manager.LoginName;
                    manager.CurrencyName = entity.Manager.Currency.CurrencyName;
                    manager.CurrencyRate = entity.Manager.Currency.CurrencyRate;

                    this.managerRepository.Update(manager);
                }

                this.context.Save();
            }
            else
            {
                throw new Exception($"Country with Hattrick ID \"{entity.Manager.Country.CountryId}\" not found.");
            }
        }
    }
}