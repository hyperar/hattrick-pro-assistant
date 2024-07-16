namespace Hyperar.HPA.Infrastructure.Features.Download.Extract.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Infrastructure.Features.Download.Extract.Constants;
    using Shared.Enums;
    using Shared.ExtensionMethods;
    using Shared.Models.Hattrick.Matches;
    using Shared.Models.UI.Download;

    public class Matches : IExtractorStrategy
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

        public async Task ExtractAsync(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            CancellationToken cancellationToken)
        {
            if (task.XmlFile is HattrickData file)
            {
                MatchType[] hattrickArenaMatchTypes = new MatchType[]
                {
                    MatchType.TournamentPlayoff,
                    MatchType.TournamentLeague,
                    MatchType.Ladder,
                    MatchType.Duel
                };

                foreach (var xmlMatch in file.Team.MatchList.Where(x => x.Status.ToMatchStatus() != MatchStatus.Finished))
                {
                    bool isMatchArenaMatchType = hattrickArenaMatchTypes.Contains((MatchType)xmlMatch.MatchType);

                    bool matchExistsInDatabase = (file.IsYouth && await this.juniorUpcomingMatchRepository.GetByHattrickIdAsync(xmlMatch.MatchId) != null) ||
                           (!file.IsYouth && await this.seniorUpcomingMatchRepository.GetByHattrickIdAsync(xmlMatch.MatchId) != null);

                    bool shouldDownloadMatchFiles = (downloadSettings.DownloadHattrickArenaMatches || !isMatchArenaMatchType)
                        && (!matchExistsInDatabase || downloadSettings.ReDownloadMatchEvents);

                    if (!shouldDownloadMatchFiles)
                    {
                        continue;
                    }

                    taskList.Add(
                        new XmlDownloadTask(
                            XmlFileType.MatchDetails,
                            task.ContextId,
                            new NameValueCollection
                            {
                                { QueryStringParameter.MatchEvents, bool.TrueString },
                                { QueryStringParameter.MatchId, xmlMatch.MatchId.ToString() },
                                { QueryStringParameter.SourceSystem, xmlMatch.SourceSystem }
                            }));
                }
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }
    }
}