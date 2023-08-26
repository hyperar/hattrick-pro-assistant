namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using Hyperar.HPA.Common.Enums;

    public class NationalTeam
    {
        public uint Index { get; set; }

        public NationalTeamStaffType NationalTeamStaffType { get; set; }

        public uint NationalTeamId { get; set; }

        public string NationalTeamName { get; set; } = string.Empty;
    }
}
