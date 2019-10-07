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
using System.Windows.Shapes;

namespace KeepInMind.Views
{
	/// <summary>
	/// Interaction logic for WordsListWindow.xaml
	/// </summary>
	public partial class WordsListWindow : Window
	{
		public WordsListWindow()
		{
			InitializeComponent();
		}

		private void OriginTextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox tb = sender as TextBox;
			WordsListViewModel vm = DataContext as WordsListViewModel;
			vm.ListFilter(tb.Text, WordsListViewModel.WordType.ORIGIN);
		}

		private void TranslateTextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox tb = sender as TextBox;
			WordsListViewModel vm = DataContext as WordsListViewModel;
			vm.ListFilter(tb.Text, WordsListViewModel.WordType.TRANSLATE);
		}
	}
}
