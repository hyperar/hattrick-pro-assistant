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
        private const string matchEventsParamKey = "matchEvents";

        private const string matchIdParamKey = "matchId";

        private const string sourceSystemParamKey = "sourceSystem";

        private const string teamIdParamKey = "teamId";

        private readonly IHattrickRepository<Domain.Senior.Match> matchRepository;

        public Matches(IHattrickRepository<Domain.Senior.Match> matchRepository)
        {
            this.matchRepository = matchRepository;
        }

        public async Task ExecuteAsync(
            IFileDownloadTask fileDownloadTask,
            ICollection<IFileDownloadTask> fileDownloadTasks,
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

                        IEnumerable<Match> matches = file.Team.MatchList.Where(x => x.Status.ToMatchStatus() == MatchStatus.Finished);

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
                                            { matchIdParamKey, curMatch.MatchId.ToString() },
                                            { matchEventsParamKey, bool.TrueString},
                                            { sourceSystemParamKey, curMatch.SourceSystem.ToString() }
                                        }));
                            }

                            if (curMatch.Status.ToMatchStatus() == MatchStatus.Finished)
                            {
                                fileDownloadTasks.Add(
                                    new XmlFileDownloadTask(
                                        XmlFileType.MatchLineUp,
                                        curMatch.MatchContextId,
                                        new NameValueCollection
                                        {
                                            { matchIdParamKey, curMatch.MatchId.ToString() },
                                            { teamIdParamKey, curMatch.HomeTeam.HomeTeamId.ToString() },
                                            { sourceSystemParamKey, curMatch.SourceSystem }
                                        }));

                                fileDownloadTasks.Add(
                                    new XmlFileDownloadTask(
                                        XmlFileType.MatchLineUp,
                                        xmlFileDownloadTask.ContextId,
                                        new NameValueCollection
                                        {
                                            { matchIdParamKey, curMatch.MatchId.ToString() },
                                            { teamIdParamKey, curMatch.AwayTeam.AwayTeamId.ToString() },
                                            { sourceSystemParamKey, curMatch.SourceSystem }
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