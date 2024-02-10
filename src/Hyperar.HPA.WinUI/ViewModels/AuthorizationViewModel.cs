namespace Hyperar.HPA.WinUI.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.Input;
    using Hyperar.HPA.WinUI.State.Interface;

    public partial class AuthorizationViewModel : AsyncViewModelBase
    {
        public AuthorizationViewModel(INavigator navigator) : base(navigator)
        {
        }

        [RelayCommand]
        public async Task GetAuthorizationCodeAsync()
        {
        }
    }
}