namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models = Shared.Models.Hattrick;

    public class Players : IPersisterStrategy
    {
        private IHattrickRepository<Domain.Senior.Player> seniorPlayerRepository;

        public Players(IHattrickRepository<Domain.Senior.Player> seniorPlayerRepository)
        {
            this.seniorPlayerRepository = seniorPlayerRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            if (task.XmlFile is Models.Players.HattrickData file)
            {
                var currentPlayerHattrickIds = file.Team.PlayerList.Select(x => x.PlayerId);

                var playersToDelete = await this.seniorPlayerRepository.Query(x => x.TeamHattrickId == file.Team.TeamId
                                                                                && !currentPlayerHattrickIds.Contains(x.HattrickId))
                    .ToListAsync();

                if (playersToDelete.Count > 0)
                {
                    this.seniorPlayerRepository.DeleteRange(playersToDelete);
                }
            }
        }
    }
}