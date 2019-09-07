using KeepInMind.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KeepInMind
{
	/// <summary>
	/// Interaction logic for SettingWindow.xaml
	/// </summary>	
	public partial class SettingsWindow : Window
	{
		public SettingsWindow()
		{
			InitializeComponent();
		} 

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void OnlyNumeric(object sender, TextCompositionEventArgs e)
		{
			var textBox = sender as TextBox;
			e.Handled = Regex.IsMatch(e.Text, "[^0-9.]");
		}
	}
}
