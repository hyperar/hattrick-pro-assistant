namespace Hyperar.HPA.Shared.Models.Hattrick.YouthAvatars
{
    using Shared.Models.Hattrick;

    public class YouthPlayer
    {
        public Avatar Avatar { get; set; } = new Avatar();

        public long PlayerId { get; set; }
    }
}