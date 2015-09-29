using System;
using System.Windows.Input;

namespace Chuck.Commands
{
    public class RelayedCommand : ICommand
    {
        private readonly Action<object> _Command;
        private readonly Predicate<object> _CommandStatus;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayedCommand(Action<object> cmd, Predicate<object> cmdStatus)
        {
            _Command = cmd;
            _CommandStatus = cmdStatus;
        }

        public bool CanExecute(object parameters)
        {
            return _CommandStatus == null || _CommandStatus(parameters);
        }

        public void Execute(object parameters)
        {
            _Command(parameters);
        }
    }
}
