using System;
using System.Globalization;
using System.Windows.Data;
using KeepInMind.Models;
using KeepInMind.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		private MainModel mainModel;
		private string originText;
		private MainWindow mainWindow;
		
		public string OriginTextEvent
		{
			get { return originText; }
			set
			{
				originText = value;
				if (originText != "")
				{
					TranslateTextEvent = GetTranslate(originText);
				}
				OnPropertyChanged("OriginTextEvent");				
			}
		}
		public void SetMainWindow(MainWindow wnd)
		{
			mainWindow = wnd;
		}
		private string GetTranslate(string text)
		{
			return mainModel.GetTranslate(text);
		}

		private string translateText;
		public string TranslateTextEvent
		{
			get { return translateText; }
			set
			{
				translateText = value;				
				OnPropertyChanged("TranslateTextEvent");
			}
		}								
		public WindowRect MainWindowRectEvent { get; }
		public DelegateCommand AddWordCommand { get; }
		public DelegateCommand ShowNowCommand { get; }
		public DelegateCommand SettingsCommand { get; }
		public DelegateCommand WordsListCommand { get; }
		public MainViewModel()
		{
			//System.Diagnostics.Debug.WriteLine("CONSTRUCTOR");
			mainModel = new MainModel(this);
			AddWordCommand = new DelegateCommand((obj) => AddWord(obj), (obj) => true);
			ShowNowCommand = new DelegateCommand((obj) => ShowNow(obj), (obj) => true);
			SettingsCommand = new DelegateCommand((obj) => OpenSettings(obj), (obj) => true);
			WordsListCommand = new DelegateCommand((obj) => OpenWordsList(obj), (obj) => true);
			MainWindowRectEvent = mainModel.GetRect(); 
			OnPropertyChanged("MainWindowRectEvent");
		}

		private void OpenWordsList(object obj)
		{
			mainModel.OpenWordsList();
		}

		private void OpenSettings(object obj)
		{
			mainModel.OpenSettings();
		}

		private void ShowNow(object obj)
		{
			mainModel.ShowNow();
		}

		private void AddWord(object obj)
		{
			object[] objects = (object[])obj;
			mainModel.AddWord(objects[0].ToString(), objects[1].ToString());
			OriginTextEvent = "";
			TranslateTextEvent = "";
		}		
		public void Close()
		{
			mainModel.Close();
		}
	}
}
