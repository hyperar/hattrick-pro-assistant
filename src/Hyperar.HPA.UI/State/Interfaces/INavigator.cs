namespace Hyperar.HPA.UI.State.Interfaces
{
    using System;
    using Hyperar.HPA.UI.ViewModels;

    public interface INavigator
    {
        event Action? StateChanged;

        ViewModelBase? CurrentViewModel { get; set; }
    }
}