namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models = Shared.Models.Hattrick;

    public class Players : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.Senior.PlayerMatch> playerMatchRepository;

        private readonly IHattrickRepository<Domain.Senior.Player> playerRepository;

        private readonly IRepository<Domain.Senior.PlayerSkillSet> playerSkillSetRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        public Players(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Senior.Player> playerRepository,
            IRepository<Domain.Senior.PlayerMatch> playerMatchRepository,
            IRepository<Domain.Senior.PlayerSkillSet> playerSkillSetRepository,
            IHattrickRepository<Domain.Senior.Team> teamRepository)
        {
            this.databaseContext = databaseContext;
            this.playerRepository = playerRepository;
            this.playerMatchRepository = playerMatchRepository;
            this.playerSkillSetRepository = playerSkillSetRepository;
            this.teamRepository = teamRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.Players.HattrickData file)
            {
                var team = await this.teamRepository.GetByHattrickIdAsync(file.Team.TeamId);

                ArgumentNullException.ThrowIfNull(team, nameof(team));

                var xmlPlayerIds = file.Team.PlayerList.Select(x => x.PlayerId);

                var playerIdsToDelete = await this.playerRepository.Query(x => !xmlPlayerIds.Contains(x.HattrickId)
                                                                            && x.TeamHattrickId == file.Team.TeamId)
                                                                   .Select(x => x.HattrickId)
                                                                   .ToListAsync(cancellationToken);

                // Delete former players.
                if (playerIdsToDelete.Count > 0)
                {
                    var playerSkillSetIdsToDelete = await this.playerSkillSetRepository.Query(x => playerIdsToDelete.Contains(x.PlayerHattrickId))
                                                                                       .Select(x => x.Id)
                                                                                       .ToListAsync(cancellationToken);

                    var playerMatchIdsToDelete = await this.playerMatchRepository.Query(x => playerIdsToDelete.Contains(x.PlayerHattrickId))
                                                                                 .Select(x => x.Id)
                                                                                 .ToListAsync(cancellationToken);

                    await this.playerMatchRepository.DeleteRangeAsync(playerMatchIdsToDelete);
                    await this.playerSkillSetRepository.DeleteRangeAsync(playerSkillSetIdsToDelete);
                    await this.playerRepository.DeleteRangeAsync(playerIdsToDelete);
                }
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.Players.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }
    }
}