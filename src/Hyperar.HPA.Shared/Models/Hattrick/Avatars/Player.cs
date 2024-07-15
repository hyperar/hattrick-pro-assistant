namespace Hyperar.HPA.Shared.Models.Hattrick.Avatars
{
    using Shared.Models.Hattrick;

    public class Player
    {
        public Avatar Avatar { get; set; } = new Avatar();

        public long PlayerId { get; set; }
    }
}