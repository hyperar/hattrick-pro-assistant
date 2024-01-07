namespace Hyperar.HPA.Application.Models.Home
{
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