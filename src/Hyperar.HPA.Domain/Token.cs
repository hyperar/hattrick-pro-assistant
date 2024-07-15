namespace Hyperar.HPA.Domain
{
    using System;
    using Domain.Interfaces;
    using Shared.Enums;

    public class Token : EntityBase, IEntity
    {
        public DateTime ExpiresOn { get; set; }

        public DateTime GeneratedOn { get; set; }

        public ChppScope Scope { get; set; }

        public string Secret { get; set; } = string.Empty;

        public virtual User User { get; set; } = new User();

        public int UserId { get; set; }

        public string Value { get; set; } = string.Empty;
    }
}