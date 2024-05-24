namespace Hyperar.HPA.Application.Services
{
    using Shared.Models.UI.Players;

    public interface IPlayersViewService
    {
        Task<Currency> GetManagerCurrencyAsync();

        Task<Player[]> GetPlayersAsync(long teamId);
    }
}