namespace Hyperar.HPA.Infrastructure.Strategies.XmlDownloadTaskExtractor
{
    using System;
    using System.Collections.Generic;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.Matches;
    using Application.Interfaces;
    using Application.Models;
    using Common.Enums;

    public class Matches : IXmlDownloadTaskExtractorStrategy
    {
        private const string matchEventsParamKey = "matchEvents";

        private const string matchIdParamKey = "matchId";

        private const string sourceSystemParamKey = "sourceSystem";

        private const string teamIdParamKey = "teamId";

        public DownloadTask[] ExtractXmlDownloadTasks(IXmlFile xmlFile)
        {
            if (xmlFile is HattrickData file)
            {
                List<DownloadTask> downloadTasks = new List<DownloadTask>();

                foreach (Match? curMatch in file.Team.MatchList.Where(x => x.Status == MatchStatus.Finished))
                {
                    downloadTasks.Add(
                        new DownloadTask(
                            XmlFileType.MatchDetails,
                            file.Team.TeamId,
                            new Dictionary<string, string>
                            {
                                { matchIdParamKey, curMatch.MatchId.ToString() },
                                { matchEventsParamKey, bool.TrueString},
                                { sourceSystemParamKey, curMatch.SourceSystem.ToString() }
                            }));

                    downloadTasks.Add(
                        new DownloadTask(
                            XmlFileType.MatchLineUp,
                            new Dictionary<string, string>
                            {
                                    { matchIdParamKey, curMatch.MatchId.ToString() },
                                    { teamIdParamKey, curMatch.HomeTeam.HomeTeamId.ToString() },
                                    { sourceSystemParamKey, curMatch.SourceSystem }
                            }));

                    downloadTasks.Add(
                        new DownloadTask(
                            XmlFileType.MatchLineUp,
                            new Dictionary<string, string>
                            {
                                    { matchIdParamKey, curMatch.MatchId.ToString() },
                                    { teamIdParamKey, curMatch.AwayTeam.AwayTeamId.ToString() },
                                    { sourceSystemParamKey, curMatch.SourceSystem }
                            }));
                }

                return downloadTasks.ToArray();
            }
            else
            {
                throw new ArgumentException(xmlFile.GetType().FullName, nameof(xmlFile));
            }
        }
    }
}