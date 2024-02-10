namespace Hyperar.HPA.WinUI.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Hyperar.HPA.WinUI.State;
    using Hyperar.HPA.WinUI.State.Interface;
    using WinUI.Commands;
    using WinUI.Enums;
    using WinUI.Models;
    using WinUI.ViewModels.Interface;

    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly ITeamSelector teamSelector;

        [ObservableProperty]
        private ViewModelBase? currentPage;

        [ObservableProperty]
        private bool isMenuOpen;

        [ObservableProperty]
        private MenuItemTemplate selectedItem;

        public MainWindowViewModel(
            INavigator navigator,
            ITeamSelector teamSelector,
            IViewModelFactory viewModelFactory,
            ViewType viewType) : base(navigator)
        {
            this.teamSelector = teamSelector;

            this.MenuItems = new ObservableCollection<MenuItemTemplate>
            {
                new MenuItemTemplate(Globalization.Strings.Home, ViewType.Home, "HomeIcon"),
                new MenuItemTemplate(Globalization.Strings.Players, ViewType.Players),
                new MenuItemTemplate(Globalization.Strings.Matches, ViewType.Matches),
                new MenuItemTemplate(Globalization.Strings.TeamSelection, ViewType.TeamSelection, "TeamSelectionIcon"),
                new MenuItemTemplate(Globalization.Strings.Download, ViewType.Download, "DownloadIcon"),
                new MenuItemTemplate(Globalization.Strings.Settings, ViewType.Settings, "SettingsIcon"),
                new MenuItemTemplate(Globalization.Strings.Authorization, ViewType.Authorization, "AuthorizationIcon"),
                new MenuItemTemplate(Globalization.Strings.About, ViewType.About, "AboutIcon")
            };

            this.UpdateCurrentPageCommand = new UpdateCurrentPageCommand(navigator, this, viewModelFactory);

            this.SelectedItem = this.MenuItems.Single(x => x.ViewType == viewType);

            this.Navigator.StateChanged += this.Navigator_StateChanged;

            this.Navigator.ResumeNavigation();
        }

        public bool CanNavigate
        {
            get
            {
                return this.Navigator.CanNavigate;
            }
        }

        public ObservableCollection<MenuItemTemplate> MenuItems { get; }

        public ICommand UpdateCurrentPageCommand { get; set; }

        private void Navigator_StateChanged()
        {
            this.OnPropertyChanged(nameof(this.CanNavigate));
        }

        partial void OnSelectedItemChanged(MenuItemTemplate value)
        {
            if (value is null)
            {
                return;
            }

            this.UpdateCurrentPageCommand.Execute(value.ViewType);
        }

        [RelayCommand]
        private void ToggleMenu()
        {
            this.IsMenuOpen = !this.IsMenuOpen;
        }
    }
}