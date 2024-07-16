namespace Hyperar.HPA.Shared.Models.UI.Home
{
    public class MatchTeam
    {
        public MatchTeam()
        {
            this.Name = string.Empty;
        }

        public long HattrickId { get; set; }

        public string Name { get; set; }
    }
}