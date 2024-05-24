namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    public class NationalTeam
    {
        public NationalTeam()
        {
            this.NationalTeamName = string.Empty;
        }

        public int Index { get; set; }

        public long NationalTeamId { get; set; }

        public string NationalTeamName { get; set; }

        public int NationalTeamStaffType { get; set; }
    }
}