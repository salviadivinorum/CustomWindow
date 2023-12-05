using CustomTitleBarPOC.Extensions;
using CustomTitleBarPOC.ViewModel;
using System;
using System.Windows;

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
            SourceInitialized += Window_SourceInitialized;
        }

        void Window_SourceInitialized(object sender, EventArgs e)
        {
            WindowSizing.WindowInitialized(this);
        }
    }
}
