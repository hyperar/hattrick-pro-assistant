namespace Hyperar.HPA.UserInterface.State.Interfaces
{
    using System.ComponentModel;
    using Hyperar.HPA.Domain;

    public interface ITokenStore
    {
        Token? CurrentToken { get; }

        event PropertyChangedEventHandler? PropertyChanged;

        void SetCurrentToken(Token? token);
    }
}
