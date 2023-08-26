namespace Hyperar.HPA.UserInterface.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.UserInterface.Commands;
    using Hyperar.HPA.UserInterface.State.Interfaces;

    public class DownloadViewModel : AuthorizedViewModelBase
    {
        private readonly IHattrickService hattrickService;

        private readonly IXmlFileService xmlFileService;

        private List<DownloadTask> downloadTasks;

        public DownloadViewModel(IAuthorizer authorizer, IHattrickService hattrickService, IXmlFileService xmlFileService) : base(authorizer)
        {
            this.downloadTasks = new List<DownloadTask>();

            this.DownloadFilesCommand = new DownloadFilesCommand(this);
            this.hattrickService = hattrickService;
            this.xmlFileService = xmlFileService;
        }

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

        public DownloadTask? CurrentDownloadTask
        {
            get
            {
                return this.DownloadTasks.Where(x => x.Status != DownloadTaskStatus.Done).FirstOrDefault();
            }
        }

        public int DownloadTaskStepsCount
        {
            get
            {
                // To avoid full progress bar on 0% completion.
                if (this.downloadTasks.Count == 0)
                {
                    return 1;
                }
                return this.downloadTasks.Count * 4;
            }
        }

        public int CompletedDownloadTaskStepsCount
        {
            get
            {
                return this.downloadTasks.Sum(x => (int)x.Status);
            }
        }

        public int ProgressPercentage
        {
            get
            {
                if (this.DownloadTaskStepsCount == 0)
                {
                    return 0;
                }

                return (int)(this.CompletedDownloadTaskStepsCount / (decimal)this.DownloadTaskStepsCount * 100m);
            }
        }

        public ICommand DownloadFilesCommand { get; }

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

        public DownloadTask? GetNextDownloadTask()
        {
            return this.DownloadTasks.FirstOrDefault(x => x.Status == DownloadTaskStatus.Pending);
        }

        public void ExecuteDownloadTask(DownloadTask task)
        {
            this.StartDownloadTask(task);

            var childTasks = this.ProcessDownloadTaskResult(task);

            if (childTasks != null)
            {
                this.AddChildTasks(task, childTasks);
            }

            this.StoreDownloadTask(task);

            this.CompleteTask(task);
        }

        private void StartDownloadTask(DownloadTask task)
        {
            this.ChangeTaskStatus(task, DownloadTaskStatus.Downloading);

            var request = this.Authorizer.BuildProtectedResourseRequest(task);

            task.Response = this.hattrickService.GetProtectedResource(request);
        }

        private List<DownloadTask>? ProcessDownloadTaskResult(DownloadTask task)
        {
            this.ChangeTaskStatus(task, DownloadTaskStatus.Processing);

            if (string.IsNullOrWhiteSpace(task.Response))
            {
                this.ChangeTaskStatus(task, DownloadTaskStatus.Error);
            }
            else
            {
                task.ParsedEntity = this.xmlFileService.ParseFile(task.Response);

                return this.xmlFileService.GetChildDownloadTaskList(task.ParsedEntity);
            }

            return null;
        }

        private void StoreDownloadTask(DownloadTask task)
        {
            this.ChangeTaskStatus(task, DownloadTaskStatus.Storing);
        }

        private void CompleteTask(DownloadTask task)
        {
            this.ChangeTaskStatus(task, DownloadTaskStatus.Done);
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

        private void AddChildTasks(DownloadTask task, List<DownloadTask>? childDownloadTasks)
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
    }
}
