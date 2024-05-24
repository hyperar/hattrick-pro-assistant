namespace Hyperar.HPA.Shared.Models.Hattrick.StaffAvatars
{
    public class Trainer
    {
        public Trainer()
        {
            this.Avatar = new Avatar();
        }

        public Avatar Avatar { get; set; }

        public long TrainerId { get; set; }
    }
}