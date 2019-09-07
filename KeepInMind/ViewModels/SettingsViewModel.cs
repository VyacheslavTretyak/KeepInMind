using System;
using System.Globalization;
using System.Windows.Data;
using KeepInMind.Models;
using KeepInMind.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.ViewModels
{
	class SettingsViewModel : BaseViewModel
	{
		private SettingsModel settingsModel = new SettingsModel();

		public DelegateCommand SaveSettingsCommand { get; }

		public WindowRect WindowRectEvent { get; }
		public int HoursEvent { get; set; }	
		public int DaysEvent { get; set; }	
		public int WeeksEvent { get; set; }	
		public AskWordsType AskWordsTypeEvent { get; set; }
		public bool AutoRunEvent { get; set; }
		
		
		public SettingsViewModel()
		{
			SaveSettingsCommand = new DelegateCommand((obj) => SaveSettings(obj), (obj) => true);
			WindowRectEvent = settingsModel.GetRect();
			settingsModel.GetConfig(this);
			OnPropertyChanged("WordWindowRectEvent");			
		}

		private void SaveSettings(object obj)
		{
			settingsModel.SaveSettings(this);
		}		
	}
}
