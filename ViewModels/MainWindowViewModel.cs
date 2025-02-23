using ImageManager.MVVM;
using System.Collections.ObjectModel;
using System.Windows;

namespace ImageManager.ViewModels
{
	internal class MainWindowViewModel : ViewModelBase
	{
		private string _debugText;
		public string DebugText
		{
			get { return _debugText; }
			set { if (_debugText != value) _debugText = value; OnPropertyChanged(); }
		}

		private ObservableCollection<IMItemViewModel> _imItems;
		private IMItemViewModel _selectedIMItem;

		public RelayCommand AddItemCommand => new RelayCommand(execute => AddItem());
		public RelayCommand AddItemToItemCommand => new RelayCommand(execute => AddItemToItem(execute as IMItemViewModel));
		public RelayCommand DeleteSelectedItemCommand => new RelayCommand(execute => DeleteSelectedItem(), canExecute => SelectedIMItem != null);
		public RelayCommand DeleteItemCommand => new RelayCommand(execute => DeleteItem(execute as IMItemViewModel, execute as IMItemViewModel), canExecute => SelectedIMItem != null);

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
				Name = "Example", 
				Description = "Folder",
				Depth = 0,
				Indent = new Thickness(0, 0, 0, 0),
				IsParentFolder = true
			});
		}

		private void AddItemToItem(IMItemViewModel parentItem)
		{
			parentItem.ChildCount++;

			IMItemViewModel childItem = new IMItemViewModel
			{
				Name = "Example",
				Description = "Subfolder",
				ParentItem = parentItem,
				Depth = parentItem.Depth + 1,
				Indent = new Thickness(parentItem.Depth * 25, 0, 0, 0)
			};

			_imItems.Insert(_imItems.IndexOf(parentItem)+parentItem.ChildCount, childItem);
		}

		private void DeleteSelectedItem()
		{
			_imItems.Remove(SelectedIMItem);
		}

		private void DeleteItem(IMItemViewModel selectedItem, IMItemViewModel currentItem)
		{
			IMItemViewModel sItem = selectedItem;
			IMItemViewModel cItem = currentItem;

			if (cItem.ChildCount > 0)
			{
				DeleteItem(sItem, _imItems[_imItems.IndexOf(cItem) + cItem.ChildCount]);
			}
			else
			{
				_imItems.Remove(cItem);

				if (cItem.ParentItem != null)
					cItem.ParentItem.ChildCount--;

				if (sItem != cItem)
					DeleteItem(sItem, sItem);
			}
		}
	}
}
