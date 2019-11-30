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
		public DelegateCommand ClosingCommand { get; }
		public DelegateCommand TaskbarIconClickCommand { get; }
		
		public MainViewModel()
		{
			//System.Diagnostics.Debug.WriteLine("CONSTRUCTOR");
			mainModel = new MainModel();
			mainModel.PropertyChanged += EditWordEvent_PropertyChanged;

			AddWordCommand = new DelegateCommand((obj) => AddWord(obj), (obj) => true);
			ShowNowCommand = new DelegateCommand((obj) => ShowNow(obj), (obj) => true);
			SettingsCommand = new DelegateCommand((obj) => OpenSettings(obj), (obj) => true);
			WordsListCommand = new DelegateCommand((obj) => OpenWordsList(obj), (obj) => true);
			ClosingCommand = new DelegateCommand((obj) => Shutdown(obj), (obj) => true);
			TaskbarIconClickCommand = new DelegateCommand((obj) => TaskbarIconClick(obj), (obj) => true);
			MainWindowRectEvent = mainModel.GetRect(); 
			OnPropertyChanged("MainWindowRectEvent");
		}		
		private string GetTranslate(string text)
		{
			return mainModel.GetTranslate(text);
		}

		private void EditWordEvent_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(e.PropertyName == "EditWordEvent")
			{
				Word word = sender.GetType().GetProperty("EditWordEvent").GetValue(sender) as Word;
				OriginTextEvent = word.Origin;
				TranslateTextEvent = word.Translate;
			}			
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
			//TODO show now
			mainModel.ShowNow();
		}

		private void AddWord(object obj)
		{
			object[] objects = (object[])obj;
			mainModel.AddWord(objects[0].ToString(), objects[1].ToString());
			OriginTextEvent = "";
			TranslateTextEvent = "";
		}		
		public void Shutdown(object obj)
		{			
			mainModel.Close();
			App.Current.Shutdown();
		}

		public void TaskbarIconClick(object obj)
		{
			(obj as MainWindow).Show();			
		}
	}
}
