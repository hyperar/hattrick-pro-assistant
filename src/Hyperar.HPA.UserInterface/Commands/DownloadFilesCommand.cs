namespace Hyperar.HPA.UserInterface.Commands
{
    using System.Threading.Tasks;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.UserInterface.ViewModels;

    public class DownloadFilesCommand : AsyncCommandBase
    {
        private readonly DownloadViewModel downloadViewModel;

        public DownloadFilesCommand(DownloadViewModel downloadViewModel)
        {
            this.downloadViewModel = downloadViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            this.downloadViewModel.BuildInitialDownloadTask();

            DownloadTask? currentTask = this.downloadViewModel.GetNextDownloadTask();

            while (currentTask != null)
            {
                await Task.Run(() => this.downloadViewModel.ExecuteDownloadTask(currentTask));

                currentTask = this.downloadViewModel.GetNextDownloadTask();
            }
        }
    }
}
