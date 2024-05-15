namespace Hyperar.HPA.Shared.Models.Hattrick
{
    public class Layer
    {
        public Layer()
        {
            this.Image = string.Empty;
        }

        public string Image { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}