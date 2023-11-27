using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TreeViewTestApp.Model
{
	public class RelayCommand : ICommand
	{
		private Action execute;
		private Func<bool> canExecute;

		public RelayCommand(Action execute, Func<bool> canExecute)
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
			this.execute();
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
	}
}
