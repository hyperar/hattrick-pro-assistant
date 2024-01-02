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
            DownloadViewModel downloadViewModel,
            INavigator navigator)
        {
            this.downloadViewModel = downloadViewModel;
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
                    await Task.Run(() => this.downloadViewModel.ExecuteDownloadTask(currentTask));

                    currentTask = this.downloadViewModel.GetNextDownloadTask();
                }
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