namespace Hyperar.HPA.UserInterface.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public abstract class AsyncCommandBase : ICommand
    {
        private bool isExecuting;
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

        public event EventHandler? CanExecuteChanged;

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
