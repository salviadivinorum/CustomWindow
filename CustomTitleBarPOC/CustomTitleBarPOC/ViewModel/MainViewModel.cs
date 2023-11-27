using CustomTitleBarPOC.Model;
using System.Windows;
using System.Windows.Input;

namespace CustomTitleBarPOC.ViewModel
{
	public class MainViewModel
	{
		public ICommand DragWindowCommand { get; set; }

		public ICommand MinimizeCommand { get; set; }

		public ICommand MaximizeCommand { get; set; }

		public ICommand CloseCommand { get; set; }

		public ICommand InstallUpdateCommand { get; set;}


		public MainViewModel()
		{
			DragWindowCommand = new RelayCommand(DragWindow, CanExecute);
			MinimizeCommand = new RelayCommand(MinimizeWindow, CanExecute);
			MaximizeCommand = new RelayCommand(MaximizeWindow, CanExecute);
			CloseCommand = new RelayCommand(CloseWindow, CanExecute);
			InstallUpdateCommand = new RelayCommand(InstallUpdate, CanExecute);
		}

		private void DragWindow(object parameter)
		{
			if (parameter is MouseButtonEventArgs e && e.ChangedButton == MouseButton.Left)
			{
				Application.Current.MainWindow.DragMove();
			}
		}

		private void MinimizeWindow(object parameter)
		{
			Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}

		private void MaximizeWindow(object parameter)
		{
			if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
			{
				Application.Current.MainWindow.WindowState = WindowState.Normal;
			}
			else
			{
				Application.Current.MainWindow.WindowState = WindowState.Maximized;
			}
		}

		private void CloseWindow(object parameter)
		{
			Application.Current.Shutdown();
		}

		private void InstallUpdate(object parameter)
		{
			// update Prime app
		}

		private bool CanExecute()
		{
			return true;
		}
	}
}
