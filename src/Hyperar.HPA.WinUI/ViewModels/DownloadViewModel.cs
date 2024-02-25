namespace Hyperar.HPA.WinUI.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Application.Hattrick.Interfaces;
    using Application.Models;
    using Application.Services;
    using Common.Enums;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using WinUI.State.Interface;

    public partial class DownloadViewModel : AsyncViewModelBase
    {
        private readonly IHattrickService hattrickService;

        private readonly ITeamSelector teamSelector;

        private readonly IUserService userService;

        private readonly IXmlFileService xmlFileService;

        [ObservableProperty]
        private bool canDownload;

        [ObservableProperty]
        private ObservableCollection<DownloadTask> downloadTasks;

        [ObservableProperty]
        private bool isDownloading;

        [ObservableProperty]
        private DownloadTask? selectedItem;

        private Domain.User? user;

        public DownloadViewModel(
            INavigator navigator,
            IHattrickService hattrickService,
            ITeamSelector teamSelector,
            IUserService userService,
            IXmlFileService xmlFileService) : base(navigator)
        {
            this.DownloadTasks = new ObservableCollection<DownloadTask>();

            this.hattrickService = hattrickService;
            this.teamSelector = teamSelector;
            this.userService = userService;
            this.xmlFileService = xmlFileService;
        }

        public int CompletedTaskCount
        {
            get
            {
                return this.DownloadTasks.Where(x => x.Status == DownloadTaskStatus.Done).Count();
            }
        }

        public int ProgressPercentage
        {
            get
            {
                return this.TaskCount == 0 ? 0 : (int)((decimal)this.CompletedTaskCount / this.TaskCount * 100m);
            }
        }

        public int TaskCount
        {
            get
            {
                return this.DownloadTasks.Count;
            }
        }

        public override async Task InitializeAsync()
        {
            this.user = await this.userService.GetUserAsync();

            if (this.user.Token != null)
            {
                try
                {
                    await this.hattrickService.CheckTokenAsync(
                        new OAuthToken(
                            this.user.Token.Value,
                            this.user.Token.SecretValue));

                    this.CanDownload = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        Globalization.Strings.Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void AddChildTasks(DownloadTask[] childDownloadTasks)
        {
            foreach (DownloadTask newTask in childDownloadTasks)
            {
                this.DownloadTasks.Add(newTask);
            }

            this.OnPropertyChanged(nameof(this.CompletedTaskCount));
            this.OnPropertyChanged(nameof(this.TaskCount));
            this.OnPropertyChanged(nameof(this.ProgressPercentage));
        }

        private async Task BeginDownloadProcess()
        {
            await this.xmlFileService.BeginPersistSession();

            this.DownloadTasks = new ObservableCollection<DownloadTask>
            {
                new DownloadTask(XmlFileType.WorldDetails),
                new DownloadTask(XmlFileType.ManagerCompendium)
            };
        }

        private async Task CancelDownloadProcess()
        {
            await this.xmlFileService.CancelPersistSession();

            int index = this.DownloadTasks.IndexOf(
                this.DownloadTasks.First(x => x.Status != DownloadTaskStatus.Done));

            this.ChangeTaskStatus(
                this.DownloadTasks[index],
                DownloadTaskStatus.Error);

            for (int i = index + 1; i < this.DownloadTasks.Count; i++)
            {
                this.ChangeTaskStatus(
                    this.DownloadTasks[i],
                    DownloadTaskStatus.Canceled);
            }
        }

        private void ChangeTaskStatus(DownloadTask task, DownloadTaskStatus newStatus)
        {
            int index = this.DownloadTasks.IndexOf(task);

            this.DownloadTasks[index].Status = newStatus;

            this.OnPropertyChanged(nameof(this.CompletedTaskCount));
            this.OnPropertyChanged(nameof(this.TaskCount));
            this.OnPropertyChanged(nameof(this.ProgressPercentage));
        }

        [RelayCommand]
        private async Task DownloadFilesAsync()
        {
            this.Navigator.SuspendNavigation();

            this.CanDownload = false;

            this.IsDownloading = true;

            try
            {
                await this.BeginDownloadProcess();

                for (int i = 0; i < this.DownloadTasks.Count; ++i)
                {
                    this.SelectedItem = this.DownloadTasks[i];

                    await this.ExecuteDownloadTaskAsync(this.SelectedItem);
                }

                this.SelectedItem = null;
            }
            catch
            {
                await this.CancelDownloadProcess();
            }
            finally
            {
                await this.EndDownloadProcessAsync();
            }

            if (this.teamSelector.SelectedTeamId == 0)
            {
                ArgumentNullException.ThrowIfNull(this.user, nameof(this.user));
                ArgumentNullException.ThrowIfNull(this.user.Manager, nameof(this.user.Manager));

                this.teamSelector.SetSelectedTeam(
                    this.user.Manager.Teams.Where(x => x.IsPrimary)
                                           .Select(x => x.HattrickId)
                                           .Single());
            }

            this.CanDownload = true;

            this.IsDownloading = false;

            this.Navigator.ResumeNavigation();
        }

        private async Task EndDownloadProcessAsync()
        {
            await this.userService.UpdateUserLastDownloadDate();

            await this.xmlFileService.EndPersistSession();
        }

        private async Task ExecuteDownloadTaskAsync(DownloadTask task)
        {
            string xmlFileContent = await this.StartDownloadTaskAsync(task);

            IXmlFile xmlFile = await this.ParseXmlFileContent(task, xmlFileContent);

            // TODO: Add XmlErrorFile check.

            this.AddChildTasks(
                this.ExtractXmlDownloadTasks(
                    task,
                    xmlFile));

            await this.StoreDownloadTaskAsync(
                task,
                xmlFile);

            this.ChangeTaskStatus(task, DownloadTaskStatus.Done);
        }

        private DownloadTask[] ExtractXmlDownloadTasks(DownloadTask task, IXmlFile xmlFile)
        {
            ArgumentNullException.ThrowIfNull(xmlFile, nameof(xmlFile));

            this.ChangeTaskStatus(task, DownloadTaskStatus.Extracting);

            return this.xmlFileService.ExtractXmlDownloadTasks(xmlFile);
        }

        private async Task<IXmlFile> ParseXmlFileContent(DownloadTask task, string xmlFileContent)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(xmlFileContent, nameof(xmlFileContent));

            this.ChangeTaskStatus(task, DownloadTaskStatus.Parsing);

            return await this.xmlFileService.ParseFileAsync(xmlFileContent);
        }

        private async Task<string> StartDownloadTaskAsync(DownloadTask task)
        {
            this.ChangeTaskStatus(task, DownloadTaskStatus.Downloading);

            ArgumentNullException.ThrowIfNull(this.user, nameof(this.user));
            ArgumentNullException.ThrowIfNull(this.user.Token, nameof(this.user.Token));

            GetProtectedResourceRequest request = new GetProtectedResourceRequest(
                this.user.Token.Value,
                this.user.Token.SecretValue,
                task.FileType,
                task.Parameters);

            return await this.hattrickService.GetProtectedResourceAsync(request);
        }

        private async Task StoreDownloadTaskAsync(DownloadTask task, IXmlFile xmlFile)
        {
            ArgumentNullException.ThrowIfNull(xmlFile, nameof(xmlFile));

            this.ChangeTaskStatus(task, DownloadTaskStatus.Saving);

            switch (task.FileType)
            {
                case XmlFileType.MatchDetails:
                    ArgumentNullException.ThrowIfNull(task.ContextId, nameof(task.ContextId));

                    await this.xmlFileService.PersistFileAsync(xmlFile, task.ContextId.Value);
                    break;

                default:
                    await this.xmlFileService.PersistFileAsync(xmlFile);
                    break;
            }
        }
    }
}