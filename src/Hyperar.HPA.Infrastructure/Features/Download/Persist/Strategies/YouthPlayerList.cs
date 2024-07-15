namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models = Shared.Models.Hattrick;

    public class YouthPlayerList : IPersisterStrategy
    {
        private IHattrickRepository<Domain.Junior.Player> juniorPlayerRepository;

        public YouthPlayerList(IHattrickRepository<Domain.Junior.Player> juniorPlayerRepository)
        {
            this.juniorPlayerRepository = juniorPlayerRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            if (task.XmlFile is Models.YouthPlayerList.HattrickData file)
            {
                var currentPlayerHattrickIds = file.YouthPlayerList.Select(x => x.YouthPlayerId);

                var youthPlayerListToDelete = await this.juniorPlayerRepository.Query(x => !currentPlayerHattrickIds.Contains(x.HattrickId))
                    .ToListAsync();

                if (youthPlayerListToDelete.Count != 0)
                {
                    this.juniorPlayerRepository.DeleteRange(youthPlayerListToDelete);
                }
            }
        }
    }
}