using System;
using System.Globalization;
using System.Windows.Data;
using KeepInMind.Models;
using KeepInMind.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.ViewModels
{
	class MainViewModel : BaseViewModel
	{
		private WordsManager wordsManager = WordsManager.Instance;
		private Action Closed;
		public string TextClearEvent { get; } = "";
		public DelegateCommand AddWordCommand { get; }
		public MainViewModel()
		{
			//System.Diagnostics.Debug.WriteLine("CONSTRUCTOR");			
			AddWordCommand = new DelegateCommand((obj) => AddWord(obj), (obj) => true);			
		}	
		private void AddWord(object obj)
		{
			object[] objects = (object[])obj;
			wordsManager.AddWord(objects[0].ToString(), objects[1].ToString());			
			OnPropertyChanged("TextClearEvent");
		}

		private void ShowWordWindow()
		{
			var wordWindow = new WordWindowView();
			wordWindow.Closed += Closed;
			wordWindow.Show(new Word());
		}
	}
}
