namespace Hyperar.HPA.BusinessContracts
{
    using Hyperar.HPA.Domain;

    public interface ITokenService
    {
        void InsertToken(string token, string tokenSecret);

        Token? GetToken();

        void DeleteToken(string token, string tokenSecret);
    }
}
