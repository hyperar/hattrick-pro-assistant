namespace Hyperar.HPA.UI.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Hyperar.HPA.Application.Models;
    using Hyperar.HPA.Application.Services;
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.UI.Commands;
    using Hyperar.HPA.UI.State.Interfaces;

    public class DownloadViewModel : AuthorizedViewModelBase
    {
        private readonly IHattrickService hattrickService;

        private readonly IUserService userService;

        private readonly IXmlFileService xmlFileService;

        private List<DownloadTask> downloadTasks;

        public DownloadViewModel(
            IAuthorizer authorizer,
            IHattrickService hattrickService,
            IUserService userService,
            IXmlFileService xmlFileService,
            INavigator navigator) : base(authorizer)
        {
            this.downloadTasks = new List<DownloadTask>();

            this.DownloadFilesCommand = new DownloadFilesCommand(this, navigator);
            this.hattrickService = hattrickService;
            this.userService = userService;
            this.xmlFileService = xmlFileService;
        }

        public int CompletedDownloadTaskStepsCount
        {
            get
            {
                return this.downloadTasks.Sum(x => (int)x.Status);
            }
        }

        public DownloadTask? CurrentDownloadTask
        {
            get
            {
                return this.DownloadTasks.Where(x => x.Status != DownloadTaskStatus.Done).FirstOrDefault();
            }
        }

        public ICommand DownloadFilesCommand { get; }

        public ObservableCollection<DownloadTask> DownloadTasks
        {
            get
            {
                return new ObservableCollection<DownloadTask>(this.downloadTasks);
            }
            set
            {
                this.downloadTasks = value.ToList();
                this.OnPropertyChanged(nameof(this.DownloadTasks));
            }
        }

        public int DownloadTaskStepsCount
        {
            get
            {
                // To avoid full progress bar on 0% completion.
                return this.downloadTasks.Count == 0 ? 1 : this.downloadTasks.Count * 4;
            }
        }

        public int ProgressPercentage
        {
            get
            {
                return this.DownloadTaskStepsCount == 0
                    ? 0
                    : (int)(this.CompletedDownloadTaskStepsCount / (decimal)this.DownloadTaskStepsCount * 100m);
            }
        }

        public void BuildInitialDownloadTask()
        {
            this.downloadTasks.Clear();

            this.downloadTasks.Add(new DownloadTask(XmlFileType.WorldDetails));
            this.downloadTasks.Add(new DownloadTask(XmlFileType.ManagerCompendium));

            this.OnPropertyChanged(nameof(this.DownloadTasks));
            this.OnPropertyChanged(nameof(this.CurrentDownloadTask));
            this.OnPropertyChanged(nameof(this.DownloadTaskStepsCount));
            this.OnPropertyChanged(nameof(this.CompletedDownloadTaskStepsCount));
        }

        public async Task ExecuteDownloadTaskAsync(DownloadTask task)
        {
            await this.StartDownloadTaskAsync(task);

            List<DownloadTask>? childTasks = await this.ProcessDownloadTaskResultAsync(task);

            if (childTasks != null)
            {
                this.AddChildTasks(childTasks);
            }

            await this.StoreDownloadTaskAsync(task);

            this.CompleteTask(task);
        }

        public async Task FinishDownloadAsync()
        {
            await this.userService.UpdateUserLastDownloadDate();
        }

        public DownloadTask? GetNextDownloadTask()
        {
            return this.DownloadTasks.FirstOrDefault(x => x.Status == DownloadTaskStatus.Pending);
        }

        private void AddChildTasks(List<DownloadTask>? childDownloadTasks)
        {
            if (childDownloadTasks == null)
            {
                return;
            }

            this.downloadTasks.AddRange(childDownloadTasks);

            this.OnPropertyChanged(nameof(this.DownloadTasks));
            this.OnPropertyChanged(nameof(this.DownloadTaskStepsCount));
            this.OnPropertyChanged(nameof(this.CompletedDownloadTaskStepsCount));
            this.OnPropertyChanged(nameof(this.ProgressPercentage));
        }

        private void ChangeTaskStatus(DownloadTask task, DownloadTaskStatus newStatus)
        {
            int index = this.DownloadTasks.IndexOf(task);

            this.DownloadTasks[index].Status = newStatus;

            this.OnPropertyChanged(nameof(this.DownloadTasks));
            this.OnPropertyChanged(nameof(this.DownloadTaskStepsCount));
            this.OnPropertyChanged(nameof(this.CompletedDownloadTaskStepsCount));
            this.OnPropertyChanged(nameof(this.ProgressPercentage));
        }

        private void CompleteTask(DownloadTask task)
        {
            this.ChangeTaskStatus(task, DownloadTaskStatus.Done);
        }

        private async Task<List<DownloadTask>?> ProcessDownloadTaskResultAsync(DownloadTask task)
        {
            this.ChangeTaskStatus(task, DownloadTaskStatus.Processing);

            if (string.IsNullOrWhiteSpace(task.Response))
            {
                this.ChangeTaskStatus(task, DownloadTaskStatus.Error);
            }
            else
            {
                task.ParsedEntity = await this.xmlFileService.ParseFileAsync(task.Response);

                if (task.ParsedEntity == null)
                {
                    this.ChangeTaskStatus(task, DownloadTaskStatus.Error);
                }
                else
                {
                    return this.xmlFileService.ExtractXmlDownloadTasks(task.ParsedEntity);
                }
            }

            return null;
        }

        private async Task StartDownloadTaskAsync(DownloadTask task)
        {
            this.ChangeTaskStatus(task, DownloadTaskStatus.Downloading);

            GetProtectedResourceRequest request = this.Authorizer.BuildProtectedResourseRequest(task);

            task.Response = await this.hattrickService.GetProtectedResourceAsync(request);
        }

        private async Task StoreDownloadTaskAsync(DownloadTask task)
        {
            if (task.Status == DownloadTaskStatus.Error || task.ParsedEntity == null)
            {
                return;
            }

            this.ChangeTaskStatus(task, DownloadTaskStatus.Saving);

            await this.xmlFileService.PersistAsync(task.ParsedEntity);
        }
    }
}