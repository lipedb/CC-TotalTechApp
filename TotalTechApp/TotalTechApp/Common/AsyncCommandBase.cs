using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TotalTechApp.Common
{
    public abstract class AsyncCommandBase : ICommand
    {
        protected bool IsExecuting { get; private set; }

        public abstract Task ExecuteAsync(object parameter);

        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter = null);

        public async void Execute(object parameter)
        {
            try
            {
                if (!IsExecuting)
                {
                    IsExecuting = true;
                    await ExecuteAsync(parameter);
                }
            }
            catch (Exception e)
            {
                // TODO: use log system to log a message when we implement one.
                Console.WriteLine($"AsyncCommand error {e.Message}");
            }
            finally
            {
                IsExecuting = false;
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

