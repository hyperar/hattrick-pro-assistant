namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Downloader
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Application.Services;
    using Domain;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Shared.Enums;
    using Shared.Models.UI.Download;

    public class XmlFile : IFileDownloadTaskStepProcessStrategy
    {
        private readonly IHattrickService hattrickService;

        private readonly IRepository<Domain.Token> tokenRepository;

        public XmlFile(
            IRepository<Token> tokenRepository,
            IHattrickService hattrickService)
        {
            this.tokenRepository = tokenRepository;
            this.hattrickService = hattrickService;
        }

        public async Task ExecuteAsync(
            IFileDownloadTask fileDownloadTask,
            IList<IFileDownloadTask> fileDownloadTasks,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken)
        {
            try
            {
                if (fileDownloadTask is IXmlFileDownloadTask xmlFileDownloadTask)
                {
                    var token = await this.tokenRepository.Query()
                        .SingleOrDefaultAsync(cancellationToken);

                    ArgumentNullException.ThrowIfNull(token, nameof(token));

                    if (xmlFileDownloadTask.XmlFileType == XmlFileType.CheckToken)
                    {
                        xmlFileDownloadTask.Response = await this.hattrickService.CheckTokenAsync(
                            token.Value,
                            token.SecretValue,
                            cancellationToken);
                    }
                    else
                    {
                        xmlFileDownloadTask.Response = await this.hattrickService.GetProtectedResourceAsync(
                            new GetProtectedResourceRequest(
                                token.Value,
                                token.SecretValue,
                                xmlFileDownloadTask.XmlFileType,
                                xmlFileDownloadTask.Parameters),
                            cancellationToken);
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