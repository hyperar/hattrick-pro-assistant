namespace Hyperar.HPA.Infrastructure.Services
{
    using System;
    using System.Threading.Tasks;
    using Application.Services;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly IDatabaseContext context;

        private readonly IRepository<Domain.Token> tokenRepository;

        private readonly IRepository<Domain.User> userRepository;

        public UserService(
            IDatabaseContext context,
            IRepository<Domain.User> userRepository,
            IRepository<Domain.Token> tokenRepository)
        {
            this.context = context;
            this.userRepository = userRepository;
            this.tokenRepository = tokenRepository;
        }

        public async Task DeleteUserTokenAsync(int userId)
        {
            Domain.Token token = await this.tokenRepository.Query(x => x.UserId == userId).SingleAsync();

            await this.tokenRepository.DeleteAsync(token.Id);

            await this.context.SaveAsync();
        }

        public async Task<Domain.User> GetUserAsync()
        {
            Domain.User? user = await this.userRepository.Query().SingleOrDefaultAsync();

            if (user == null)
            {
                user = new Domain.User();

                await this.userRepository.InsertAsync(user);

                await this.context.SaveAsync();
            }

            return user;
        }

        public async Task InsertUserTokenAsync(string token, string tokenSecret)
        {
            Domain.User storedUser = await this.userRepository.Query().SingleAsync();

            Domain.Token newToken = new Domain.Token
            {
                User = storedUser,
                GeneratedOn = DateTime.Now,
                ExpiresOn = DateTime.MaxValue,
                Value = token,
                Secret = tokenSecret
            };

            await this.tokenRepository.InsertAsync(newToken);

            await this.context.SaveAsync();
        }

        public async Task UpdateUserLastDownloadDate()
        {
            Domain.User storedUser = await this.userRepository.Query().SingleAsync();

            storedUser.LastDownloadDate = DateTime.Now;

            this.userRepository.Update(storedUser);

            await this.context.SaveAsync();
        }
    }
}