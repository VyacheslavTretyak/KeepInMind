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
		public DelegateCommand LevelUpCommand { get; }
		public DelegateCommand LevelDownCommand { get; }
		public WindowRect WordWindowRectEvent { get; }
		private Word word;
		public Word WordEvent
		{
			get { return word; }
			set {
				word = value;								
				OnPropertyChanged("WordEvent");				
			}
		}		
		public WordViewModel()
		{
			LevelUpCommand = new DelegateCommand((obj) => LevelUp(obj), (obj) => true);
			LevelDownCommand = new DelegateCommand((obj) => LevelDown(obj), (obj) => true);
			WordWindowRectEvent = wordModel.GetRect();
			OnPropertyChanged("WordWindowRectEvent");
		}

		public void Close()
		{
			wordModel.CloseWord(word);
		}
		private void LevelDown(object obj)
		{
			wordModel.SetLevel(word, Word.WordChangeLevel.Down);
			OnPropertyChanged("WordEvent");
		}
		private void LevelUp(object obj)
		{
			wordModel.SetLevel(word, Word.WordChangeLevel.Up);
			OnPropertyChanged("WordEvent");
		}
	}
}
