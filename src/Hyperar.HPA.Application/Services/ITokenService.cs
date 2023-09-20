namespace Hyperar.HPA.Application.Services
{
    using Hyperar.HPA.Domain;

    public interface ITokenService
    {
        void DeleteToken(string token, string tokenSecret);

        Token? GetToken();

        void InsertToken(string token, string tokenSecret);
    }
}