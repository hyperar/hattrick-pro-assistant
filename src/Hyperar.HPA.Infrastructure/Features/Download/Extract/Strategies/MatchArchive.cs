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
    using Microsoft.EntityFrameworkCore;
    using Shared.Enums;
    using Shared.Models.Hattrick.MatchArchive;
    using Shared.Models.UI.Download;

    public class MatchArchive : IExtractorStrategy
    {
        private readonly IHattrickRepository<Domain.Junior.Match> juniorMatchRepository;

        private readonly IHattrickRepository<Domain.Senior.Match> seniorMatchRepository;

        public MatchArchive(
            IHattrickRepository<Domain.Junior.Match> juniorMatchRepository,
            IHattrickRepository<Domain.Senior.Match> seniorMatchRepository)
        {
            this.juniorMatchRepository = juniorMatchRepository;
            this.seniorMatchRepository = seniorMatchRepository;
        }

        public async Task ExtractAsync(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(task.ContextId, nameof(task.ContextId));

            if (task.XmlFile is HattrickData file)
            {
                MatchType[] hattrickArenaMatchTypes = new MatchType[]
                {
                    MatchType.TournamentPlayoff,
                    MatchType.TournamentLeague,
                    MatchType.Ladder,
                    MatchType.Duel
                };

                foreach (var xmlMatch in file.Team.MatchList)
                {
                    bool isMatchArenaMatchType = hattrickArenaMatchTypes.Contains((MatchType)xmlMatch.MatchType);

                    bool matchExistsInDatabase = (file.IsYouth && await this.juniorMatchRepository.GetByHattrickIdAsync(xmlMatch.MatchId) != null) ||
                           (!file.IsYouth && await this.seniorMatchRepository.GetByHattrickIdAsync(xmlMatch.MatchId) != null);

                    bool shouldDownloadMatchFiles = (downloadSettings.DownloadHattrickArenaMatches || !isMatchArenaMatchType)
                        && (!matchExistsInDatabase || downloadSettings.ReDownloadMatchEvents);

                    if (!shouldDownloadMatchFiles)
                    {
                        continue;
                    }

                    taskList.Add(
                        new XmlDownloadTask(
                            XmlFileType.MatchDetails,
                            file.Team.TeamId,
                            new NameValueCollection
                            {
                                { QueryStringParameter.MatchEvents, bool.TrueString },
                                { QueryStringParameter.MatchId, xmlMatch.MatchId.ToString() },
                                { QueryStringParameter.SourceSystem, xmlMatch.SourceSystem }
                            }));

                    taskList.Add(
                        new XmlDownloadTask(
                            XmlFileType.MatchLineUp,
                            file.Team.TeamId,
                            new NameValueCollection
                            {
                                { QueryStringParameter.MatchId, xmlMatch.MatchId.ToString() },
                                { QueryStringParameter.TeamId, xmlMatch.HomeTeam.Id.ToString() },
                                { QueryStringParameter.SourceSystem, xmlMatch.SourceSystem }
                            }));

                    taskList.Add(
                        new XmlDownloadTask(
                            XmlFileType.MatchLineUp,
                            file.Team.TeamId,
                            new NameValueCollection
                            {
                                { QueryStringParameter.MatchId, xmlMatch.MatchId.ToString() },
                                { QueryStringParameter.TeamId, xmlMatch.AwayTeam.Id.ToString() },
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