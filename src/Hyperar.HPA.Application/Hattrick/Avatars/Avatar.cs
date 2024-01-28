namespace Hyperar.HPA.Application.Hattrick.Avatars
{
    using System.Collections.Generic;

    public class Avatar
    {
        public string BackgroundImage { get; set; } = string.Empty;

        public List<Layer> Layers { get; set; } = new List<Layer>();
    }
}