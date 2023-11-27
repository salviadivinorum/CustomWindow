using System;
using System.Windows.Input;

namespace CustomTitleBarPOC.Model
{
	public class RelayCommand : ICommand
	{
		private Action<object> execute;
		private Func<bool> canExecute;

		public RelayCommand(Action<object> execute, Func<bool> canExecute)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return this.canExecute == null || this.canExecute();
		}

		public void Execute(object parameter)
		{
			this.execute(parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
	}
}
