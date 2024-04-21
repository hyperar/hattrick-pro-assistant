namespace Hyperar.HPA.Shared.Models.Hattrick.ManagerCompendium
{
    public class Currency
    {
        public Currency()
        {
            this.CurrencyName = string.Empty;
        }

        public string CurrencyName { get; set; }

        public decimal CurrencyRate { get; set; }
    }
}