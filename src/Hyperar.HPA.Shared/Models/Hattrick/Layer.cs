namespace Hyperar.HPA.Shared.Models.Hattrick
{
    public class Layer
    {
        public Layer()
        {
            this.Image = string.Empty;
        }

        public string Image { get; set; }

        public byte X { get; set; }

        public byte Y { get; set; }
    }
}