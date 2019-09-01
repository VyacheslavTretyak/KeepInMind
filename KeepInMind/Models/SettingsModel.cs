using KeepInMind.Classes;
using KeepInMind.ViewModels;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.Models
{
	class SettingsModel
	{				
		private Configurator config = new Configurator();
		public SettingsModel()
		{
			
		}

		internal WindowRect GetRect()
		{
			WindowRect rect = new WindowRect();
			rect.Height = config.SettingsWidowHeight;
			rect.Width = config.SettingsWidowWidth;
			rect.SetRightBottom();
			return rect;
		}

		internal void SaveSettings()
		{
			throw new NotImplementedException();
		}

		internal void GetConfig(SettingsViewModel vm)
		{
			vm.HoursEvent = config.Hours;
			vm.DaysEvent = config.Days;
			vm.WeeksEvent = config.Weeks;
			vm.AskWordsTypeEvent = config.AskWords;
			vm.AutoRunEvent = config.AutoRun;
		}
	}
}
