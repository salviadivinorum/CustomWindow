using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomTitleBarPOC.View
{
    /// <summary>
    /// Interaction logic for ChyronWindowTitleBar.xaml
    /// </summary>
    public partial class ChyronWindowTitleBar : UserControl
    {
        private Window window;

        public ChyronWindowTitleBar()
        {
            InitializeComponent();

            DataContext = this;

            MouseLeftButtonDown += OnTitleBarLeftButtonDown;
            MouseDoubleClick += TitleBar_MouseDoubleClick;
            Loaded += ChyronWindowTitleBar_Loaded;
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

        private void OnTitleBarLeftButtonDown(object sender, MouseEventArgs e)
        {
            if (window != null)
            {
                window.DragMove();
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
