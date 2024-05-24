namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models = Shared.Models.Hattrick;

    public class ManagerCompendium : PersisterBase, IFileDownloadTaskStepProcessStrategy
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

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.ManagerCompendium.HattrickData file)
            {
                await this.ProcessManagerAsync(file.Manager);
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.ManagerCompendium.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessManagerAsync(Models.ManagerCompendium.Manager xmlManager)
        {
            var manager = await this.managerRepository.GetByHattrickIdAsync(xmlManager.UserId);

            byte[] avatarBytes = xmlManager.Avatar != null
                               ? await BuildAvatarFromLayersAsync(xmlManager.Avatar)
                               : Array.Empty<byte>();

            if (manager == null)
            {
                var country = await this.countryRepository.GetByHattrickIdAsync(xmlManager.Country.Id);

                ArgumentNullException.ThrowIfNull(country, nameof(country));

                var user = await this.userRepository.Query().SingleOrDefaultAsync();

                ArgumentNullException.ThrowIfNull(user, nameof(user));

                await this.managerRepository.InsertAsync(
                    Domain.Manager.Create(
                        xmlManager,
                        avatarBytes,
                        country,
                        user));
            }
            else if (manager.HasChanged(xmlManager, avatarBytes))
            {
                manager.Update(xmlManager, avatarBytes);
            }
        }
    }
}