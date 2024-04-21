namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using Shared.Enums;

    public class NationalTeam
    {
        public NationalTeam()
        {
            this.NationalTeamName = string.Empty;
        }

        public byte Index { get; set; }

        public long NationalTeamId { get; set; }

        public string NationalTeamName { get; set; }

        public NationalTeamStaffType NationalTeamStaffType { get; set; }
    }
}