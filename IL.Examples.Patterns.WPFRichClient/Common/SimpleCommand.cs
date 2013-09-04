using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IL.Examples.Patterns.WPFRichClient.Common
{
    public class SimpleCommand : ICommand
    {
        private Action<object> _action;

        public SimpleCommand(Action<object> action)
        {
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this._action(parameter);
        }
    }
}
