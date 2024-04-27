namespace Hyperar.HPA.WinUI.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using WinUI.Commands;
    using WinUI.Enums;
    using WinUI.Models;
    using WinUI.State;
    using WinUI.State.Interface;
    using WinUI.ViewModels.Interface;

    public partial class MainViewModel : ViewModelBase, IDisposable
    {
        [ObservableProperty]
        private ViewModelBase? currentPage;

        [ObservableProperty]
        private bool isMenuOpen;

        [ObservableProperty]
        private MenuItemTemplate? selectedItem;

        public MainViewModel() : base(new Navigator())
        {
        }

        public MainViewModel(
            INavigator navigator,
            IViewModelFactory viewModelFactory,
            ViewType viewType) : base(navigator)
        {
            this.Navigator.StateChanged += this.Navigator_StateChanged;

            this.MenuItems = new ObservableCollection<MenuItemTemplate>
            {
                //new MenuItemTemplate(Globalization.Translations.Home, ViewType.Home, "HomeIcon"),
                //new MenuItemTemplate(Globalization.Translations.Players, ViewType.Players),
                //new MenuItemTemplate(Globalization.Translations.Matches, ViewType.Matches),
                new MenuItemTemplate(Globalization.Translations.TeamSelection, ViewType.TeamSelection, "TeamSelectionIcon"),
                new MenuItemTemplate(Globalization.Translations.Download, ViewType.Download, "DownloadIcon"),
                //new MenuItemTemplate(Globalization.Translations.Settings, ViewType.Settings, "SettingsIcon"),
                new MenuItemTemplate(Globalization.Translations.Authorization, ViewType.Authorization, "AuthorizationIcon"),
                //new MenuItemTemplate(Globalization.Translations.About, ViewType.About, "AboutIcon")
            };

            this.UpdateCurrentPageCommand = new UpdateCurrentPageCommand(navigator, this, viewModelFactory);

            this.SelectedItem = this.MenuItems.Single(x => x.ViewType == viewType);
        }

        public bool CanNavigate
        {
            get
            {
                return this.Navigator.CanNavigate;
            }
        }

        public ObservableCollection<MenuItemTemplate> MenuItems { get; } = new ObservableCollection<MenuItemTemplate>();

        public ICommand? UpdateCurrentPageCommand { get; set; }

        public void Dispose()
        {
            this.Navigator.StateChanged -= this.Navigator_StateChanged;

            GC.SuppressFinalize(this);
        }

        private void Navigator_StateChanged()
        {
            this.OnPropertyChanged(nameof(this.CanNavigate));
        }

        partial void OnSelectedItemChanged(MenuItemTemplate? value)
        {
            if (value is null)
            {
                return;
            }

            this.UpdateCurrentPageCommand?.Execute(value.ViewType);
        }

        [RelayCommand]
        private void ToggleMenu()
        {
            this.IsMenuOpen = !this.IsMenuOpen;
        }
    }
}