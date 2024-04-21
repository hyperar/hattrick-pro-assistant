namespace Hyperar.HPA.Application.Models.Home
{
    using Shared.Enums;

    public class Manager
    {
        public byte[]? Avatar { get; set; }

        public Country Country { get; set; } = new Country();

        public long HattrickId { get; set; }

        public SupporterTier SupporterTier { get; set; }

        public string UserName { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{this.UserName} ({this.HattrickId})";
        }
    }
}