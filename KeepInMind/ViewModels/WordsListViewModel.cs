using System;
using System.Globalization;
using System.Windows.Data;
using KeepInMind.Models;
using KeepInMind.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace KeepInMind.ViewModels
{
	class WordsListViewModel : BaseViewModel
	{
		public enum WordType { ORIGIN, TRANSLATE};		
		public WindowRect ListWindowRectEvent { get; }

		private WordsListModel listModel = new WordsListModel();		

		public List<Word> WordsListEvent { get; set; }
			
		public WordsListViewModel()
		{				
			WordsListEvent = listModel.List;
			OnPropertyChanged("WordsListEvent");
			ListWindowRectEvent = listModel.GetRect();
			OnPropertyChanged("ListWindowRectEvent");
		}

		public void ListFilter(String text, WordType wordType)
		{
			WordsListEvent = null;
			OnPropertyChanged("WordsListEvent");
			WordsListEvent = listModel.ListFilter(text, wordType);
			OnPropertyChanged("WordsListEvent");
		}
	}
}
