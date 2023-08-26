namespace Hyperar.HPA.Domain.Database
{
    using System;
    using Hyperar.HPA.Domain.Interfaces;

    public class Token : EntityBase, IEntity
    {
        public string TokenValue { get; set; } = string.Empty;

        public DateTime TokenCreatedOn { get; set; }

        public DateTime TokenExpiresOn { get; set; }

        public string TokenSecretValue { get; set; } = string.Empty;
    }
}