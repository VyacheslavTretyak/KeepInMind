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
		private DateTime openTime;
		public WordWindow()
		{
			InitializeComponent();
			SizeToContent = SizeToContent.Height;
			origin.SizeChanged += Origin_SizeChanged;
			openTime = DateTime.Now;
		}

		private void Origin_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			backCard.Height = ActualHeight;
			Top = WindowRect.GetWorkAreaHeight() - ActualHeight;
		}

		private void Flipper_IsFlippedChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
		{

		}

		private void Card_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (DateTime.Now.Subtract(openTime).Milliseconds > 150)
			{
				(this.DataContext as WordViewModel).Close();
				Close();
			}
		}
		private void CheckButton_Click(object sender, RoutedEventArgs e)
		{			
			Close();
		}

		private void BackCard_MouseUp(object sender, MouseButtonEventArgs e)
		{
			CommandBinding command = new CommandBinding(MaterialDesignThemes.Wpf.Flipper.FlipCommand);
			command.Command.Execute(sender);
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			ButtonsGrid.Visibility = Visibility.Collapsed;
			AcceptionGrid.Visibility = Visibility.Visible;
		}
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			ButtonsGrid.Visibility = Visibility.Visible;
			AcceptionGrid.Visibility = Visibility.Collapsed;
		}
	}
}
