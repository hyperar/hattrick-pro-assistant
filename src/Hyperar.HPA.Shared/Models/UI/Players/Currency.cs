namespace Hyperar.HPA.Shared.Models.UI.Players
{
    public class Currency
    {
        public Currency()
        {
            this.Name = string.Empty;
        }

        public string Name { get; set; }

        public decimal Rate { get; set; }
    }
}