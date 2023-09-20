namespace Hyperar.HPA.Infrastructure.Services
{
    using System;
    using System.Linq;
    using Hyperar.HPA.Application.Services;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Interfaces;
    using Hyperar.HPA.Infrastructure.DataAccess;

    public class TokenService : ITokenService
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Token> tokenRepository;

        public TokenService(IDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            this.tokenRepository = new Repository<Token>(this.databaseContext);
        }

        public void DeleteToken(string token, string tokenSecret)
        {
            this.tokenRepository.Delete(this.tokenRepository.Query().Single().Id);

            this.databaseContext.Save();
        }

        public Token? GetToken()
        {
            return this.tokenRepository.Query().SingleOrDefault();
        }

        public void InsertToken(string token, string tokenSecret)
        {
            this.tokenRepository.Insert(
                new Token
                {
                    TokenValue = token,
                    TokenSecretValue = tokenSecret,
                    TokenCreatedOn = DateTime.Now,
                    TokenExpiresOn = DateTime.MaxValue
                });

            this.databaseContext.Save();
        }
    }
}