namespace Hyperar.HPA.Infrastructure.Features.Download.Extract.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Extract.Constants;
    using Shared.Enums;
    using Shared.Models.Hattrick.YouthTeamDetails;
    using Shared.Models.UI.Download;

    public class YouthTeamDetails : DownloadTaskStrategyBase, IExtractorStrategy
    {
        public Task ExtractAsync(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            CancellationToken cancellationToken)
        {
            if (task.XmlFile is HattrickData file)
            {
                if (file.YouthTeam.YouthLeague != null)
                {
                    taskList.Add(
                        new XmlDownloadTask(
                            XmlFileType.YouthLeagueDetails,
                            file.YouthTeam.YouthTeamId,
                            new NameValueCollection
                            {
                                { QueryStringParameter.YouthLeagueId, file.YouthTeam.YouthLeague.YouthLeagueId.ToString() }
                            }));
                }

                taskList.Add(
                    new XmlDownloadTask(
                        XmlFileType.YouthPlayerList,
                        file.YouthTeam.YouthTeamId,
                        new NameValueCollection
                        {
                            { QueryStringParameter.YouthTeamId, file.YouthTeam.YouthTeamId.ToString() },
                            { QueryStringParameter.ActionType, QueryStringParameterValue.ActionTypeList },
                            { QueryStringParameter.ShowScoutCall, bool.FalseString },
                            { QueryStringParameter.ShowLastMatch, bool.FalseString }
                        }));

                if (downloadSettings.DownloadFullMatchArchive)
                {
                    DateTime startDate = file.YouthTeam.CreatedDate;
                    DateTime endDate = startDate.AddDays(7);

                    while (endDate < DateTime.Today)
                    {
                        taskList.Add(
                            new XmlDownloadTask(
                                XmlFileType.MatchArchive,
                                file.YouthTeam.YouthTeamId,
                                new NameValueCollection
                                {
                                    { QueryStringParameter.TeamId, file.YouthTeam.YouthTeamId.ToString() },
                                    { QueryStringParameter.FirstMatchDate, startDate.ToString("yyyy-MM-dd") },
                                    { QueryStringParameter.LastMatchDate, endDate.ToString("yyyy-MM-dd") },
                                    { QueryStringParameter.IsYouth, bool.TrueString },
                                    { QueryStringParameter.IncludeHto, bool.FalseString }
                                }));

                        startDate = startDate.AddDays(7);
                        endDate = endDate.AddDays(7);
                    }
                }
                else
                {
                    taskList.Add(
                        new XmlDownloadTask(
                            XmlFileType.MatchArchive,
                            file.YouthTeam.YouthTeamId,
                            new NameValueCollection
                            {
                                { QueryStringParameter.TeamId, file.YouthTeam.YouthTeamId.ToString() },
                                { QueryStringParameter.IsYouth, bool.TrueString },
                                { QueryStringParameter.IncludeHto, bool.FalseString }
                            }));
                }

                taskList.Add(
                    new XmlDownloadTask(
                        XmlFileType.Matches,
                        file.YouthTeam.YouthTeamId,
                        new NameValueCollection
                        {
                            { QueryStringParameter.TeamId, file.YouthTeam.YouthTeamId.ToString() },
                            { QueryStringParameter.IsYouth, bool.TrueString }
                        }));
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }

            return Task.CompletedTask;
        }
    }
}