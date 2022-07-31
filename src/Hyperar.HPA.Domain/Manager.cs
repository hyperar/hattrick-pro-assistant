namespace Hyperar.HPA.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HPA.Domain.Interfaces;

    public class Manager : HattrickEntityBase, IEntity, IHattrickEntity
    {
        /// <summary>
        /// Gets or sets the UserName.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }
}
