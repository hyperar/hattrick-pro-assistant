namespace Hyperar.HPA.Domain
{
    using System;
    using Hyperar.HPA.Domain.Interfaces;

    public class Token : EntityBase, IEntity
    {
        public DateTime TokenCreatedOn { get; set; }

        public DateTime TokenExpiresOn { get; set; }

        public string TokenSecretValue { get; set; } = string.Empty;

        public string TokenValue { get; set; } = string.Empty;
    }
}