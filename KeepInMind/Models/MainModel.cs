using KeepInMind.Classes;
using KeepInMind.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KeepInMind.Models
{
	public class MainModel
	{
		private WordsManager wordsManager;
		private Configurator config = new Configurator();
		public MainModel()
		{
			//System.Diagnostics.Debug.WriteLine("\nCONSTRUCTOR\n");
			wordsManager = WordsManager.Instance;
			Task task = Task.Run(() => GetWord());
			//config.SaveConfig();
		}
		public void AddWord(string original, string translate)
		{
			wordsManager.AddWord(original, translate);
		}

		internal WindowRect GetRect()
		{
			WindowRect rect = new WindowRect();
			rect.Height = config.WidowHeight;
			rect.Width = config.WidowWidth;
			rect.SetRightBottom();
			return rect;
		}

		public void GetWord()
		{
			Thread thread = Thread.CurrentThread;
			Word word = WordsManager.Instance.GetWord();
			if (word != null)
			{
				Application.Current.Dispatcher.Invoke(() =>
				{
					WordWindow wordWindow = new WordWindow();
					WordViewModel wordViewModel = wordWindow.DataContext as WordViewModel;
					wordViewModel.Word = word;
					wordWindow.ShowDialog();
				});
				GetWord();
			}
			Thread.Sleep(config.SleepBetweenShows * 1000);
			GetWord();
		}

		public string ClearTextBox()
		{
			return "";
		}
	}

}
