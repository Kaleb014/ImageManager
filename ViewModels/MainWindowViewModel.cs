using ImageManager.MVVM;
using System.Collections.ObjectModel;
using System.Windows;

namespace ImageManager.ViewModels
{
	internal class MainWindowViewModel : ViewModelBase
	{
		private ObservableCollection<IMItemViewModel> _imItems;
		private IMItemViewModel _selectedIMItem;

		public RelayCommand AddItemCommand => new RelayCommand(execute => AddItem());
		public RelayCommand AddItemToItemCommand => new RelayCommand(execute => AddItemToItem(execute as IMItemViewModel));
		public RelayCommand DeleteSelectedItemCommand => new RelayCommand(execute => DeleteSelectedItem(), canExecute => SelectedIMItem != null);
		public RelayCommand DeleteItemCommand => new RelayCommand(execute => DeleteItem());

		public MainWindowViewModel()
		{
			_imItems = new ObservableCollection<IMItemViewModel>();
		}

		public ObservableCollection<IMItemViewModel> IMItems
		{
			get { return _imItems; }
			set { if(_imItems != value) _imItems = value; OnPropertyChanged(); }
		}

		public IMItemViewModel SelectedIMItem
		{
			get { return _selectedIMItem; }
			set { if(_selectedIMItem != value) _selectedIMItem = value; }
		}

		private void AddItem()
		{
			_imItems.Add(new IMItemViewModel 
			{ 
				Name = $"ListItem{_imItems.Count+1}", 
				Description = "New Description",
				Depth = 0,
				Indent = new Thickness(0, 0, 0, 0)
			});
		}

		private void AddItemToItem(IMItemViewModel item)
		{
			if (item.Items == null)
			{
				item.Items = new ObservableCollection<IMItemViewModel>();
			}

			item.Items.Add(new IMItemViewModel
			{
				Name = $"ChildItem{item.Items.Count + 1}",
				Description = "New Description",
				ParentName = item.Name,
				Depth = item.Depth + 1,
				Indent = new Thickness(item.Depth * 25, 0, 0, 0)
			});

			_imItems.Add(item.Items[item.Items.Count-1]);
		}

		private void DeleteSelectedItem()
		{
			_imItems.Remove(SelectedIMItem);
		}

		private void DeleteItem()
		{
			_imItems.Remove(SelectedIMItem);
		}
	}
}
