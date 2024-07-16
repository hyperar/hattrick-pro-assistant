namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Models = Shared.Models.Hattrick;

    public class YouthTeamDetails : DownloadTaskStrategyBase, IPersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Junior.Team> juniorTeamRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> seniorTeamRepository;

        public YouthTeamDetails(
            IHattrickRepository<Domain.Junior.Team> juniorTeamRepository,
            IHattrickRepository<Domain.Senior.Team> seniorTeamRepository)
        {
            this.juniorTeamRepository = juniorTeamRepository;
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            if (task.XmlFile is Models.YouthTeamDetails.HattrickData file)
            {
                await this.ProcessJuniorTeamAsync(file.YouthTeam, cancellationToken);
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }

        private async Task ProcessJuniorTeamAsync(
            Models.YouthTeamDetails.YouthTeam xmlYouthTeam,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(xmlYouthTeam.OwningTeam, nameof(xmlYouthTeam.OwningTeam));

            var juniorTeam = await this.juniorTeamRepository.GetByHattrickIdAsync(xmlYouthTeam.YouthTeamId);

            if (juniorTeam == null)
            {
                var seniorTeam = await this.seniorTeamRepository.GetByHattrickIdAsync(xmlYouthTeam.OwningTeam.Id);

                ArgumentNullException.ThrowIfNull(seniorTeam, nameof(seniorTeam));

                await this.juniorTeamRepository.InsertAsync(
                    new Domain.Junior.Team
                    {
                        HattrickId = xmlYouthTeam.YouthTeamId,
                        Name = xmlYouthTeam.YouthTeamName,
                        ShortName = xmlYouthTeam.ShortTeamName,
                        FoundedOn = xmlYouthTeam.CreatedDate,
                        CanBookFriendlyOn = xmlYouthTeam.NextTrainingMatchDate,
                        TrainerPlayerHattrickId = xmlYouthTeam.YouthTrainer.YouthPlayerId,
                        SeniorTeam = seniorTeam
                    });
            }
            else
            {
                juniorTeam.Name = xmlYouthTeam.YouthTeamName;
                juniorTeam.ShortName = xmlYouthTeam.ShortTeamName;
                juniorTeam.FoundedOn = xmlYouthTeam.CreatedDate;
                juniorTeam.CanBookFriendlyOn = xmlYouthTeam.NextTrainingMatchDate;
                juniorTeam.TrainerPlayerHattrickId = xmlYouthTeam.YouthTrainer.YouthPlayerId;

                this.juniorTeamRepository.Update(juniorTeam);
            }
        }
    }
}