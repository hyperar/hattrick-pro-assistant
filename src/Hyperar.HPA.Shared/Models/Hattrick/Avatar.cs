namespace Hyperar.HPA.Shared.Models.Hattrick
{
    using System.Collections.Generic;

    public class Avatar
    {
        public Avatar()
        {
            this.Layers = new List<Layer>();
        }

        public string BackgroundImage { get; set; } = string.Empty;

        public List<Layer> Layers { get; set; }
    }
}