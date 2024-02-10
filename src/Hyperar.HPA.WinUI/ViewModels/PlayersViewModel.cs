namespace Hyperar.HPA.WinUI.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HPA.WinUI.State.Interface;

    public class PlayersViewModel : AsyncViewModelBase
    {
        public PlayersViewModel(INavigator navigator) : base(navigator)
        {
        }
    }
}