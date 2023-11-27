using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TreeViewTestApp.Model;
using TreeViewTestApp.ViewModel;

namespace TreeViewTestApp.View
{
	/// <summary>
	/// Interaction logic for ConditionsView.xaml
	/// </summary>
	public partial class ConditionsView : Window
	{
		public ConditionsView()
		{
			InitializeComponent();
			DataContext = new MainViewModel();
		}


		private void TreeViewItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (Keyboard.Modifiers == ModifierKeys.Control)
			{
				var treeViewItem = sender as TreeViewItem;
				var treeItem = treeViewItem.DataContext as TreeItem;
				treeItem.IsSelected = !treeItem.IsSelected;
			}
			else if (Keyboard.Modifiers == ModifierKeys.Shift)
			{
				// Implement logic for selecting a range of items
			}
			else
			{
				// No modifier key is pressed
				var treeViewItem = sender as TreeViewItem;
				var treeItem = treeViewItem?.DataContext as TreeItem;

				// Clear the selection of other items and select only the clicked item
				var MyItems = (DataContext as MainViewModel)?.MyItems;
				if (MyItems != null)
				{
					foreach (var item in MyItems)
					{
						item.IsSelected = false;
					}
					treeItem.IsSelected = true;
				}
			}
		}
	}
}
