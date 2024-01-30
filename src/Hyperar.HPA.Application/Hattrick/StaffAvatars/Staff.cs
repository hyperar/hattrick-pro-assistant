namespace Hyperar.HPA.Application.Hattrick.StaffAvatars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Staff
    {
        public Avatar Avatar { get; set; } = new Avatar();

        public uint StaffId { get; set; }
    }
}