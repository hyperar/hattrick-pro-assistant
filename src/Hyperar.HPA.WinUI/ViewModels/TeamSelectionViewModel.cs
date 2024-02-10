namespace Hyperar.HPA.WinUI.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms.VisualStyles;
    using Hyperar.HPA.WinUI.State.Interface;

    public class TeamSelectionViewModel : AsyncViewModelBase
    {
        private readonly ITeamSelector teamSelector;

        public TeamSelectionViewModel(
            INavigator navigator,
            ITeamSelector teamSelector) : base(navigator)
        {
            this.teamSelector = teamSelector;
        }
    }
}