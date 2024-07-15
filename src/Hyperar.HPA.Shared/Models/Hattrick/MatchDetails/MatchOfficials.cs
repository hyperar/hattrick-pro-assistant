namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class MatchOfficials
    {
        public Referee Referee { get; set; } = new Referee();

        public Referee RefereeAssistant1 { get; set; } = new Referee();

        public Referee RefereeAssistant2 { get; set; } = new Referee();
    }
}