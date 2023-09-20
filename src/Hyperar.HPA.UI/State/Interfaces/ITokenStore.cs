namespace Hyperar.HPA.UI.State.Interfaces
{
    using System.ComponentModel;
    using Hyperar.HPA.Domain;

    public interface ITokenStore
    {
        event PropertyChangedEventHandler? PropertyChanged;

        Token? CurrentToken { get; }

        void SetCurrentToken(Token? token);
    }
}