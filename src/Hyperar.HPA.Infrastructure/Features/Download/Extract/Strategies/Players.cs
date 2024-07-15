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
    using Shared.Models.Hattrick.Players;
    using Shared.Models.UI.Download;

    public class Players : IExtractorStrategy
    {
        public Task ExtractAsync(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            CancellationToken cancellationToken)
        {
            if (task.XmlFile is HattrickData file)
            {
                int index = taskList.IndexOf(task);

                foreach (var xmlPlayer in file.Team.PlayerList.Where(x => x.TrainerData is null))
                {
                    index++;

                    // Insert PlayerDetails files before Avatars so the players exist on the database before trying to build the avatars.
                    taskList.Insert(
                        index,
                        new XmlDownloadTask(
                            XmlFileType.PlayerDetails,
                            file.Team.TeamId,
                            new NameValueCollection
                            {
                                { QueryStringParameter.PlayerId, xmlPlayer.PlayerId.ToString() }
                            }));
                }

                taskList.Add(
                    new XmlDownloadTask(
                        XmlFileType.Avatars,
                        file.Team.TeamId,
                        new NameValueCollection
                        {
                                { QueryStringParameter.TeamId, file.Team.TeamId.ToString() }
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