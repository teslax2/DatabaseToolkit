using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseToolkit.ViewModel
{
    class CommandHandler : ICommand
    {
        private bool _canExecute;
        private Action<object> _action;
        private Action _actionNoPar;
        public event EventHandler CanExecuteChanged;

        public CommandHandler(Action<object> action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public CommandHandler(Action action, bool canExecute)
        {
            _actionNoPar = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public bool CanExecute()
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
            {
                this.Execute();
                return;
            }
            _action(parameter);
        }

        public void Execute()
        {
            _actionNoPar();
        }
    }
}
