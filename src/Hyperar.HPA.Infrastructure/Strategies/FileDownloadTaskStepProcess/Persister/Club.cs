namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Models = Shared.Models.Hattrick;

    public class Club : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        public Club(IDatabaseContext databaseContext, IHattrickRepository<Domain.Senior.Team> teamRepository)
        {
            this.databaseContext = databaseContext;
            this.teamRepository = teamRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.Club.HattrickData file)
            {
                await this.ProcessTeamAsync(file.Team);
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.Club.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessTeamAsync(Models.Club.Team xmlTeam)
        {
            var team = await this.teamRepository.GetByHattrickIdAsync(xmlTeam.TeamId);

            ArgumentNullException.ThrowIfNull(team, nameof(team));

            team.HasPromotedJuniorPlayer = xmlTeam.YouthSquad.HasPromoted;

            this.teamRepository.Update(team);
        }
    }
}