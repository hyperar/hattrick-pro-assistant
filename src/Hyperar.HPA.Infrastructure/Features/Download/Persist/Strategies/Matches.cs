namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Shared.Enums;
    using Shared.ExtensionMethods;
    using Models = Shared.Models.Hattrick;

    public class Matches : IPersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Junior.UpcomingMatch> juniorUpcomingMatchRepository;

        private readonly IHattrickRepository<Domain.Senior.UpcomingMatch> seniorUpcomingMatchRepository;

        public Matches(
            IHattrickRepository<Domain.Junior.UpcomingMatch> juniorUpcomingMatchRepository,
            IHattrickRepository<Domain.Senior.UpcomingMatch> seniorUpcomingMatchRepository)
        {
            this.juniorUpcomingMatchRepository = juniorUpcomingMatchRepository;
            this.seniorUpcomingMatchRepository = seniorUpcomingMatchRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(task.ContextId, nameof(task.ContextId));

            if (task.XmlFile is Models.Matches.HattrickData file)
            {
                var currentMatchIds = file.Team.MatchList.Where(x => x.Status.ToMatchStatus() == MatchStatus.Upcoming)
                    .Select(x => x.MatchId);

                if (file.IsYouth)
                {
                    var matchesToDelete = await this.juniorUpcomingMatchRepository.Query(x => x.TeamHattrickId == file.Team.TeamId
                                                                                           && !currentMatchIds.Contains(x.HattrickId))
                        .ToListAsync();

                    if (matchesToDelete.Count > 0)
                    {
                        this.juniorUpcomingMatchRepository.DeleteRange(matchesToDelete);
                    }
                }
                else
                {
                    var matchesToDelete = await this.seniorUpcomingMatchRepository.Query(x => x.TeamHattrickId == file.Team.TeamId
                                                                                           && !currentMatchIds.Contains(x.HattrickId))
                        .ToListAsync();

                    if (matchesToDelete.Count > 0)
                    {
                        this.seniorUpcomingMatchRepository.DeleteRange(matchesToDelete);
                    }
                }
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }
    }
}