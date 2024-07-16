namespace Hyperar.HPA.Shared.Models.Hattrick
{
    using System.Collections.Generic;

    public class Avatar
    {
        public string BackgroundImage { get; set; } = string.Empty;

        public List<Layer> Layers { get; set; } = new List<Layer>();
    }
}