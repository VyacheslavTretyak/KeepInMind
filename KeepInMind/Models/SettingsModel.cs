using KeepInMind.Classes;
using KeepInMind.ViewModels;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.Models
{
	class SettingsModel
	{
		private Configurator config;
		public SettingsModel()
		{
			config = WordsManager.Instance.GetConfig();
		}

		internal WindowRect GetRect()
		{
			WindowRect rect = new WindowRect();
			rect.Height = config.SettingsWidowHeight;
			rect.Width = config.SettingsWidowWidth;
			rect.SetRightBottom();
			return rect;
		}

		internal void SaveSettings(SettingsViewModel vm)
		{
			config.AskWords = vm.AskWordsTypeEvent;
			config.Hours = vm.HoursEvent;
			config.Days = vm.DaysEvent;
			config.Weeks = vm.WeeksEvent;
			config.CountOldWords = vm.OldWordsEvent;
			config.MaxCountWordsInTurn = vm.MaxWordsEvent;
			config.SleepBetweenShows = vm.TimeBetweenEvent;
			config.AutoRun = vm.AutoRunEvent;
			ConfigLoader configLoader = new ConfigLoader();
			configLoader.SaveConfig(config);			
		}

		internal void GetConfig(SettingsViewModel vm)
		{
			vm.HoursEvent = config.Hours;
			vm.DaysEvent = config.Days;
			vm.WeeksEvent = config.Weeks;
			vm.OldWordsEvent = config.CountOldWords;
			vm.MaxWordsEvent = config.MaxCountWordsInTurn;
			vm.TimeBetweenEvent = config.SleepBetweenShows;
			vm.AskWordsTypeEvent = config.AskWords;
			vm.AutoRunEvent = config.AutoRun;
		}

		internal void RollBack()
		{
			WordsManager.Instance.Rollback();
		}
	}
}
