namespace Hyperar.HPA.Application.Models.Home
{
    public class PlayedMatch : Match
    {
        public uint AwayGoals { get; set; }

        public uint HomeGoals { get; set; }
    }
}