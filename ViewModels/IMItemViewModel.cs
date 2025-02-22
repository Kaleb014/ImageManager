using ImageManager.MVVM;
using System.Collections.ObjectModel;
using System.Windows;

namespace ImageManager.ViewModels
{
	internal class IMItemViewModel : ViewModelBase
	{
		private string _name;
		private string _description;
		private bool _isExpanded;
		private string _parentName;
		private int _depth;
		private Thickness _indent;
		private string _path;
		private ObservableCollection<IMItemViewModel> _items;

		public string Name
		{
			get { return _name; }
			set { if (_name != value) _name = value; OnPropertyChanged(); }
		}
		public string Description
		{
			get { return _description; }
			set { if (_description != value) _description = value; OnPropertyChanged(); }
		}
		public bool IsExpanded
		{
			get { return _isExpanded; }
			set { if (_isExpanded != value) _isExpanded = value; OnPropertyChanged(); }
		}
		public string ParentName
		{
			get { return _parentName; }
			set { if (_parentName != value) _parentName = value; OnPropertyChanged(); }
		}
		public int Depth
		{
			get { return _depth; }
			set { if (_depth != value) _depth = value; OnPropertyChanged(); }
		}
		public Thickness Indent
		{
			get { return _indent; }
			set 
			{
				_indent = new Thickness(_depth * 25, 0, 0, 0);
				OnPropertyChanged(nameof(_depth));
			}
		}
		public string Path
		{
			get { return _path; }
			set { if (_path != value) _path = value; OnPropertyChanged(); }
		}
		public ObservableCollection<IMItemViewModel> Items
		{
			get { return _items; }
			set { if (_items != value) _items = value; OnPropertyChanged(); }
		}
	}
}
