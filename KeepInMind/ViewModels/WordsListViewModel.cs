using System;
using System.Globalization;
using System.Windows.Data;
using KeepInMind.Models;
using KeepInMind.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KeepInMind.ViewModels
{
	class WordsListViewModel : BaseViewModel
	{
		private WordsListModel listModel = new WordsListModel();
		public WindowRect ListWindowRectEvent { get; }
		public ReadOnlyObservableCollection<Word> WordsListEvent { get; }


		private string originText;
		public string OriginTextEvent
		{
			get { return originText; }
			set
			{
				originText = value;
				FilterList();
				OnPropertyChanged("OriginTextEvent");
			}
		}

		private string translateText;

		internal void ListFilter()
		{
			throw new NotImplementedException();
		}

		public string TranslateTextEvent
		{
			get { return translateText; }
			set
			{
				translateText = value;
				FilterList();
				OnPropertyChanged("TranslateTextEvent");
			}
		}

		private void FilterList()
		{
			throw new NotImplementedException();
		}

		public WordsListViewModel()
		{				
			WordsListEvent = listModel.GetList();
			OnPropertyChanged("WordsListEvent");
			ListWindowRectEvent = listModel.GetRect();
			OnPropertyChanged("ListWindowRectEvent");
		}
	}
}
