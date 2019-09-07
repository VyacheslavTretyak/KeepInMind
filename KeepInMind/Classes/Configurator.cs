using KeepInMind.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KeepInMind.Classes
{
	class Configurator
	{
		[CommentEnum]
		public AskWordsType AskWords { get; set; } = AskWordsType.Both;		
		public int Hours { get; set; } = 8;		
		public int Days { get; set; } = 7;
		public int Weeks { get; set; } = 4;
		public bool AutoRun { get; set; } = false;
		public string DirectoryName { get; set; } = "data";
		public string FileName { get; set; } = "words";
		public string FileExtension { get; set; } = "wrd";
		public string FormatInFile { get; set; } = "yyyy_MM_dd_HH_mm_ss";
		public int MaxCountFiles { get; set; } = 20;
		public int CountOldWords { get; set; } = 1;
		public int LevelPercent { get; set; } = 30;
		public int WidowHeight { get; set; } = 300;
		public int WidowWidth { get; set; } = 500;				
		public int WordWidowHeight { get; set; } = 250;//Не впливає на висоту вікна, так як вона встановлюється з розміру тексту 
		public int WordWidowWidth { get; set; } = 350;
		public int SettingsWidowHeight { get; set; } = 400;
		public int SettingsWidowWidth { get; set; } = 300;

		[Comment("Time between shows (in seconds)")]
		public int SleepBetweenShows { get; set; } = 3600;

		private string appName = "KeepInMind";
		private ConfigLoader configLoader;
		public Configurator()
		{
			configLoader = new ConfigLoader(this);
			Load();
		}

		public void Load()
		{			
			SetConfig(configLoader.LoadConfig());
		}
		private void SetConfig(Configurator config)
		{
			foreach (var p in typeof(Configurator).GetProperties())
			{				
				p.SetValue(this, p.GetValue(config));
			}
		}

		public void SaveConfig()
		{
			configLoader.SaveConfig();
		}	

		public void AutoRunSet()
		{
			RegistryKey curUserkey = Registry.CurrentUser;
			RegistryKey autoRunKey = curUserkey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
			var location = System.Reflection.Assembly.GetEntryAssembly().Location;
			autoRunKey.SetValue(appName, location);
			autoRunKey.Close();
			curUserkey.Close();
		}
		public void AutoRunUnset()
		{
			RegistryKey curUserkey = Registry.CurrentUser;
			RegistryKey autoRunKey = curUserkey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
			autoRunKey.DeleteValue(appName);
			autoRunKey.Close();
			curUserkey.Close();
		}
	}
}
