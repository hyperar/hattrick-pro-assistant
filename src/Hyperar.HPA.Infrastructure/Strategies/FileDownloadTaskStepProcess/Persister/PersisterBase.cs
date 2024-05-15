namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Models;
    using Hyperar.HPA.Shared.Constants;
    using Microsoft.Identity.Client;
    using Shared.Enums;
    using Shared.Models.UI.Download;
    using Models = Shared.Models.Hattrick;

    public abstract class PersisterBase : FileDownloadTaskStepProcessStrategyBase, IFileDownloadTaskStepProcessStrategy
    {
        public async Task ExecuteAsync(
            IFileDownloadTask fileDownloadTask,
            ICollection<IFileDownloadTask> fileDownloadTasks,
            DownloadSettings downloadSettings,
            IProgress<ProcessReport> progress,
            CancellationToken cancellationToken)
        {
            try
            {
                if (fileDownloadTask is IXmlFileDownloadTask xmlFileDownloadTask)
                {
                    await this.PersistFileAsync(xmlFileDownloadTask, cancellationToken);
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

        public abstract Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken);

        protected static async Task<byte[]> BuildAvatarFromLayersAsync(Models.Avatar avatar)
        {
            Bitmap backgroundImage = await CreateAvatarImageAsync(avatar.BackgroundImage);

            Bitmap avatarImage = new Bitmap(
                backgroundImage.Width,
                backgroundImage.Height,
                PixelFormat.Format32bppArgb);

            Graphics graphics = Graphics.FromImage(avatarImage);

            graphics.DrawImage(
                backgroundImage,
                0,
                0,
                backgroundImage.Width,
                backgroundImage.Height);

            for (int i = 0; i < avatar.Layers.Count; i++)
            {
                var layer = avatar.Layers[i];

                Bitmap layerImage = GetImageFromBytes(
                    await GetImageBytesFromCacheAsync(
                        layer.Image));

                graphics.DrawImage(
                    layerImage,
                    layer.X,
                    layer.Y,
                    layerImage.Width,
                    layerImage.Height);
            }

            return GetBytesFromImage(avatarImage);
        }

        protected static string CalculateRating(decimal averageRating, decimal endOfMatchRating)
        {
            List<string> startList = new List<string>();

            decimal delta = Math.Abs(averageRating - endOfMatchRating);
            decimal baseValue = delta > 0 ? endOfMatchRating : averageRating;

            int primaryBigStars = (int)baseValue / 5;
            baseValue -= 5 * primaryBigStars;

            int primaryWholeStars = (int)baseValue;
            baseValue -= primaryWholeStars;

            int primaryHalfStar = baseValue > 0 && delta == 0
                                ? 1
                                : 0;
            baseValue -= primaryHalfStar * 0.5m;

            int transitionStar = baseValue > 0 && delta > 0
                               ? 1
                               : 0;

            baseValue -= transitionStar * 0.5m;
            delta -= transitionStar * 0.5m;

            int secondaryWholeStars = (int)delta / 1;
            delta -= secondaryWholeStars;

            int secondaryHalfStar = delta > 0 ? 1 : 0;
            delta -= secondaryHalfStar * 0.5m;

            if (baseValue != 0 || delta != 0)
            {

            }

            while (primaryBigStars > 0)
            {
                startList.Add(MatchRatingStars.YellowBigStar);
                primaryBigStars--;
            }

            while (primaryWholeStars > 0)
            {
                startList.Add(MatchRatingStars.YellowWholeStar);
                primaryWholeStars--;
            }

            while (primaryHalfStar > 0)
            {
                startList.Add(MatchRatingStars.YellowHalfStar);
                primaryHalfStar--;
            }

            while (transitionStar > 0)
            {
                startList.Add(averageRating > endOfMatchRating ? MatchRatingStars.YellowToBrownStar : MatchRatingStars.YellowToRedStar);
                transitionStar--;
            }

            while (secondaryWholeStars > 0)
            {
                startList.Add(averageRating > endOfMatchRating ? MatchRatingStars.BrownWholeStar : MatchRatingStars.RedWholeStar);
                secondaryWholeStars--;
            }

            while (secondaryHalfStar > 0)
            {
                startList.Add(averageRating > endOfMatchRating ? MatchRatingStars.BrownHalfStar : MatchRatingStars.RedHalfStar);
                secondaryHalfStar--;
            }

            if (primaryBigStars > 0 || primaryWholeStars > 0 || primaryWholeStars > 0 || transitionStar > 0 || secondaryWholeStars > 0 || secondaryWholeStars > 0)
            {

            }

            return string.Join(",", startList);
        }

        private static async Task<Bitmap> CreateAvatarImageAsync(string url)
        {
            return GetImageFromBytes(
                await GetImageBytesFromCacheAsync(
                    url));
        }

        private static byte[] GetBytesFromImage(Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);

                return memoryStream.ToArray();
            }
        }

        private static Bitmap GetImageFromBytes(byte[] imageBytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(imageBytes))
            {
                Bitmap bitmap = new Bitmap(Image.FromStream(memoryStream));

                bitmap.SetResolution(120, 120);

                return bitmap;
            }
        }
    }
}