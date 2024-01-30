using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CustomTitleBarPOC.Extensions;

namespace CustomTitleBarPOC.View
{
	/// <summary>
	/// Interaction logic for ChyronWindowTitleBar.xaml
	/// </summary>
	public partial class ChyronWindowTitleBar : System.Windows.Controls.UserControl
	{
		private Window window;

		public ChyronWindowTitleBar()
		{
			InitializeComponent();

			DataContext = this;
			windowIcon.MouseDown += WindowIcon_MouseDown;
			MouseLeftButtonDown += OnTitleBarLeftButtonDown;
			MouseDoubleClick += TitleBar_MouseDoubleClick;
			Loaded += ChyronWindowTitleBar_Loaded;
			MouseMove += ChyronWindowTitleBar_MouseMove;
		}


		private void WindowIcon_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				// standard invocation of window system menu
				// do not use it
				//var actualTop = WindowHelper.GetWindowTop(window);
				//var actualLeft = WindowHelper.GetWindowLeft(window);

				//var point = new Point(actualLeft + windowIcon.Width, actualTop + windowIcon.Height);
				//SystemCommands.ShowSystemMenu(window, point);

				ShowContextMenu();
			}
		}

		private ContextMenu? SystemMenu
		{
			get
			{
				return Resources["systemMenu"] as ContextMenu;
			}
		}

		private void ShowContextMenu()
		{
			if (SystemMenu != null)
			{
				SystemMenu.IsOpen = true;
			}
		}

		private void ChyronWindowTitleBar_Loaded(object sender, RoutedEventArgs e)
		{
			window = TemplatedParent as Window;
			if (window != null)
			{
				window.StateChanged += Window_StateChanged;
				RefreshMaximizeRestoreButton(window.WindowState);
			}
		}


		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ChyronWindowTitleBar));

		public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ChyronWindowTitleBar));

		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public ImageSource Icon
		{
			get { return (ImageSource)GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}

		private void TitleBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			MaxButton_Click(sender, e);
		}

		private Point mouseDownPoint;
		bool canMoveMaximizedWindow;

		private void ChyronWindowTitleBar_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (window != null && e.LeftButton == MouseButtonState.Pressed)
			{
				if (canMoveMaximizedWindow)
				{
					var actualTop = WindowHelper.GetWindowTop(window);
					var actualLeft = WindowHelper.GetWindowLeft(window);

					var mousePositionToWindow = mouseDownPoint;
					var actualWidth = ActualWidth;

					var distL = mousePositionToWindow.X;
					var distR = actualWidth - mousePositionToWindow.X;


					window.WindowState = WindowState.Normal;

					var titleBarHeight = ActualHeight;

					var top = (mousePositionToWindow.Y - (titleBarHeight / 2));

					double left;

					var half = ActualWidth / 2;
					if (distL < half)
					{
						left = distL;
					}
					else if (distR < half)
					{
						left = ActualWidth - distR;
					}
					else
					{
						left = half;
					}

					left = (mousePositionToWindow.X - left);

					top = top < 0 ? 0 : top;
					left = left < 0 ? 0 : left;

					window.Top = actualTop + top;
					window.Left = actualLeft + left;
				}

				canMoveMaximizedWindow = false;
				window.DragMove();
			}
		}


		private void OnTitleBarLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (window.WindowState == WindowState.Maximized)
			{
				canMoveMaximizedWindow = true;
				mouseDownPoint = e.MouseDevice.GetPosition(window);
			}
			else
			{
				canMoveMaximizedWindow = false;
			}
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			window?.Close();
		}

		private void MinButton_Click(object sender, RoutedEventArgs e)
		{
			if (window != null)
			{
				window.WindowState = WindowState.Minimized;
			}
		}

		private void MaxButton_Click(object sender, RoutedEventArgs e)
		{
			if (window != null)
			{
				if (window.WindowState == WindowState.Maximized)
				{
					window.WindowState = WindowState.Normal;
				}
				else
				{
					window.WindowState = WindowState.Maximized;
				}

				canMoveMaximizedWindow = false;
			}
		}

		private void RefreshMaximizeRestoreButton(WindowState windowState)
		{
			if (windowState == WindowState.Maximized)
			{
				maximizeButton.Visibility = Visibility.Collapsed;
				restoreButton.Visibility = Visibility.Visible;
			}
			else
			{
				maximizeButton.Visibility = Visibility.Visible;
				restoreButton.Visibility = Visibility.Collapsed;
			}
		}

		private void Window_StateChanged(object sender, EventArgs e)
		{
			var window = (Window)sender;
			RefreshMaximizeRestoreButton(window.WindowState);
		}

		private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
		{
			window.WindowState = WindowState.Minimized;
		}

		public static readonly DependencyProperty MenuContentProperty = DependencyProperty.Register(
		"MenuContent", typeof(object), typeof(ChyronWindowTitleBar), new PropertyMetadata(default(object)));

		public object MenuContent
		{
			get { return GetValue(MenuContentProperty); }
			set { SetValue(MenuContentProperty, value); }
		}
	}
}
