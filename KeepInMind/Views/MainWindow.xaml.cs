using System.Dynamic;
using System.Windows;
using System.Windows.Input;
using KeepInMind.ViewModels;

namespace KeepInMind
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{		
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainViewModel(this);
			TextBoxWord.KeyDown += TextBoxWord_KeyDown;
			TextBoxTranslate.KeyDown += TextBoxTranslate_KeyDown; ;
		}

		private void TextBoxTranslate_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Enter)
			{				
				var vm = (MainViewModel)DataContext;
				object[] obj = new object[2];
				obj[0] = TextBoxWord.Text;
				obj[1] = TextBoxTranslate.Text;
				
				if (vm.AddWordCommand.CanExecute(obj))
				{
					vm.AddWordCommand.Execute(obj);
				}
				FocusManager.SetFocusedElement(this, TextBoxWord);
			}
		}

		private void TextBoxWord_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if(e.Key == System.Windows.Input.Key.Enter)
			{
				FocusManager.SetFocusedElement(this, TextBoxTranslate);				
			}
		}
	}
}
