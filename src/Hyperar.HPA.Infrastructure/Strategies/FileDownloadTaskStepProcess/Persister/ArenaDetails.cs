namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Models = Shared.Models.Hattrick;

    public class ArenaDetails : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.Senior.TeamArena> teamArenaRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        public ArenaDetails(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Senior.TeamArena> teamArenaRepository,
            IHattrickRepository<Domain.Senior.Team> teamRepository)
        {
            this.databaseContext = databaseContext;
            this.teamArenaRepository = teamArenaRepository;
            this.teamRepository = teamRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.ArenaDetails.HattrickData file)
            {
                var team = await this.teamRepository.GetByHattrickIdAsync(file.Arena.Team.Id);

                ArgumentNullException.ThrowIfNull(team, nameof(team));

                await this.ProcessArenaAsync(file.Arena, team);
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.ArenaDetails.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessArenaAsync(Models.ArenaDetails.Arena xmlArena, Domain.Senior.Team team)
        {
            var arena = await this.teamArenaRepository.GetByHattrickIdAsync(xmlArena.ArenaId);

            byte[] imageBytes = await GetImageBytesFromCacheAsync(
                xmlArena.ArenaImage,
                xmlArena.ArenaFallbackImage);

            if (arena == null)
            {
                arena = Domain.Senior.TeamArena.Create(
                    xmlArena,
                    imageBytes,
                    team);

                await this.teamArenaRepository.InsertAsync(arena);
            }
            else if (arena.HasChanged(
                xmlArena,
                imageBytes))
            {
                arena.Update(xmlArena, imageBytes);

                this.teamArenaRepository.Update(arena);
            }
        }
    }
}