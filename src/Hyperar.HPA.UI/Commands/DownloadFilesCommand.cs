namespace Hyperar.HPA.UI.Commands
{
    using System.Threading.Tasks;
    using Hyperar.HPA.Application.OAuth;
    using Hyperar.HPA.UI.State.Interfaces;
    using Hyperar.HPA.UI.ViewModels;

    public class DownloadFilesCommand : AsyncCommandBase
    {
        private readonly DownloadViewModel downloadViewModel;

        private readonly INavigator navigator;

        public DownloadFilesCommand(
            DownloadViewModel downloadAsyncViewModel,
            INavigator navigator)
        {
            this.downloadViewModel = downloadAsyncViewModel;
            this.navigator = navigator;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                this.navigator.CanNavigate = false;

                this.downloadViewModel.BuildInitialDownloadTask();

                DownloadTask? currentTask = this.downloadViewModel.GetNextDownloadTask();

                while (currentTask != null)
                {
                    await this.downloadViewModel.ExecuteDownloadTaskAsync(currentTask);

                    currentTask = this.downloadViewModel.GetNextDownloadTask();
                }

                await this.downloadViewModel.FinishDownloadAsync();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.navigator.CanNavigate = true;
            }
        }
    }
}