namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class MatchOfficials
    {
        public MatchOfficials()
        {
            this.Referee = new Referee();
            this.RefereeAssistant1 = new Referee();
            this.RefereeAssistant2 = new Referee();
        }

        public Referee Referee { get; set; }

        public Referee RefereeAssistant1 { get; set; }

        public Referee RefereeAssistant2 { get; set; }
    }
}