namespace Hyperar.HPA.Application.Services
{
    using Hyperar.HPA.Domain;

    public interface ITokenService
    {
        Task DeleteTokenAsync(string token, string tokenSecret);

        Task<Token?> GetTokenAsync();

        Task InsertTokenAsync(string token, string tokenSecret);
    }
}