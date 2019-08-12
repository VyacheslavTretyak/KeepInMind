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
		private MainModel mainModel = new MainModel();
		public string TextClearEvent => mainModel.ClearTextBox();
		public WindowRect MainWindowRectEvent { get; }
		public DelegateCommand AddWordCommand { get; }
		public DelegateCommand ShowNowCommand { get; }
		public MainViewModel()
		{
			//System.Diagnostics.Debug.WriteLine("CONSTRUCTOR");			
			AddWordCommand = new DelegateCommand((obj) => AddWord(obj), (obj) => true);
			ShowNowCommand = new DelegateCommand((obj) => ShowNow(obj), (obj) => true);
			MainWindowRectEvent = mainModel.GetRect(); 
			OnPropertyChanged("MainWindowRectEvent");
		}

		private void ShowNow(object obj)
		{
			mainModel.ShowNow();
		}

		private void AddWord(object obj)
		{
			object[] objects = (object[])obj;
			mainModel.AddWord(objects[0].ToString(), objects[1].ToString());			
			OnPropertyChanged("TextClearEvent");
		}		
	}
}
