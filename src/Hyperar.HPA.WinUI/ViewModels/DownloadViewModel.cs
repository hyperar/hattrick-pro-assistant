namespace Hyperar.HPA.WinUI.ViewModels
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Services;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Shared.Models.UI.Download;
    using WinUI.State.Interface;

    public partial class DownloadViewModel : AsyncViewModelBase
    {
        private readonly IDownloadViewService downloadViewService;

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
            IDownloadViewService downloadViewService) : base(navigator)
        {
            this.downloadViewService = downloadViewService;
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
                new Application.Models.DownloadSettings
                {
                    DownloadFullMatchArchive = this.DownloadFullMatchArchive,
                    DownloadHattrickArenaMatches = this.DownloadHattrickArenaMatches,
                    ReDownloadMatchEvents = this.RedownloadMatches
                },
                progress,
                this.cancellationTokenSource.Token);

            this.cancellationTokenSource = null;

            this.Navigator.ResumeNavigation();
        }
    }
}