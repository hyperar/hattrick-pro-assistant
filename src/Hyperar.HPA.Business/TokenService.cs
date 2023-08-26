namespace Hyperar.HPA.Business
{
    using System;
    using System.Linq;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Data;
    using Hyperar.HPA.Domain.Database;

    public class TokenService : ITokenService
    {
        private readonly DatabaseContextFactory databaseContextFactory;

        public TokenService(DatabaseContextFactory databaseContextFactory)
        {
            this.databaseContextFactory = databaseContextFactory;
        }

        public void DeleteToken(string token, string tokenSecret)
        {
            using (var context = this.databaseContextFactory.CreateDbContext())
            {
                context.Tokens.Remove(context.Tokens.Single());

                context.Save();
            }
        }

        public Token? GetToken()
        {
            using (var context = this.databaseContextFactory.CreateDbContext())
            {
                return context.Tokens.SingleOrDefault();
            }
        }

        public void InsertToken(string token, string tokenSecret)
        {
            using (var context = this.databaseContextFactory.CreateDbContext())
            {
                context.Tokens.Add(new Token
                {
                    TokenValue = token,
                    TokenSecretValue = tokenSecret,
                    TokenCreatedOn = DateTime.Now,
                    TokenExpiresOn = DateTime.MaxValue
                });

                context.Save();
            }
        }
    }
}
