namespace Hyperar.HPA.Infrastructure.Strategies.XmlDownloadTaskExtractor
{
    using System;
    using System.Collections.Generic;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Hattrick.Matches;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Application.Models;
    using Hyperar.HPA.Common.Enums;

    public class Matches : IXmlDownloadTaskExtractorStrategy
    {
        private const string matchEventsParamKey = "matchEvents";

        private const string matchIdParamKey = "matchId";

        private const string sourceSystemParamKey = "sourceSystem";

        public List<DownloadTask>? ExtractXmlDownloadTasks(IXmlFile xmlFile)
        {
            if (xmlFile is HattrickData file)
            {
                var downloadTasks = new List<DownloadTask>();

                foreach (var curMatch in file.Team.MatchList)
                {
                    downloadTasks.Add(
                        new DownloadTask(
                            XmlFileType.MatchDetails,
                            new Dictionary<string, string>
                            {
                                { matchIdParamKey, curMatch.MatchId.ToString()},
                                { matchEventsParamKey, bool.TrueString},
                                { sourceSystemParamKey, curMatch.SourceSystem.ToString() }
                            }));
                }

                return downloadTasks;
            }
            else
            {
                throw new ArgumentException(xmlFile.GetType().FullName, nameof(xmlFile));
            }
        }
    }
}