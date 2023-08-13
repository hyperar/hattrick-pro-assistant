namespace Hyperar.HPA.Domain
{
    using System;
    using Hyperar.HPA.Domain.Interfaces;

    public class User : EntityBase, IEntity
    {
        public string Token { get; set; } = string.Empty;

        public DateTime TokenCreatedOn { get; set; }

        public DateTime TokenExpiresOn { get; set; }

        public string TokenSecret { get; set; } = string.Empty;
    }
}