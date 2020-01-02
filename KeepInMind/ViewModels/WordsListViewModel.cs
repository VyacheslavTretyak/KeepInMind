using System;
using KeepInMind.Models;
using KeepInMind.Classes;
using System.Collections.Generic;
using KeepInMind.Views;

namespace KeepInMind.ViewModels
{
	class WordsListViewModel : BaseViewModel
	{
		public enum WordType { ORIGIN, TRANSLATE};		
		public WindowRect ListWindowRectEvent { get; }

		
		private WordsListModel listModel = new WordsListModel();
		public List<Word> WordsListEvent { get; set; }
		private object selectedItem;
		public object SelectedItemEvent {
			get { return selectedItem; }
			set {
				selectedItem = value;
				SelectedWordEvent = selectedItem as Word;
			} 
		}
		public Word SelectedWordEvent { get; set; }
		private string originChanged;
		public string OriginTextChangedEvent
		{
			get { return originChanged; }
			set
			{
				originChanged = value;
				ListFilter(originChanged, WordType.ORIGIN);
				OnPropertyChanged("OriginTextChangedEvent");
			}
		}
		private string translateChanged;
		public string TranslateTextChangedEvent {
			get { return translateChanged; }
			set { 
				translateChanged = value;
				ListFilter(translateChanged, WordType.TRANSLATE);
				OnPropertyChanged("TranslateTextChangedEvent");
			}
		}

		public DelegateCommand DoubleClickCommand { get; }

		public WordsListViewModel()
		{
			DoubleClickCommand = new DelegateCommand((obj) => DoubleClick(obj), (obj) => true);
			WordsListEvent = listModel.List;
			OnPropertyChanged("WordsListEvent");
			ListWindowRectEvent = listModel.GetRect();
			OnPropertyChanged("ListWindowRectEvent");			
		}

		public void DoubleClick(object obj)
		{			
			if (SelectedWordEvent != null)
			{				
				(obj as WordsListWindow).DialogResult = true;				
				OnPropertyChanged("SelectedWordEvent");
			}
		}

		private void ListFilter(String text, WordType wordType)
		{
			WordsListEvent = null;
			OnPropertyChanged("WordsListEvent");
			WordsListEvent = listModel.ListFilter(text, wordType);
			OnPropertyChanged("WordsListEvent");
		}	
	}
}
