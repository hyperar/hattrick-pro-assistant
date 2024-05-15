namespace Hyperar.HPA.Shared.Models.Hattrick.MatchLineUp
{
    public class StartingPlayer
    {
        public StartingPlayer()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
        }

        public int? Behaviour { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? NickName { get; set; }

        public long PlayerId { get; set; }

        public int RoleId { get; set; }
    }
}