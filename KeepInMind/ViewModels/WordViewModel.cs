using System;
using System.Globalization;
using System.Windows.Data;
using KeepInMind.Models;
using KeepInMind.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.ViewModels
{
	class WordViewModel : BaseViewModel
	{
		private WordModel wordModel = new WordModel();
		public WindowRect WordWindowRectEvent { get; }
		public DelegateCommand DeleteWordCommand { get; }
		public DelegateCommand EditWordCommand { get; }
		public DelegateCommand PreviousWordCommand { get; }		
		public Word EditingWord { get; set; }
		public bool PreviousWordEvent { get; set; }

		private Word word;
		private Word wordEvent;
		public Word WordEvent
		{
			get { return word; }
			set {
				word = value;
				wordEvent = value;
				wordEvent = wordModel.GetWordShowType(wordEvent);
				levelNum = (int)wordEvent.Level;
				OnPropertyChanged("WordEvent");				
			}
		}
		private int levelNum;
		public int LevelNumEvent
		{
			get { return levelNum; }
			set {
				var word = WordEvent;
				word.Level = (Word.WordLevel)Enum.Parse(typeof(Word.WordLevel), value.ToString());
				WordEvent = word;
			}
		}
		public WordViewModel()
		{
			DeleteWordCommand = new DelegateCommand((obj) => DeleteWord(obj), (obj) => true);
			EditWordCommand = new DelegateCommand((obj) => EditWord(obj), (obj) => true);
			PreviousWordCommand = new DelegateCommand((obj) => PreviousWord(obj), (obj) => true);
			WordWindowRectEvent = wordModel.GetRect();
			OnPropertyChanged("WordWindowRectEvent");
		}

		private void PreviousWord(object obj)
		{
			PreviousWordEvent = true;			
		}

		private void EditWord(Object obj)
		{
			EditingWord = word;
		}
		private void DeleteWord(Object obj)
		{
			wordModel.DeleteWord(word);			
		}

		public void Close()
		{
			wordModel.CloseWord(word);
		}		
	}
}
