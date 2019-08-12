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
		private Configurator config = new Configurator();
		private Thread thread;
		public MainModel()
		{
			//System.Diagnostics.Debug.WriteLine("\nCONSTRUCTOR\n");			
			RunTask();
		}
		public void AddWord(string original, string translate)
		{
			WordsManager.Instance.AddWord(original, translate);
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
			thread = Thread.CurrentThread;
			Word word = WordsManager.Instance.GetWord();
			if (word != null)
			{
				Application.Current.Dispatcher.Invoke(() =>
				{
					WordWindow wordWindow = new WordWindow();
					WordViewModel wordViewModel = wordWindow.DataContext as WordViewModel;
					wordViewModel.WordEvent = word;
					wordWindow.ShowDialog();
				});
				GetWord();
			}
			Thread.Sleep(config.SleepBetweenShows * 1000);
			WordsManager.Instance.GetNextWordsList();
			GetWord();
		}
		private void RunTask()
		{
			thread?.Abort();
			Task.Run(() => GetWord());
		}

		internal void ShowNow()
		{
			WordsManager.Instance.GetNextWordsList();
			RunTask();
		}

		public string ClearTextBox()
		{
			return "";
		}
	}

}
