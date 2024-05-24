namespace Hyperar.HPA.Shared.Models.UI.Home
{
    public class Region
    {
        public Region()
        {
            this.Name = string.Empty;
        }

        public long HattrickId { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}