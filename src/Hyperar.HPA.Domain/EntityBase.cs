namespace Hyperar.HPA.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HPA.Domain.Interfaces;

    public abstract class EntityBase : IEntity, ICloneable
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
