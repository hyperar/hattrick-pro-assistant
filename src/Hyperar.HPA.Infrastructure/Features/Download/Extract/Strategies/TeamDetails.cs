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
    using Shared.Models.Hattrick.TeamDetails;
    using Shared.Models.UI.Download;

    public class TeamDetails : DownloadTaskStrategyBase, IExtractorStrategy
    {
        public Task ExtractAsync(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            CancellationToken cancellationToken)
        {
            if (task.XmlFile is HattrickData file)
            {
                foreach (var xmlTeam in file.Teams)
                {
                    string logoUrl = xmlTeam.LogoUrl ?? Url.DefaultTeamLogo;

                    if (!ImageFileExists(logoUrl))
                    {
                        task.ChildImageTaskList.Add(
                            new ImageDownloadTask(
                                logoUrl));
                    }

                    if (!ImageFileExists(xmlTeam.DressUri))
                    {
                        task.ChildImageTaskList.Add(
                            new ImageDownloadTask(
                                xmlTeam.DressUri));
                    }

                    if (!ImageFileExists(xmlTeam.DressAlternateUri))
                    {
                        task.ChildImageTaskList.Add(
                            new ImageDownloadTask(
                                xmlTeam.DressAlternateUri));
                    }

                    taskList.Add(
                        new XmlDownloadTask(
                            XmlFileType.LeagueDetails,
                            xmlTeam.TeamId,
                            new NameValueCollection
                            {
                                { QueryStringParameter.LeagueLevelUnitId, xmlTeam.LeagueLevelUnit.LeagueLevelUnitId.ToString() }
                            }));

                    taskList.Add(
                        new XmlDownloadTask(
                            XmlFileType.Players,
                            xmlTeam.TeamId,
                            new NameValueCollection
                            {
                                { QueryStringParameter.TeamId, xmlTeam.TeamId.ToString() }
                            }));

                    if (downloadSettings.DownloadFullMatchArchive)
                    {
                        DateTime startDate = xmlTeam.FoundedDate;
                        DateTime endDate = startDate.AddDays(7);

                        while (endDate < DateTime.Today)
                        {
                            taskList.Add(
                                new XmlDownloadTask(
                                    XmlFileType.MatchArchive,
                                    xmlTeam.TeamId,
                                    new NameValueCollection
                                    {
                                        { QueryStringParameter.TeamId, xmlTeam.TeamId.ToString() },
                                        { QueryStringParameter.FirstMatchDate, startDate.ToString("yyyy-MM-dd") },
                                        { QueryStringParameter.LastMatchDate, endDate.ToString("yyyy-MM-dd") },
                                        { QueryStringParameter.IsYouth, bool.FalseString },
                                        { QueryStringParameter.IncludeHto, downloadSettings.DownloadHattrickArenaMatches.ToString() }
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
                                xmlTeam.TeamId,
                                new NameValueCollection
                                {
                                    { QueryStringParameter.TeamId, xmlTeam.TeamId.ToString() },
                                    { QueryStringParameter.IsYouth, bool.FalseString },
                                    { QueryStringParameter.IncludeHto, downloadSettings.DownloadHattrickArenaMatches.ToString() }
                                }));
                    }

                    taskList.Add(
                        new XmlDownloadTask(
                            XmlFileType.Matches,
                            xmlTeam.TeamId,
                            new NameValueCollection
                            {
                                { QueryStringParameter.TeamId, xmlTeam.TeamId.ToString() },
                                { QueryStringParameter.IsYouth, bool.FalseString }
                            }));

                    if (xmlTeam.YouthTeamId != 0)
                    {
                        taskList.Add(
                            new XmlDownloadTask(
                                XmlFileType.YouthTeamDetails,
                                xmlTeam.TeamId,
                                new NameValueCollection
                                {
                                    { QueryStringParameter.YouthTeamId, xmlTeam.YouthTeamId.ToString() },
                                    { QueryStringParameter.ShowScouts, bool.TrueString }
                                }));
                    }
                }
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }

            return Task.CompletedTask;
        }
    }
}