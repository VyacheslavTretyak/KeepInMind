using KeepInMind.Classes;
using KeepInMind.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeepInMind
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class WordWindow : Window
	{
		public WordWindow()
		{
			InitializeComponent();		
		}
		private void Flipper_IsFlippedChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
		{

		}

		private void Card_MouseUp(object sender, MouseButtonEventArgs e)
		{			
			(this.DataContext as WordViewModel).Close();
			Close();
		}

		private void BackCard_MouseUp(object sender, MouseButtonEventArgs e)
		{
			CommandBinding command = new CommandBinding(MaterialDesignThemes.Wpf.Flipper.FlipCommand);
			command.Command.Execute(sender);
		}
	}
}
