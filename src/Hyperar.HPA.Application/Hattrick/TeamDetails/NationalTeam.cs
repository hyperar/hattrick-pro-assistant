namespace Hyperar.HPA.Application.Hattrick.TeamDetails
{
    using Common.Enums;

    public class NationalTeam
    {
        public uint Index { get; set; }

        public uint NationalTeamId { get; set; }

        public string NationalTeamName { get; set; } = string.Empty;

        public NationalTeamStaffType NationalTeamStaffType { get; set; }
    }
}