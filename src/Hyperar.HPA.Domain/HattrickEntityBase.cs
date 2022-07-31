namespace Hyperar.HPA.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HPA.Domain.Interfaces;

    public class HattrickEntityBase : EntityBase, IEntity, IHattrickEntity
    {
        /// <summary>
        /// Gets or sets the Hattrick Id.
        /// </summary>
        public uint HattrickId { get; set; }
    }
}
