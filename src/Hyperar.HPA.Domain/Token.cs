namespace Hyperar.HPA.Domain
{
    using System;
    using Domain.Interfaces;
    using Shared.Enums;

    public class Token : EntityBase, IEntity
    {
        public Token()
        {
            this.User = new User();

            this.SecretValue = string.Empty;
            this.Value = string.Empty;
        }

        public DateTime CreatedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public ChppScope Scope { get; set; }

        public string SecretValue { get; set; }

        public virtual User User { get; set; }

        public int UserId { get; set; }

        public string Value { get; set; }
    }
}