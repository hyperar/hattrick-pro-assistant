namespace Hyperar.HPA.Application.Models.Main
{
    public class Team
    {
        public uint HattrickId { get; set; }

        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}