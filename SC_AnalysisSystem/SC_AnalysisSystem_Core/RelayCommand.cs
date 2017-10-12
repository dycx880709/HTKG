using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SC_AnalysisSystem_Core
{
    public class RelayCommand : ICommand
    {
        private Action _action;
        private Predicate<object> _func;

        public RelayCommand(Action action) : this(action, null) { }

        public RelayCommand(Action action, Predicate<object> func)
        {
            this._action = action;
            this._func = func;
        }

        public bool CanExecute(object parameter)
        {
            if (_func != null)
                return _func.Invoke(parameter);
            return true;
        }


        public void Execute(object parameter)
        {
            if (_action != null)
                _action.Invoke();
        }

        public event EventHandler CanExecuteChanged;
        protected void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged.Invoke(this, null);
        }
    }

    public class RelayCommand<T> : ICommand where T : class
    {
        private Action<T> _action;
        private Func<T, bool> _func;

        public RelayCommand(Action<T> action)
            : this(action, null){ }

        public RelayCommand(Action<T> action, Func<T, bool> func)
        {
            this._action = action;
            this._func = func;
        }

        public bool CanExecute(object parameter)
        {
            if (_func != null)
                return _func.Invoke(parameter as T);
            return true;
        }


        public void Execute(object parameter)
        {
            if (_action != null)
                _action.Invoke(parameter as T);
        }

        public event EventHandler CanExecuteChanged;
        protected void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged.Invoke(this, null);
        }
    }
}
