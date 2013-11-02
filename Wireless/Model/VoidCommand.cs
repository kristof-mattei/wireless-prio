namespace Wireless.Model
{
    using System;
    using System.Windows.Input;

    /// <summary>
    ///     Specifies a command that does something
    /// </summary>
    internal class VoidCommand : ICommand
    {
        private readonly Action _action;

        public VoidCommand(Action action)
        {
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this._action();
        }

        public event EventHandler CanExecuteChanged;
    }
}