using ImageManager.MVVM;

namespace ImageManager.ViewModels
{
	internal class TabViewModel : ViewModelBase
	{
		private string _name;
		private bool _isSelected;

		public string Name
		{
			get { return _name; }
			set { if (_name != value) _name = value; OnPropertyChanged(); }
		}
		public bool IsSelected
		{
			get { return _isSelected; }
			set { if (_isSelected != value) _isSelected = value; OnPropertyChanged(); }
		}
	}
}
