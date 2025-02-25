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
		public RelayCommand DeleteItemCommand => new RelayCommand(execute => DeleteItem(execute as IMItemViewModel, execute as IMItemViewModel), canExecute => SelectedIMItem != null);
		public RelayCommand ExpandCollapseCommand => new RelayCommand(execute => ExpandCollapse(execute as IMItemViewModel), canExecute => SelectedIMItem != null);

		public MainWindowViewModel()
		{
			_imItems = new ObservableCollection<IMItemViewModel>();
		}

		public ObservableCollection<IMItemViewModel> IMItems
		{
			get { return _imItems; }
			set { if (_imItems != value) _imItems = value; OnPropertyChanged(); }
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
				Depth = 0
			});
		}

		private async void AddItemToItem(IMItemViewModel parentItem)
		{
			parentItem.ChildCount++;

			IMItemViewModel childItem = new IMItemViewModel
			{
				Name = "Example",
				Description = "Subfolder",
				ParentItem = parentItem,
				Depth = parentItem.Depth + 1
			};

			_imItems.Insert(_imItems.IndexOf(parentItem) + await GetChildCountTotal(parentItem, parentItem), childItem);
		}

		private async Task<int> GetChildCountTotal(IMItemViewModel selectedItem, IMItemViewModel currentItem)
		{
			int count = 0;
			IMItemViewModel sItem = selectedItem;
			IMItemViewModel cItem = currentItem;

			if (cItem.ChildCount > 0)
			{
				for (int i = cItem.ChildCount; i > 0; i--)
				{
					if (_imItems[_imItems.IndexOf(cItem) + i].ChildCount > 0)
					count += await GetChildCountTotal(sItem, _imItems[_imItems.IndexOf(cItem) + i]);

					count++;
				}
			}

			return count;
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

		private async void ExpandCollapse(IMItemViewModel selectedItem)
		{
			int count = await GetChildCountTotal(selectedItem, selectedItem);
			bool isExpanded = selectedItem.IsExpanded;

			switch(isExpanded)
			{
				case true: //collapse all children and self
					selectedItem.IsExpanded = false;

					for (int i = count; i > 0; i--)
					{
						IMItemViewModel currentItem = _imItems[_imItems.IndexOf(selectedItem) + i];

						currentItem.IsVisible = false;
						currentItem.IsExpanded = false;
					}
					break;
				case false: //expand self
					selectedItem.IsExpanded = true;

					for (int i = count; i > 0; i--)
					{
						IMItemViewModel currentItem = _imItems[_imItems.IndexOf(selectedItem) + i];

						if(currentItem.ParentItem == selectedItem)
							currentItem.IsVisible = true;
					}
					break;
			}
		}
	}
}
