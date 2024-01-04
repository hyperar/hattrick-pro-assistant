namespace Hyperar.HPA.UI.Commands
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Hyperar.HPA.Application.Models;
    using Hyperar.HPA.UI.Enums;
    using Hyperar.HPA.UI.State.Interfaces;
    using Hyperar.HPA.UI.ViewModels;
    using Hyperar.HPA.UI.ViewModels.Interfaces;

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

            this.downloadViewModel.BuildInitialDownloadTask();

            DownloadTask? currentTask = this.downloadViewModel.GetNextDownloadTask();

            while (currentTask != null)
            {
                await this.downloadViewModel.ExecuteDownloadTaskAsync(currentTask);

                currentTask = this.downloadViewModel.GetNextDownloadTask();
            }

            await this.downloadViewModel.FinishDownloadAsync();

            if (!this.navigator.SelectedTeamId.HasValue)
            {
                ArgumentNullException.ThrowIfNull(this.downloadViewModel.Authorizer.User, nameof(this.downloadViewModel.Authorizer.User));
                ArgumentNullException.ThrowIfNull(this.downloadViewModel.Authorizer.User.Manager, nameof(this.downloadViewModel.Authorizer.User.Manager));
                ArgumentNullException.ThrowIfNull(this.downloadViewModel.Authorizer.User.Manager.SeniorTeams, nameof(this.downloadViewModel.Authorizer.User.Manager.SeniorTeams));

#pragma warning disable CS8604 // .NET Core refuses to acknowledge that I'm checking for nulls just before accessing the object.
                this.navigator.SelectedTeamId = this.downloadViewModel.Authorizer.User.Manager.SeniorTeams.Where(x => x.IsPrimary)
                    .Select(x => x.HattrickId)
                    .Single();
#pragma warning restore CS8604 // .NET Core refuses to acknowledge that I'm checking for nulls just before accessing the object.
            }

            this.navigator.CurrentViewModel = await this.viewModelFactory.CreateAsyncViewModel(ViewType.Home);

            this.navigator.ResumeNavigation();
        }
    }
}