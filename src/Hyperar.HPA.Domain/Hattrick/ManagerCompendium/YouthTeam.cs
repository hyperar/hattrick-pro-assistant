namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    public class YouthTeam
    {
        public uint YouthTeamId { get; set; }

        public string YouthTeamName { get; set; } = string.Empty;

        public YouthLeague? YouthLeague { get; set; } = null;
    }
}
