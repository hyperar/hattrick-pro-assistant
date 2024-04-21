namespace Hyperar.HPA.Application.Models.TeamSelection
{
    public class Series
    {
        public long HattrickId { get; set; }

        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}