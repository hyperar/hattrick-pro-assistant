namespace Hyperar.HPA.Shared.Models.UI.Home
{
    using Hyperar.HPA.Shared.Enums;

    public class Manager
    {
        public Manager()
        {
            this.UserName = string.Empty;

            this.Country = new Country();
        }

        public byte[]? AvatarBytes { get; set; }

        public Country Country { get; set; }

        public long HattrickId { get; set; }

        public string UserName { get; set; }

        public SupporterTier SupporterTier { get; set; }

        public override string ToString()
        {
            return $"{this.UserName} ({this.HattrickId})";
        }
    }
}