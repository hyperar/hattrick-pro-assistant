namespace Hyperar.HPA.Shared.Models.Hattrick.ManagerCompendium
{
    public class YouthTeam
    {
        public YouthTeam()
        {
            this.YouthTeamName = string.Empty;
        }

        public IdName? YouthLeague { get; set; }

        public long YouthTeamId { get; set; }

        public string YouthTeamName { get; set; }
    }
}