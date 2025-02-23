using ImageManager.MVVM;
using System.Windows;

namespace ImageManager.ViewModels
{
	internal class IMItemViewModel : ViewModelBase
	{
		private string _name;
		private string _description;
		private bool _isExpanded;
		private IMItemViewModel _parentItem;
		private int _depth;
		private Thickness _indent;
		private string _filePath;
		private int _childCount;
		private bool _isParentFolder;

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
		public IMItemViewModel ParentItem
		{
			get { return _parentItem; }
			set { if (_parentItem != value) _parentItem = value; OnPropertyChanged(); }
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
		public string FilePath
		{
			get { return _filePath; }
			set { if (_filePath != value) _filePath = value; OnPropertyChanged(); }
		}
		public int ChildCount
		{
			get { return _childCount; }
			set { if (_childCount != value) _childCount = value; OnPropertyChanged(); }
		}

		public bool IsParentFolder
		{
			get { return _isParentFolder; }
			set { if (_isParentFolder != value) _isParentFolder = value; OnPropertyChanged(); }
		}
	}
}
