namespace Hyperar.HPA.WinUI.State.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITeamSelector
    {
        uint SelectedTeamId { get; }

        public void SetSelectedTeam(uint selectedTeam);
    }
}