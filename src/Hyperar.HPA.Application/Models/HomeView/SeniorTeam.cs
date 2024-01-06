namespace Hyperar.HPA.Application.Models.HomeView
{
    using System;

    public class SeniorTeam
    {
        public uint HattrickId { get; set; }

        public byte[]? Logo { get; set; }

        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}