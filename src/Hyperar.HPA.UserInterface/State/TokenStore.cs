namespace Hyperar.HPA.UserInterface.State
{
    using System.ComponentModel;
    using Hyperar.HPA.Domain.Database;
    using Hyperar.HPA.UserInterface.State.Interfaces;

    public class TokenStore : ITokenStore, INotifyPropertyChanged
    {
        private Token? currentToken;

        public Token? CurrentToken
        {
            get
            {
                return this.currentToken;
            }

            private set
            {
                this.currentToken = value;

                this.OnPropertyChanged(nameof(this.CurrentToken));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void SetCurrentToken(Token? token)
        {
            this.CurrentToken = token;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
