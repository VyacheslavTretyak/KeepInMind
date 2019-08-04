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
using System.Windows.Shapes;

namespace KeepInMind.Views
{
	/// <summary>
	/// Interaction logic for WordWindow.xaml
	/// </summary>
	public partial class WordView : UserControl
	{
		public WordView()
		{
			InitializeComponent();
		}
		private void Flipper_IsFlippedChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
		{

		}

		private void Card_MouseUp(object sender, MouseButtonEventArgs e)
		{
			//Close();
		}

		private void BackCard_MouseUp(object sender, MouseButtonEventArgs e)
		{
			CommandBinding command = new CommandBinding(MaterialDesignThemes.Wpf.Flipper.FlipCommand);
			command.Command.Execute(sender);
		}
	}
}
