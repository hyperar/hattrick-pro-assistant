namespace Hyperar.HPA.Domain
{
    using System;
    using Hyperar.HPA.Domain.Interfaces;

    public abstract class EntityBase : IEntity, ICloneable
    {
        public int Id { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}