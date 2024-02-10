namespace Hyperar.HPA.WinUI
{
    using System;
    using System.Collections.Generic;
    using Avalonia.Controls;
    using Avalonia.Controls.Templates;
    using WinUI.ViewModels;

    public class ViewLocator : IDataTemplate
    {
        private readonly Dictionary<Type, Func<Control?>> locator = new Dictionary<Type, Func<Control?>>();

        public ViewLocator()
        {
            this.RegisterViewFactory<HomeViewModel, HomePageView>();
            this.RegisterViewFactory<PlayersViewModel, PlayersPageView>();
            this.RegisterViewFactory<MatchesViewModel, MatchesPageView>();
            this.RegisterViewFactory<TeamSelectionViewModel, TeamSelectionPageView>();
            this.RegisterViewFactory<DownloadViewModel, DownloadPageView>();
            this.RegisterViewFactory<SettingsViewModel, SettingsPageView>();
            this.RegisterViewFactory<AuthorizationViewModel, AuthorizationPageView>();
            this.RegisterViewFactory<AboutViewModel, AboutPageView>();
        }

        public Control? Build(object? data)
        {
            if (data is null)
            {
                return null;
            }

            this.locator.TryGetValue(data.GetType(), out var factory);

            return factory?.Invoke() ?? new TextBlock { Text = $"VM Not Registered: {data.GetType()}" };
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }

        private void RegisterViewFactory<TViewModel, TView>() where TViewModel : class where TView : Control
        {
            this.locator.Add(typeof(TViewModel), Activator.CreateInstance<TView>);
        }
    }
}