using System.Windows;
using KeepInMind.ViewModels;

namespace KeepInMind
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainViewModel vm;
		public MainWindow()
		{
			InitializeComponent();
			vm = new MainViewModel();
			vm.SetMainWindow(this);
			this.DataContext = vm;
			Closing += MainWindow_Closing;
		}

		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			vm.Close();
		}
	}
}
