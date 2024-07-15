namespace Hyperar.HPA.Infrastructure.Features.Download.Extract.Strategies
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Shared.Models.Hattrick.YouthAvatars;
    using Shared.Models.UI.Download;

    public class YouthAvatars : DownloadTaskStrategyBase, IExtractorStrategy
    {
        public Task ExtractAsync(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            CancellationToken cancellationToken)
        {
            if (task.XmlFile is HattrickData file)
            {
                foreach (var xmlPlayer in file.YouthTeam.YouthPlayers)
                {
                    if (!ImageFileExists(xmlPlayer.Avatar.BackgroundImage))
                    {
                        task.ChildImageTaskList.Add(
                            new ImageDownloadTask(
                                xmlPlayer.Avatar.BackgroundImage));
                    }

                    foreach (var xmlLayer in xmlPlayer.Avatar.Layers)
                    {
                        if (!ImageFileExists(xmlLayer.Image))
                        {
                            task.ChildImageTaskList.Add(
                                new ImageDownloadTask(
                                    xmlLayer.Image));
                        }
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