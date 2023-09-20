namespace Hyperar.HPA.UI.State
{
    using System.ComponentModel;
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.UI.State.Interfaces;

    public class TokenStore : ITokenStore, INotifyPropertyChanged
    {
        private Token? currentToken;

        public event PropertyChangedEventHandler? PropertyChanged;

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