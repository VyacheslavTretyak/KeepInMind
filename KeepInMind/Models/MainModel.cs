using KeepInMind.Classes;
using KeepInMind.ViewModels;
using KeepInMind.Views;
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

		private Word editWord;

		public Word EditWordEvent
		{
			get { return editWord; }
			set { editWord = value; }
		}
		public MainModel()
		{
			wordsManager = WordsManager.Instance;
			config = wordsManager.GetConfig();
			RunTask();
			GoogleTranslator googleTranslator = new GoogleTranslator();
		}
		public void AddWord(string original, string translate)
		{
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
						thread.Suspend();
					}
					isPreventWord = wordViewModel.PreviousWordEvent;
				});
				GetWord();
			}
			wordsManager.Save();
			Thread.Sleep(config.SleepBetweenShows * 1000);
			wordsManager.GetNextWordsList();
			GetWord();
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
			thread?.Abort();
			Task.Run(() => GetWord(word));			
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
