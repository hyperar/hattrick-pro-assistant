namespace Hyperar.HPA.WinUI.State
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WinUI.State.Interface;

    public class TeamSelector : ITeamSelector
    {
        public uint SelectedTeamId { get; private set; }

        public void SetSelectedTeam(uint selectedTeamId)
        {
            this.SelectedTeamId = selectedTeamId;
        }
    }
}