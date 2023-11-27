using CustomTitleBarPOC.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace CustomTitleBarPOC
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainViewModel();
		}

		/// <summary>
		/// Code behind version of Drag window functionality
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			//if (e.ChangedButton == MouseButton.Left)
			//{
			//	DragMove();
			//}
		}
	}
}
