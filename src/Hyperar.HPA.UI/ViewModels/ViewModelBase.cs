namespace Hyperar.HPA.UI.ViewModels
{
    using System.ComponentModel;
    using Hyperar.HPA.UI.ViewModels.Interfaces;

    public class ViewModelBase : IViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void Dispose()
        { }

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}