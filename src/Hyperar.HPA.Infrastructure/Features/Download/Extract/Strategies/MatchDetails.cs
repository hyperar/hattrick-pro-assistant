namespace Hyperar.HPA.Infrastructure.Features.Download.Extract.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Infrastructure.Features.Download.Extract.Constants;
    using Shared.Enums;
    using Shared.ExtensionMethods;
    using Shared.Models.Hattrick.MatchDetails;
    using Shared.Models.UI.Download;

    public class MatchDetails : DownloadTaskStrategyBase, IExtractorStrategy
    {
        public Task ExtractAsync(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            CancellationToken cancellationToken)
        {
            if (task.XmlFile is HattrickData file)
            {
                if (file.Match.FinishedDate is not null && file.SourceSystem.ToMatchSystem() != MatchSystem.Youth)
                {
                    ArgumentNullException.ThrowIfNull(file.Match.HomeTeam.DressUri, nameof(file.Match.HomeTeam.DressUri));
                    ArgumentNullException.ThrowIfNull(file.Match.AwayTeam.DressUri, nameof(file.Match.AwayTeam.DressUri));

                    if (!ImageFileExists(file.Match.HomeTeam.DressUri))
                    {
                        task.ChildImageTaskList.Add(
                            new ImageDownloadTask(
                                file.Match.HomeTeam.DressUri));
                    }

                    if (!ImageFileExists(file.Match.AwayTeam.DressUri))
                    {
                        task.ChildImageTaskList.Add(
                            new ImageDownloadTask(
                                file.Match.AwayTeam.DressUri));
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