namespace Hyperar.HPA.UserInterface.State.Interfaces
{
    using System;
    using Hyperar.HPA.UserInterface.ViewModels;

    public interface INavigator
    {
        ViewModelBase? CurrentViewModel { get; set; }

        event Action? StateChanged;
    }
}
