namespace Hyperar.HPA.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Domain.Interfaces;

    public class World : EntityBase, IEntity
    {
        public uint Season { get; set; }

        public uint Week { get; set; }
    }
}