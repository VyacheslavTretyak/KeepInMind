using KeepInMind.Classes;
using KeepInMind.ViewModels;
using KeepInMind.Views;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KeepInMind.Models
{
	public class MainModel : BaseViewModel
	{
		private WordsManager wordsManager;
		private Configurator config;
		private GoogleTranslator translator = new GoogleTranslator();
		private Thread thread;		
		private bool isPreventWord = false;
		private MainWindow mainWindow;

		private Word editWord;

		public Word EditWordEvent
		{
			get { return editWord; }
			set { editWord = value; }
		}
		public MainModel(MainWindow mainWindow)
		{
			this.mainWindow = mainWindow;
			wordsManager = WordsManager.Instance;
			config = wordsManager.GetConfig();
			RunTask();
			GoogleTranslator googleTranslator = new GoogleTranslator();
		}
		public void AddWord(string original, string translate)
		{
			var config = WordsManager.Instance.GetConfig();
			original = Regex.Replace(original, Word.spliter, Word.spliterReplace);
			translate = Regex.Replace(translate, Word.spliter, Word.spliterReplace);
			if (EditWordEvent != null)
			{				
				editWord.Origin = original;
				editWord.Translate = translate;
				wordsManager.UpdateWord(editWord);
				editWord = null;
				thread.Resume();
			}
			else
			{
				wordsManager.AddWord(original, translate);
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

		public void GetWord(Word word = null)
		{
			thread = Thread.CurrentThread;
			if (word == null)
			{
				word = wordsManager.GetWord(isPreventWord);
			}
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
						mainWindow.Show();
						thread.Suspend();
					}
					isPreventWord = wordViewModel.PreviousWordEvent;
				});
				try
				{
					GetWord();
				}
				catch(Exception ex)
				{
					Loger.Write(ex.Message);
				}
			}
			try
			{
				wordsManager.Save();
				Thread.Sleep(config.SleepBetweenShows * 1000);
				wordsManager.GetNextWordsList();
				GetWord();
			}
			catch (Exception ex)
			{
				Loger.Write(ex.Message);
			}
		}

		internal void OpenWordsList()
		{
			WordsListWindow wnd = new WordsListWindow();
			WordsListViewModel vm = wnd.DataContext as WordsListViewModel;
			vm.PropertyChanged += SelectedWord_PropertyChanged;			
			wnd.ShowDialog();
		}

		private void SelectedWord_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SelectedWordEvent")
			{			
				RunTask(sender.GetType().GetProperty("SelectedWordEvent").GetValue(sender) as Word);
			}
		}

		internal void OpenSettings()
		{
			//TODO вибір мови
			SettingsWindow wnd = new SettingsWindow();
			wnd.ShowDialog();			
		}

		internal void Close()
		{
			thread?.Abort();
			wordsManager.Close();
		}

		private void RunTask(Word word = null)
		{
			try
			{
				thread?.Abort();
				Task.Run(() => GetWord(word));
			}
			catch(Exception ex)
			{
				Loger.Write(ex.Message);
			}
			
		}

		internal void ShowNow()
		{
			wordsManager.GetNextWordsList();
			RunTask();
		}

		public string ClearTextBox()
		{
			return "";
		}
	}

}
