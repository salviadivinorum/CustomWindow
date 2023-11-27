using CustomTitleBarPOC.Model;
using System.Windows;
using System.Windows.Input;

namespace CustomTitleBarPOC.ViewModel
{
	public class MainViewModel
	{
		public ICommand DragWindowCommand { get; set; }
		public MainViewModel()
		{
			DragWindowCommand = new RelayCommand(DragWindow, CanExecute);
		}

		private void DragWindow(object parameter)
		{
			if (parameter is MouseButtonEventArgs e && e.ChangedButton == MouseButton.Left)
			{
				Application.Current.MainWindow.DragMove();
			}
		}

		private bool CanExecute()
		{
			return true;
		}
	}
}
