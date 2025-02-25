using ImageManager.MVVM;
using System.Windows;

namespace ImageManager.ViewModels
{
	internal class IMItemViewModel : ViewModelBase
	{
		private string _name;
		private string _description;
		private bool _isExpanded = true;
		private bool _isVisible = true;
		private IMItemViewModel _parentItem;
		private int _depth;
		private string _filePath;
		private int _childCount;

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
			set { if (_isExpanded != value) _isExpanded = value; OnPropertyChanged(nameof(ExpandContent)); OnPropertyChanged(); }
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
		public string FilePath
		{
			get { return _filePath; }
			set { if (_filePath != value) _filePath = value; OnPropertyChanged(); }
		}
		public int ChildCount
		{
			get { return _childCount; }
			set { if (_childCount != value) _childCount = value; OnPropertyChanged(nameof(HasChildren)); }
		}

		public Thickness Indent
		{
			get { return new Thickness(Depth * 50, 0, 0, 0); }
		}
		public bool IsParentItem
		{
			get { return Depth == 0; }
		}
		public bool HasChildren
		{
			get { return ChildCount > 0; }
		}
		public char ExpandContent
		{
			get => !IsExpanded ? '+' : '-';
		}
		public bool IsVisible
		{
			get
			{
				return _isVisible;
				//if (ParentItem == null)
				//	return true;
				//else
				//{
				//	if (ParentItem.IsExpanded)
				//		return true;
				//	else
				//		return false;
				//}
			}
			set { if (_isVisible != value) _isVisible = value; OnPropertyChanged(); }
		}
	}
}
