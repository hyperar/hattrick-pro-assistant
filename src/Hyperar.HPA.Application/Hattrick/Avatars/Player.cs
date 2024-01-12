namespace Hyperar.HPA.Application.Hattrick.Avatars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Player
    {
        public Avatar Avatar { get; set; } = new Avatar();

        public uint PlayerId { get; set; }
    }
}