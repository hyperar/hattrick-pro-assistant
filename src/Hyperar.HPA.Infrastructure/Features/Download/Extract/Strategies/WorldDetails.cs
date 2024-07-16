namespace Hyperar.HPA.Infrastructure.Features.Download.Extract.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Shared.Models.Hattrick.WorldDetails;
    using Shared.Models.UI.Download;

    public class WorldDetails : DownloadTaskStrategyBase, IExtractorStrategy
    {
        private const string LeagueFlagImageUrlMask = "/Img/flags/{0}.png";

        public Task ExtractAsync(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            CancellationToken cancellationToken)
        {
            if (task.XmlFile is HattrickData file)
            {
                foreach (var xmlLeague in file.LeagueList)
                {
                    string url = string.Format(
                        LeagueFlagImageUrlMask,
                        xmlLeague.LeagueId);

                    if (!ImageFileExists(url))
                    {
                        task.ChildImageTaskList.Add(
                            new ImageDownloadTask(url));
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