namespace Hyperar.HPA.Infrastructure.Services
{
    using System;
    using Hyperar.HPA.Application.Services;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TokenService : ITokenService
    {
        private readonly IDatabaseContext context;

        private readonly IRepository<Token> tokenRepository;

        public TokenService(IDatabaseContext context, IRepository<Token> tokenRepository)
        {
            this.context = context;
            this.tokenRepository = tokenRepository;
        }

        public async Task DeleteTokenAsync(string token, string tokenSecret)
        {
            var storedToken = await this.tokenRepository.Query().SingleAsync();

            await this.tokenRepository.DeleteAsync(storedToken.Id);

            await this.context.SaveAsync();
        }

        public async Task<Token?> GetTokenAsync()
        {
            return await this.tokenRepository.Query().SingleOrDefaultAsync();
        }

        public async Task InsertTokenAsync(string token, string tokenSecret)
        {
            await this.tokenRepository.InsertAsync(
                new Token
                {
                    TokenValue = token,
                    TokenSecretValue = tokenSecret,
                    TokenCreatedOn = DateTime.Now,
                    TokenExpiresOn = DateTime.MaxValue
                });

            await this.context.SaveAsync();
        }
    }
}