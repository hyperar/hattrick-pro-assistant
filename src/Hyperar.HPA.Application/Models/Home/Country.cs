namespace Hyperar.HPA.Application.Models.Home
{
    public class Country
    {
        public uint HattrickId { get; set; }

        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}