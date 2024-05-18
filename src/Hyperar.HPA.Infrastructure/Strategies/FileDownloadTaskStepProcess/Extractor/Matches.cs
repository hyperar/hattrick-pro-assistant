namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Domain.Interfaces;
    using Shared.Enums;
    using Shared.ExtensionMethods;
    using Shared.Models.Hattrick.Matches;
    using Shared.Models.UI.Download;

    public class Matches : IFileDownloadTaskStepProcessStrategy
    {
        private const string MatchEventsParamKey = "matchEvents";

        private const string MatchIdParamKey = "matchId";

        private const string SourceSystemParamKey = "sourceSystem";

        private const string TeamIdParamKey = "teamId";

        private readonly IHattrickRepository<Domain.Senior.Match> matchRepository;

        public Matches(IHattrickRepository<Domain.Senior.Match> matchRepository)
        {
            this.matchRepository = matchRepository;
        }

        public async Task ExecuteAsync(
            IFileDownloadTask fileDownloadTask,
            IList<IFileDownloadTask> fileDownloadTasks,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(0, cancellationToken);

                if (fileDownloadTask is XmlFileDownloadTask xmlFileDownloadTask)
                {
                    ArgumentNullException.ThrowIfNull(xmlFileDownloadTask.XmlFile, nameof(xmlFileDownloadTask.XmlFile));

                    if (xmlFileDownloadTask.XmlFile is HattrickData file)
                    {
                        MatchType[] hattrickArenaMatchTypes = new MatchType[]
                        {
                            MatchType.TournamentPlayoff,
                            MatchType.TournamentLeague,
                            MatchType.Ladder,
                            MatchType.Duel
                        };

                        IEnumerable<Match> matches = file.Team.MatchList.ToList();

                        if (!downloadSettings.DownloadHattrickArenaMatches)
                        {
                            matches = matches.Where(x => !hattrickArenaMatchTypes.Contains((MatchType)x.MatchType));
                        }

                        foreach (Match? curMatch in matches.ToList())
                        {
                            Domain.Senior.Match? match = await this.matchRepository.GetByHattrickIdAsync(curMatch.MatchId);

                            if (match == null || downloadSettings.ReDownloadMatchEvents)
                            {
                                fileDownloadTasks.Add(
                                    new XmlFileDownloadTask(
                                        XmlFileType.MatchDetails,
                                        file.Team.TeamId,
                                        new NameValueCollection
                                        {
                                            { MatchIdParamKey, curMatch.MatchId.ToString() },
                                            { MatchEventsParamKey, bool.TrueString},
                                            { SourceSystemParamKey, curMatch.SourceSystem.ToString() }
                                        }));
                            }

                            if (curMatch.Status.ToMatchStatus() == MatchStatus.Finished && match == null)
                            {
                                fileDownloadTasks.Add(
                                    new XmlFileDownloadTask(
                                        XmlFileType.MatchLineUp,
                                        curMatch.MatchContextId,
                                        new NameValueCollection
                                        {
                                            { MatchIdParamKey, curMatch.MatchId.ToString() },
                                            { TeamIdParamKey, curMatch.HomeTeam.HomeTeamId.ToString() },
                                            { SourceSystemParamKey, curMatch.SourceSystem }
                                        }));

                                fileDownloadTasks.Add(
                                    new XmlFileDownloadTask(
                                        XmlFileType.MatchLineUp,
                                        xmlFileDownloadTask.ContextId,
                                        new NameValueCollection
                                        {
                                            { MatchIdParamKey, curMatch.MatchId.ToString() },
                                            { TeamIdParamKey, curMatch.AwayTeam.AwayTeamId.ToString() },
                                            { SourceSystemParamKey, curMatch.SourceSystem }
                                        }));
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format(
                                Globalization.Translations.UnexpectedFileType,
                                typeof(HattrickData).FullName,
                                xmlFileDownloadTask.XmlFile.GetType().FullName));
                    }
                }
                else
                {
                    throw new ArgumentException(Globalization.Translations.UnexpectedFileDownloadTaskType);
                }
            }
            catch
            {
                fileDownloadTask.Status = DownloadTaskStatus.Error;

                throw;
            }
        }
    }
}