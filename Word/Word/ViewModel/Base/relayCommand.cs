using System;
using System.Windows.Input;

namespace Word
{
    class relayCommand : ICommand
    {
        private Action mAction;

        public relayCommand(Action action)
        {
            mAction = action;
        }

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// Is button clickable always true if entered
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }
        

        public void Execute(object parameter)
        {
            mAction();
        }
    }
}
