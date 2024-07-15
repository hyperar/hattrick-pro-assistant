namespace Hyperar.HPA.Shared.Models.Hattrick.YouthPlayerList
{
    public class OwningYouthTeam
    {
        public IdName SeniorTeam { get; set; } = new IdName();

        public long YouthTeamId { get; set; }

        public long? YouthTeamLeagueId { get; set; }

        public string YouthTeamName { get; set; } = string.Empty;
    }
}