namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Extractor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Shared.Enums;
    using Shared.Models.Hattrick.Players;
    using Shared.Models.UI.Download;

    public class Players : IFileDownloadTaskStepProcessStrategy
    {
        private const string BidAmount = "bidAmount";

        private const string IncludeMatchInfo = "includeMatchInfo";

        private const string MaxBidAmount = "maxBidAmount";

        private const string PlayerId = "playerId";

        private const string TeamIdParamKey = "teamId";

        public async Task ExecuteAsync(
            IFileDownloadTask fileDownloadTask,
            IList<IFileDownloadTask> fileDownloadTasks,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(0, cancellationToken);

                if (fileDownloadTask is XmlFileDownloadTask xmlFileDownloadTask)
                {
                    ArgumentNullException.ThrowIfNull(xmlFileDownloadTask.XmlFile, nameof(xmlFileDownloadTask.XmlFile));

                    if (xmlFileDownloadTask.XmlFile is HattrickData file)
                    {
                        int index = fileDownloadTasks.IndexOf(fileDownloadTask);

                        foreach (var xmlPlayer in file.Team.PlayerList.Where(x => x.TrainerData == null))
                        {
                            index++;

                            fileDownloadTasks.Insert(
                                index,
                                new XmlFileDownloadTask(
                                    XmlFileType.PlayerDetails,
                                    xmlFileDownloadTask.ContextId,
                                    new NameValueCollection
                                    {
                                        { PlayerId, xmlPlayer.PlayerId.ToString()},
                                        { IncludeMatchInfo, bool.TrueString }
                                    }));
                        }
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format(
                                Globalization.Translations.UnexpectedFileType,
                                typeof(HattrickData).FullName,
                                xmlFileDownloadTask.XmlFile.GetType().FullName));
                    }
                }
                else
                {
                    throw new ArgumentException(Globalization.Translations.UnexpectedFileDownloadTaskType);
                }
            }
            catch
            {
                fileDownloadTask.Status = DownloadTaskStatus.Error;

                throw;
            }
        }
    }
}