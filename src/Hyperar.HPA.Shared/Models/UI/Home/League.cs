namespace Hyperar.HPA.Shared.Models.UI.Home
{
    using System;

    public class League
    {
        public League()
        {
            this.Name = string.Empty;
            this.FlagBytes = Array.Empty<byte>();
        }

        public byte[] FlagBytes { get; set; }

        public long HattrickId { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}