using ImageManager.Helpers;
using ImageManager.ViewModels;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ImageManager.Views.Windows
{
	public partial class MainWindow : Window
	{
		private MainWindowViewModel _viewModel;
		private bool _isDragging { get; set; }
		private int counter { get; set; }

		public MainWindow()
		{
			InitializeComponent();
			_viewModel = (MainWindowViewModel)this.DataContext;
		}

		private void ImageManagerWindow_ShowPopup()
		{
			DragRowPopup.IsOpen = true;

			if (DragRowPopup.Width != 150)
				DragRowPopup.Width = 150;
			if (DragRowPopup.Height != 40)
				DragRowPopup.Height = 40;

			Point mousePosition = Mouse.GetPosition(ImageManagerGrid);
			DragRowPopup.HorizontalOffset = mousePosition.X - DragRowPopup.Width * 0.5;
			DragRowPopup.VerticalOffset = mousePosition.Y + DragRowPopup.Height * 2;

			Mouse.AddMouseMoveHandler(ImageManagerGrid, ImageManagerWindow_MouseMove);
		}

		private void ImageManagerWindow_MouseMove(object sender, MouseEventArgs e)
		{
			Mouse.SetCursor(Cursors.Hand); //Research - mouse resets to arrow if I don't set it here. But it flickers now.
			Point mousePosition = e.GetPosition(ImageManagerGrid);
			DragRowPopup.HorizontalOffset = mousePosition.X - DragRowPopup.Width * 0.5;
			DragRowPopup.VerticalOffset = mousePosition.Y + DragRowPopup.Height * 2;
		}

		private void ImageManagerWindow_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (DataContext is not ViewModels.MainWindowViewModel viewModel)
				DataContext = viewModel = new ViewModels.MainWindowViewModel();

			if (DragRowPopup.IsOpen)
			{
				viewModel.MouseLeftButtonUpCommand.Execute(e);
				Mouse.SetCursor(Cursors.Arrow);
				ImageManagerGrid.IsReadOnly = false;
				DragRowPopup.IsOpen = false;
				Mouse.RemoveMouseMoveHandler(ImageManagerGrid, ImageManagerWindow_MouseMove);
			}
		}

		private void ImageManagerGrid_MouseDown(object sender, MouseButtonEventArgs e)
		{

			if (Mouse.LeftButton == MouseButtonState.Pressed && !DragRowPopup.IsOpen)
			{
				DispatcherTimer timer = new DispatcherTimer();

				CustomTimer customTimer = new CustomTimer(1, 0, 0, 0, 0, 500, IsDragging);
			}
		}

		private void IsDragging()
		{
			if (Mouse.LeftButton == MouseButtonState.Pressed)
			{
				_isDragging = true;
				ImageManagerGrid.IsReadOnly = true;
				_viewModel.MouseLeftButtonDownCommand.Execute(_viewModel);
				Mouse.SetCursor(Cursors.Hand);
				ImageManagerWindow_ShowPopup();
			}
		}
	}
}