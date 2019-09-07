using KeepInMind.Classes;
using KeepInMind.ViewModels;
using KeepInMind.Views;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KeepInMind.Models
{
	public class MainModel
	{		
		private Configurator config = new Configurator();
		private GoogleTranslator translator = new GoogleTranslator();
		private Thread thread;
		private Word editWord;
		private bool isPreventWord = false;
		private MainViewModel mainViewModel;
		public MainModel(MainViewModel vm)
		{
			//System.Diagnostics.Debug.WriteLine("\nCONSTRUCTOR\n");	
			mainViewModel = vm;
			RunTask();
			GoogleTranslator googleTranslator = new GoogleTranslator();
		}
		public void AddWord(string original, string translate)
		{
			if (editWord != null)
			{
				editWord.Origin = original;
				editWord.Translate = translate;
				WordsManager.Instance.UpdateWord(editWord);
				editWord = null;
				thread.Resume();
			}
			else
			{
				WordsManager.Instance.AddWord(original, translate);
			}
		}

		internal WindowRect GetRect()
		{
			WindowRect rect = new WindowRect();
			rect.Height = config.WidowHeight;
			rect.Width = config.WidowWidth;
			rect.SetRightBottom();
			return rect;
		}

		internal string GetTranslate(string text)
		{
			return translator.Translate(text);
		}

		public void GetWord()
		{
			thread = Thread.CurrentThread;
			Word word = WordsManager.Instance.GetWord(isPreventWord);
			isPreventWord = false;
			if (word != null)
			{
				Application.Current.Dispatcher.Invoke(() =>
				{
					WordWindow wordWindow = new WordWindow();			
					WordViewModel wordViewModel = wordWindow.DataContext as WordViewModel;
					wordViewModel.WordEvent = word;
					wordWindow.ShowDialog();
					if (wordViewModel.EditWordEvent != null)
					{
						editWord = wordViewModel.EditWordEvent;
						mainViewModel.OriginTextEvent = editWord.Origin;
						mainViewModel.TranslateTextEvent = editWord.Translate;
						thread.Suspend();
					}
					isPreventWord = wordViewModel.PreviousWordEvent;
				});
				GetWord();
			}
			Thread.Sleep(config.SleepBetweenShows * 1000);
			WordsManager.Instance.GetNextWordsList();
			GetWord();
		}

		internal void OpenWordsList()
		{
			WordsListWindow wnd = new WordsListWindow();
			wnd.ShowDialog();
		}

		internal void OpenSettings()
		{
			SettingsWindow wnd = new SettingsWindow();
			wnd.ShowDialog();			
		}

		internal void Close()
		{
			WordsManager.Instance.Close();
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
