using KeepInMind.Classes;
using KeepInMind.ViewModels;
using KeepInMind.Views;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KeepInMind.Models
{
	public class MainModel : BaseViewModel
	{		
		private Configurator config = new Configurator();
		private GoogleTranslator translator = new GoogleTranslator();
		private Thread thread;		
		private bool isPreventWord = false;

		private Word editWord;

		public Word EditWordEvent
		{
			get { return editWord; }
			set { editWord = value; }
		}

		//public Word EditWordEvent { get; set; }
		public MainModel()
		{
			//System.Diagnostics.Debug.WriteLine("\nCONSTRUCTOR\n");				
			RunTask();
			GoogleTranslator googleTranslator = new GoogleTranslator();
		}
		public void AddWord(string original, string translate)
		{
			if (EditWordEvent != null)
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
					if (wordViewModel.EditingWord != null)
					{
						EditWordEvent = wordViewModel.EditingWord;
						OnPropertyChanged("EditWordEvent");
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
			WordsListViewModel vm = wnd.DataContext as WordsListViewModel;
			Word selectedWord = vm.SelectedWordEvent;
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
			//TODO показати два рази підряд
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
