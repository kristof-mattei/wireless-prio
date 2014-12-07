namespace WirelessPrio
{
    using System;
    using System.Windows.Input;

    /// <summary>
    ///     Specifies a command that does something
    /// </summary>
    internal class VoidCommand<T> : ICommand
    {
        private readonly Action<T> _action;

        public VoidCommand(Action<T> action)
        {
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this._action((T) parameter);
        }

        /// <summary>
        ///     Api support for Notify changed
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}