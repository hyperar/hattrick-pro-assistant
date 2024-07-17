namespace Hyperar.HPA.WinUI.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Hyperar.HPA.Application.Services;
    using Microsoft.Extensions.DependencyInjection;
    using WinUI.Commands;
    using WinUI.Enums;
    using WinUI.Models;
    using WinUI.State.Interface;
    using WinUI.ViewModels.Interface;

    public partial class MainViewModel : AsyncViewModelBase, IDisposable
    {
        private const string AboutIconKey = "AboutIcon";

        private const string AuthorizationIconKey = "AuthorizationIcon";

        private const string DownloadIconKey = "DownloadIcon";

        private const string HomeIconKey = "HomeIcon";

        private const string MatchesIconKey = "MatchesIcon";

        private const string PlayersIconKey = "PlayersIcon";

        private const string SettingsIconKey = "SettingsIcon";

        private const string TeamSelectionIconKey = "TeamSelectionIcon";

        private readonly IServiceScopeFactory serviceScopeFactory;

        [ObservableProperty]
        private bool isMenuOpen;

        [ObservableProperty]
        private MenuItemTemplate? selectedItem;

        public MainViewModel(
            INavigator navigator,
            IServiceScopeFactory serviceScopeFactory,
            IViewModelFactory viewModelFactory) : base(navigator)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.Navigator.CanNavigateChanged += this.Navigator_CanNavigateChanged;
            this.Navigator.CurrentPageChanged += this.Navigator_CurrentPageChanged;
            this.Navigator.PageTypeChanged += this.Navigator_PageChanged;
            this.Navigator.SelectedTeamChanged += this.Navigator_SelectedTeamChanged;

            this.UpdateCurrentPageCommand = new UpdateCurrentPageCommand(navigator, viewModelFactory);
            this.MenuItems = new ObservableCollection<MenuItemTemplate>();
        }

        public bool CanNavigate
        {
            get
            {
                return this.Navigator.CanNavigate;
            }
        }

        public ViewModelBase? CurrentPage
        {
            get
            {
                return this.Navigator.CurrentPage;
            }
        }

        public ObservableCollection<MenuItemTemplate> MenuItems { get; } = new ObservableCollection<MenuItemTemplate>();

        public ICommand? UpdateCurrentPageCommand { get; set; }

        public void Dispose()
        {
            this.Navigator.CanNavigateChanged -= this.Navigator_CanNavigateChanged;
            this.Navigator.CurrentPageChanged -= this.Navigator_CurrentPageChanged;
            this.Navigator.PageTypeChanged -= this.Navigator_PageChanged;
            this.Navigator.SelectedTeamChanged -= this.Navigator_SelectedTeamChanged;

            GC.SuppressFinalize(this);
        }

        public override async Task InitializeAsync()
        {
            long? selectedTeamHattrickId = null;

            using (var scope = this.serviceScopeFactory.CreateScope())
            {
                selectedTeamHattrickId = (await scope.ServiceProvider.GetRequiredService<IUserService>().GetUserAsync()).SelectedTeamHattrickId;
            }

            this.Navigator.SetSelectedTeam(selectedTeamHattrickId ?? 0);

            await base.InitializeAsync();
        }

        private ViewType BuildMenuItems(Domain.User user)
        {
            ViewType viewType;

            this.MenuItems.Clear();

            if (user.Token == null)
            {
                viewType = ViewType.Authorization;

                this.MenuItems.Add(
                    new MenuViewItemTemplate(
                        Globalization.Translations.Authorization,
                        ViewType.Authorization,
                        AuthorizationIconKey));
            }
            else if (user.LastDownloadDate == null)
            {
                viewType = ViewType.Download;

                this.MenuItems.Add(
                    new MenuViewItemTemplate(
                        Globalization.Translations.Download,
                        ViewType.Download,
                        DownloadIconKey));

                this.MenuItems.Add(
                    new MenuViewItemTemplate(
                        Globalization.Translations.Authorization,
                        ViewType.Authorization,
                        AuthorizationIconKey));
            }
            else
            {
                ArgumentNullException.ThrowIfNull(user.SelectedTeamHattrickId, nameof(user.SelectedTeamHattrickId));
                ArgumentNullException.ThrowIfNull(user.Manager, nameof(user.Manager));

                viewType = ViewType.Home;

                this.MenuItems.Add(
                    new MenuViewItemTemplate(
                        Globalization.Translations.Home,
                        ViewType.Home,
                        HomeIconKey));

                if (user.Manager.SeniorTeams.Count > 1)
                {
                    this.MenuItems.Add(
                        new MenuViewItemTemplate(
                            Globalization.Translations.TeamSelection,
                            ViewType.TeamSelection,
                            TeamSelectionIconKey));
                }

                this.MenuItems.Add(
                    new SeparatorItemTemplate());

                this.MenuItems.Add(
                    new MenuViewItemTemplate(
                        Globalization.Translations.Players,
                        ViewType.Players,
                        PlayersIconKey));

                this.MenuItems.Add(
                    new MenuViewItemTemplate(
                        Globalization.Translations.Matches,
                        ViewType.Matches,
                        MatchesIconKey));

                var selectedTeam = user.Manager.SeniorTeams.Single(x => x.HattrickId == user.SelectedTeamHattrickId);

                if (selectedTeam.JuniorTeam != null)
                {
                    this.MenuItems.Add(
                        new SeparatorItemTemplate());

                    this.MenuItems.Add(
                        new MenuViewItemTemplate(
                            Globalization.Translations.JuniorPlayers,
                            ViewType.JuniorPlayers,
                            PlayersIconKey));

                    this.MenuItems.Add(
                        new MenuViewItemTemplate(
                            Globalization.Translations.JuniorMatches,
                            ViewType.JuniorMatches,
                            MatchesIconKey));
                }

                this.MenuItems.Add(
                    new SeparatorItemTemplate());

                this.MenuItems.Add(
                    new MenuViewItemTemplate(
                        Globalization.Translations.Download,
                        ViewType.Download,
                        DownloadIconKey));

                this.MenuItems.Add(
                    new MenuViewItemTemplate(
                        Globalization.Translations.Settings,
                        ViewType.Settings,
                        SettingsIconKey));

                this.MenuItems.Add(
                    new MenuViewItemTemplate(
                        Globalization.Translations.Authorization,
                        ViewType.Authorization,
                        AuthorizationIconKey));

                this.MenuItems.Add(
                    new MenuViewItemTemplate(
                        Globalization.Translations.About,
                        ViewType.About,
                        AboutIconKey));
            }

            return viewType;
        }

        private void Navigator_CanNavigateChanged()
        {
            this.OnPropertyChanged(nameof(this.CanNavigate));
        }

        private void Navigator_CurrentPageChanged()
        {
            this.OnPropertyChanged(nameof(this.CurrentPage));
        }

        private void Navigator_PageChanged()
        {
            this.SelectedItem = this.MenuItems.Where(x => x is MenuViewItemTemplate)
                .Select(x => x as MenuViewItemTemplate)
                .Where(x => x is not null)
                .SingleOrDefault(x => x?.ViewType == this.Navigator.PageType);
        }

        private async void Navigator_SelectedTeamChanged()
        {
            ViewType viewType;

            using (var scope = this.serviceScopeFactory.CreateScope())
            {
                var user = await scope.ServiceProvider.GetRequiredService<IUserService>().GetUserAsync();

                viewType = this.BuildMenuItems(user);
            }

            this.SelectedItem = this.MenuItems.Where(x => x is MenuViewItemTemplate)
                .Select(x => x as MenuViewItemTemplate)
                .Where(x => x is not null)
                .SingleOrDefault(x => x?.ViewType == viewType);
        }

        partial void OnSelectedItemChanged(MenuItemTemplate? value)
        {
            if (value is null)
            {
                return;
            }

            if (value is MenuViewItemTemplate menuViewItemTemplate)
            {
                this.UpdateCurrentPageCommand?.Execute(menuViewItemTemplate.ViewType);
            }
        }

        [RelayCommand]
        private void ToggleMenu()
        {
            this.IsMenuOpen = !this.IsMenuOpen;
        }
    }
}