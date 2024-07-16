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
    using Shared.Models.Hattrick.ManagerCompendium;
    using Shared.Models.UI.Download;

    public class ManagerCompendium : DownloadTaskStrategyBase, IExtractorStrategy
    {
        public Task ExtractAsync(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            CancellationToken cancellationToken)
        {
            if (task.XmlFile is HattrickData file)
            {
                if (file.Manager.Avatar != null)
                {
                    if (!ImageFileExists(file.Manager.Avatar.BackgroundImage))
                    {
                        task.ChildImageTaskList.Add(
                            new ImageDownloadTask(file.Manager.Avatar.BackgroundImage));
                    }

                    foreach (var xmlLayer in file.Manager.Avatar.Layers)
                    {
                        if (!ImageFileExists(file.Manager.Avatar.BackgroundImage))
                        {
                            task.ChildImageTaskList.Add(
                                new ImageDownloadTask(xmlLayer.Image));
                        }
                    }
                }

                foreach (var xmlTeam in file.Manager.Teams)
                {
                    taskList.Add(
                        new XmlDownloadTask(
                            XmlFileType.WorldDetails,
                            null,
                            new NameValueCollection
                            {
                                { QueryStringParameter.LeagueId, xmlTeam.League.LeagueId.ToString() },
                                { QueryStringParameter.IncludeRegions, bool.TrueString }
                            }));
                }

                taskList.Add(
                    new XmlDownloadTask(
                        XmlFileType.TeamDetails,
                        file.Manager.UserId,
                        new NameValueCollection
                        {
                            { QueryStringParameter.UserId, file.Manager.UserId.ToString() }
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