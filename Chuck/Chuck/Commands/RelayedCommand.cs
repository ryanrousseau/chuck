using System;
using System.Windows.Input;

namespace Chuck.Commands
{
    /// <summary>
    ///     A command that is relayed from the interface to context.
    /// </summary>
    public class RelayedCommand : ICommand
    {
        private readonly Action<object> _Command;
        private readonly Predicate<object> _CommandStatus;

        /// <summary>
        ///     Can our command be run right now without consequences?
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        ///     Create a new instance of a relayed command
        /// </summary>
        /// <param name="cmd">the command to relay</param>
        /// <param name="cmdStatus">the status of the command e.g., can this execute</param>
        public RelayedCommand(Action<object> cmd, Predicate<object> cmdStatus)
        {
            _Command = cmd;
            _CommandStatus = cmdStatus;
        }

        /// <summary>
        ///     Can we execute the command?
        /// </summary>
        /// <param name="parameters">the command params</param>
        public bool CanExecute(object parameters)
        {
            return _CommandStatus == null || _CommandStatus(parameters);
        }

        /// <summary>
        ///     Execute the command
        /// </summary>
        /// <param name="parameters">the command params</param>
        public void Execute(object parameters)
        {
            _Command(parameters);
        }
    }
}
