namespace Hyperar.HPA.Application.Services
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task DeleteUserTokenAsync(int userId);

        Domain.User GetUser();

        Task<Domain.User> GetUserAsync();

        Task InsertUserTokenAsync(string token, string tokenSecret);

        Task UpdateUserLastDownloadDate();
    }
}