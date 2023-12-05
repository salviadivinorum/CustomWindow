using CustomTitleBarPOC.Extensions;
using CustomTitleBarPOC.ViewModel;
using System;
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
            //WindowSizing.ExtendFrameIntoClientArea(this);
            //WindowSizing.HandleLocationChanged(this);
            DataContext = new MainViewModel();

			this.SourceInitialized += Window_SourceInitialized;
		}

        void Window_SourceInitialized(object sender, EventArgs e)
        {
            WindowSizing.WindowInitialized(this);
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
