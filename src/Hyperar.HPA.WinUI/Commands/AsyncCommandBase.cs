namespace Hyperar.HPA.WinUI.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    internal abstract class AsyncCommandBase : ICommand
    {
        private bool isExecuting;

        public event EventHandler? CanExecuteChanged;

        public bool IsExecuting
        {
            get
            {
                return this.isExecuting;
            }
            set
            {
                this.isExecuting = value;
                this.OnCanExecuteChanged();
            }
        }

        public virtual bool CanExecute(object? parameter)
        {
            return !this.IsExecuting;
        }

        public async void Execute(object? parameter)
        {
            this.IsExecuting = true;

            await this.ExecuteAsync(parameter);

            this.IsExecuting = false;
        }

        public abstract Task ExecuteAsync(object? parameter);

        protected void OnCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}