namespace Hyperar.HPA.UI.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UI.ViewModels.Interfaces;

    public abstract class ViewModelBase : IViewModel, INotifyPropertyChanged
    {
        private bool isInitialized;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsInitialized
        {
            get
            {
                return isInitialized;
            }
            set
            {
                this.isInitialized = value;
                this.OnPropertyChanged(nameof(this.IsInitialized));
            }
        }

        public virtual void Dispose()
        { }

        public virtual async Task InitializeAsync()
        {
            await Task.Delay(1);
            this.IsInitialized = true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}