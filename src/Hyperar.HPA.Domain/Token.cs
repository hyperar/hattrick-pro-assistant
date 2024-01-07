namespace Hyperar.HPA.Domain
{
    using System;
    using Domain.Interfaces;

    public class Token : EntityBase, IEntity
    {
        public DateTime CreatedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public string SecretValue { get; set; } = string.Empty;

        public virtual User User { get; set; } = new User();

        public int UserId { get; set; }

        public string Value { get; set; } = string.Empty;
    }
}