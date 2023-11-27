using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TreeViewTestApp.Model
{
	public class TreeItem : INotifyPropertyChanged
	{
		private bool isSelected;
		private string name;
		private ObservableCollection<TreeItem> children;

		public TreeItem()
		{
			Children = new ObservableCollection<TreeItem>();
		}
		public bool IsSelected
		{
			get
			{
				return isSelected;
			}
			set
			{
				if (isSelected != value)
				{
					isSelected = value;
					OnPropertyChanged(nameof(IsSelected));
				}
			}
		}


		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				if (name != value)
				{
					name = value;
					OnPropertyChanged(nameof(Name));
				}
			}
		}

		public ObservableCollection<TreeItem> Children
		{
			get
			{
				return children;
			}
			set
			{
				if (children != value)
				{
					children = value;
					OnPropertyChanged(nameof(Children));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
