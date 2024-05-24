namespace Hyperar.HPA.Shared.Models.Hattrick.StaffAvatars
{
    public class Staff
    {
        public Staff()
        {
            this.Avatar = new Avatar();
        }

        public Avatar Avatar { get; set; }

        public long StaffId { get; set; }
    }
}