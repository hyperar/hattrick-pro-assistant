namespace Hyperar.HPA.UserInterface.ViewModels
{
    using System.ComponentModel;
    using Hyperar.HPA.UserInterface.ViewModels.Interfaces;

    public class ViewModelBase : IViewModel
    {
        public virtual void Dispose() { }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
