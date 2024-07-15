namespace Hyperar.HPA.Infrastructure.Features.Download.Extract.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Shared.Enums;
    using Shared.Models.Hattrick.CheckToken;

    using Shared.Models.UI.Download;

    public class CheckToken : IExtractorStrategy
    {
        public Task ExtractAsync(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            CancellationToken cancellationToken)
        {
            if (task.XmlFile is HattrickData file)
            {
                taskList.Add(
                    new XmlDownloadTask(
                        XmlFileType.WorldDetails));

                taskList.Add(
                    new XmlDownloadTask(
                        XmlFileType.ManagerCompendium,
                        file.UserId));
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }

            return Task.CompletedTask;
        }
    }
}