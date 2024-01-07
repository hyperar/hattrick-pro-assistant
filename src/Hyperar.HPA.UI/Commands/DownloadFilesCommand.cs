namespace Hyperar.HPA.UI.Commands
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Models;
    using UI.Enums;
    using UI.State.Interfaces;
    using UI.ViewModels;
    using UI.ViewModels.Interfaces;

    public class DownloadFilesCommand : AsyncCommandBase
    {
        private readonly DownloadViewModel downloadViewModel;

        private readonly INavigator navigator;

        private readonly IViewModelFactory viewModelFactory;

        public DownloadFilesCommand(
            DownloadViewModel downloadAsyncViewModel,
            INavigator navigator,
            IViewModelFactory viewModelFactory)
        {
            this.downloadViewModel = downloadAsyncViewModel;
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            this.navigator.SuspendNavigation();

            await this.downloadViewModel.BeginDownloadProcess();

            DownloadTask? currentTask = this.downloadViewModel.GetNextDownloadTask();

            while (currentTask != null)
            {
                await this.downloadViewModel.ExecuteDownloadTaskAsync(currentTask);

                currentTask = this.downloadViewModel.GetNextDownloadTask();
            }

            await this.downloadViewModel.EndDownloadProcessAsync();

            if (!this.navigator.SelectedTeamId.HasValue)
            {
                ArgumentNullException.ThrowIfNull(this.downloadViewModel.Authorizer.User, nameof(this.downloadViewModel.Authorizer.User));
                ArgumentNullException.ThrowIfNull(this.downloadViewModel.Authorizer.User.Manager, nameof(this.downloadViewModel.Authorizer.User.Manager));

                this.navigator.SelectedTeamId = this.downloadViewModel.Authorizer.User.Manager.SeniorTeams.Where(x => x.IsPrimary)
                    .Select(x => x.HattrickId)
                    .Single();
            }

            this.navigator.CurrentViewModel = await this.viewModelFactory.CreateAsyncViewModel(ViewType.Home);

            this.navigator.ResumeNavigation();
        }
    }
}