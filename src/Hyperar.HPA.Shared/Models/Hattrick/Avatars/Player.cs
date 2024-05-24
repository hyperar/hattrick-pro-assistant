namespace Hyperar.HPA.Shared.Models.Hattrick.Avatars
{
    using Shared.Models.Hattrick;

    public class Player
    {
        public Player()
        {
            this.Avatar = new Avatar();
        }

        public Avatar Avatar { get; set; }

        public long PlayerId { get; set; }
    }
}