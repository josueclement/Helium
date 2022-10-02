using System;
using System.Windows.Input;

namespace Helium.Wpf.Commands
{
    /// <summary>
    /// <see cref="ICommand"/> implementation class
    /// </summary>
    public class WpfCommand : ICommand
    {
        #region Constructors

        /// <summary>
        /// Constructor for <see cref="WpfCommand"/>
        /// </summary>
        public WpfCommand()
        { }

        /// <summary>
        /// Constructor for <see cref="WpfCommand"/>
        /// </summary>
        /// <param name="canExecuteFunc">CanExecute method for the command</param>
        /// <param name="executeAction">Execute method for the command</param>
        public WpfCommand(Func<object?, bool> canExecuteFunc, Action<object?> executeAction)
        {
            CanExecuteFunc = canExecuteFunc;
            ExecuteAction = executeAction;
        }

        #endregion

        #region Properties

        /// <summary>
        /// CanExecute method
        /// </summary>
        public Func<object?, bool>? CanExecuteFunc { get; set; }

        /// <summary>
        /// Execute method
        /// </summary>
        public Action<object?>? ExecuteAction { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the CanExecute of the command
        /// </summary>
        public void UpdateCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region ICommand implementation

        /// <inheritdoc/>
        public event EventHandler? CanExecuteChanged;

        /// <inheritdoc/>
        public virtual bool CanExecute(object? parameter)
        {
            if (CanExecuteFunc != null)
                return CanExecuteFunc(parameter);
            return true;
        }

        /// <inheritdoc/>
        public virtual void Execute(object? parameter)
        {
            if (ExecuteAction != null)
                ExecuteAction(parameter);
        }

        #endregion
    }
}
