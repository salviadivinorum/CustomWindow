using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TreeViewTestApp.Model;

namespace TreeViewTestApp.ViewModel
{
	public class MainViewModel : INotifyPropertyChanged
	{
		public ICommand AddItemCommand { get; set; }
		public ICommand ClearItemsCommand { get; set; }
		public ObservableCollection<TreeItem> MyItems { get; set; }

		private int itemNo;

		private string name;
		private bool isSelected;
		public string Name
		{
			get { return name; }
			set
			{
				if (name != value)
				{
					name = value;
					OnPropertyChanged(nameof(Name));
				}
			}
		}


		public bool IsSelected
		{
			get { return isSelected; }
			set
			{
				if (value != isSelected)
				{
					isSelected = value;
					OnPropertyChanged(nameof(IsSelected));
				}
			}
		}

		public MainViewModel()
		{
			MyItems = new ObservableCollection<TreeItem>();
			AddItemCommand = new RelayCommand(AddNewItem, CanExecute);
			ClearItemsCommand = new RelayCommand(ClearItems, CanExecute);
		}


		private void AddNewItem()
		{
			for (int i = 0; i < 100; i++)
			{
				itemNo++;
				var newItem = new TreeItem()
				{
					Name = "Condition " + itemNo.ToString(),
					Children = new ObservableCollection<TreeItem>()
				};

				MyItems.Add(newItem);
			}
		}

		private void ClearItems()
		{
			itemNo = 0;
			MyItems.Clear();
		}

		private bool CanExecute()
		{
			return true;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
