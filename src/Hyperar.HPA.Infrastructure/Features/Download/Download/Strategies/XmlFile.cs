namespace Hyperar.HPA.Infrastructure.Features.Download.Download.Strategies
{
    using System;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Application.Models;
    using Application.Services;
    using Shared.Enums;

    public class XmlFile : IDownloaderStrategy
    {
        private readonly IHattrickService hattrickService;

        private readonly IProtectedResourceUrlFactory protectedResourceUrlFactory;

        private readonly IUserService userService;

        public XmlFile(
            IHattrickService hattrickService,
            IProtectedResourceUrlFactory protectedResourceUrlFactory,
            IUserService userService)
        {
            this.hattrickService = hattrickService;
            this.protectedResourceUrlFactory = protectedResourceUrlFactory;
            this.userService = userService;
        }

        public async Task DownloadAsync(DownloadTaskBase task, CancellationToken cancellationToken)
        {
            if (task is XmlDownloadTask xmlDownloadTask)
            {
                xmlDownloadTask.Status = DownloadTaskStatus.Downloading;

                var user = await this.userService.GetUserAsync();

                ArgumentNullException.ThrowIfNull(user, nameof(user));
                ArgumentNullException.ThrowIfNull(user.Token, nameof(user.Token));
                ArgumentNullException.ThrowIfNull(user.Token.Value, nameof(user.Token.Value));
                ArgumentNullException.ThrowIfNull(user.Token.Secret, nameof(user.Token.Secret));

                if (xmlDownloadTask.FileType == XmlFileType.CheckToken)
                {
                    xmlDownloadTask.Response = await this.hattrickService.CheckTokenAsync(
                        user.Token.Value,
                        user.Token.Secret,
                        cancellationToken);
                }
                else
                {
                    xmlDownloadTask.Response = await this.hattrickService.GetProtectedResourceAsync(
                        new GetProtectedResourceRequest(
                            user.Token.Value,
                            user.Token.Secret,
                            xmlDownloadTask.FileType,
                            xmlDownloadTask.Parameters),
                        cancellationToken);
                }

                xmlDownloadTask.Status = DownloadTaskStatus.Downloaded;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(task));
            }
        }
    }
}