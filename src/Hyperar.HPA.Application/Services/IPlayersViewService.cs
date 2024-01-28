namespace Hyperar.HPA.Application.Services
{
    using Application.Models.Players;

    public interface IPlayersViewService
    {
        Task<Currency> GetManagerCurrencyAsync();

        Task<Player[]> GetPlayerAsync(uint teamId);
    }
}