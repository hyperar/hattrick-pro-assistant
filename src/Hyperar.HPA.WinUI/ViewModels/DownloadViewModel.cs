namespace Hyperar.HPA.WinUI.ViewModels
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Services;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Shared.Models.UI.Download;
    using WinUI.Enums;
    using WinUI.State.Interface;

    public partial class DownloadViewModel : AsyncViewModelBase
    {
        private readonly IDownloadViewService downloadViewService;

        private readonly IUserService userService;

        private CancellationTokenSource? cancellationTokenSource;

        [ObservableProperty]
        private bool downloadFullMatchArchive;

        [ObservableProperty]
        private bool downloadHattrickArenaMatches;

        [ObservableProperty]
        private ProcessReport? downloadProgressReport;

        [ObservableProperty]
        private bool redownloadMatches;

        public DownloadViewModel(
            INavigator navigator,
            IDownloadViewService downloadViewService,
            IUserService userService) : base(navigator)
        {
            this.downloadViewService = downloadViewService;
            this.userService = userService;
        }

        public override async Task InitializeAsync()
        {
            var user = await this.userService.GetUserAsync();

            if (user.LastDownloadDate != null)
            {
                this.Navigator.ResumeNavigation();
            }

            await base.InitializeAsync();
        }

        [RelayCommand]
        private async Task CancelDownloadAsync()
        {
            ArgumentNullException.ThrowIfNull(this.cancellationTokenSource, nameof(this.cancellationTokenSource));

            await this.cancellationTokenSource.CancelAsync();
        }

        [RelayCommand]
        private async Task ExecuteDownloadAsync()
        {
            this.Navigator.SuspendNavigation();

            this.cancellationTokenSource = new CancellationTokenSource();

            Progress<ProcessReport> progress = new Progress<ProcessReport>(report =>
            {
                this.DownloadProgressReport = report;
            });

            await this.downloadViewService.UpdateFromHattrickAsync(
                new DownloadSettings
                {
                    DownloadFullMatchArchive = this.DownloadFullMatchArchive,
                    DownloadHattrickArenaMatches = this.DownloadHattrickArenaMatches,
                    ReDownloadMatchEvents = this.RedownloadMatches
                },
                progress,
                this.cancellationTokenSource.Token);

            this.cancellationTokenSource = null;

            var user = await this.userService.GetUserAsync();

            if (user.LastDownloadDate == null)
            {
                return;
            }

            if (user.SelectedTeamHattrickId == null)
            {
                this.Navigator.SetPageType(ViewType.TeamSelection);
            }
            else
            {
                this.Navigator.SetPageType(ViewType.Home);

                this.Navigator.ResumeNavigation();
            }
        }
    }
}