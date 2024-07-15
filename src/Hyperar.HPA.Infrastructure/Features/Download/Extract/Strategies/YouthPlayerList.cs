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
    using Shared.Models.Hattrick.YouthPlayerList;
    using Shared.Models.UI.Download;

    public class YouthPlayerList : DownloadTaskStrategyBase, IExtractorStrategy
    {
        public Task ExtractAsync(
            XmlDownloadTask task,
            IList<XmlDownloadTask> taskList,
            DownloadSettings downloadSettings,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(task.ContextId, nameof(task.ContextId));

            if (task.XmlFile is HattrickData file)
            {
                foreach (var xmlPlayer in file.YouthPlayerList)
                {
                    taskList.Add(
                        new XmlDownloadTask(
                            XmlFileType.YouthPlayerDetails,
                            task.ContextId,
                            new NameValueCollection
                            {
                                    { QueryStringParameter.ActionType, QueryStringParameterValue.ActionTypeList },
                                    { QueryStringParameter.YouthPlayerId, xmlPlayer.YouthPlayerId.ToString() },
                                    { QueryStringParameter.ShowScoutCall, bool.FalseString },
                                    { QueryStringParameter.ShowLastMatch, bool.FalseString }
                            }));
                }

                taskList.Add(
                    new XmlDownloadTask(
                        XmlFileType.YouthAvatars,
                        task.ContextId,
                        new NameValueCollection
                        {
                            { QueryStringParameter.YouthTeamId, task.ContextId.ToString() }
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