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
		private Word word;
		public Word Word {
			get { return word; }
			set { word = value; OnPropertyChanged("Word"); }
		}		
		public WordViewModel()
		{
			
		}

		public void Close()
		{
			wordModel.CloseWord(word);
		}
	}
}
