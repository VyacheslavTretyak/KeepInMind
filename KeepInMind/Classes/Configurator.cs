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
		public string AppName { get; set; } = "KeepInMind";
		[CommentEnum]
		public AskWordsType AskWords { get; set; } = AskWordsType.Both;		
		public int Hours { get; set; } = 8;		
		public int Days { get; set; } = 7;
		public int Weeks { get; set; } = 4;
		public bool AutoRun { get; set; } = false;
		public string FromLanguage { get; set; } = "en";
		public string ToLanguage { get; set; } = "ukr";
		public string DirectoryName { get; set; } = "data";
		public string FileName { get; set; } = "words";
		public string FileExtension { get; set; } = "wrd";
		public string FormatInFile { get; set; } = "yyyy_MM_dd_HH_mm_ss";
		public int MaxCountFiles { get; set; } = 20;
		public int CountOldWords { get; set; } = 1;
		public int MaxCountWordsInTurn { get; set; } = 30;
		public int LevelPercent { get; set; } = 30;
		public int WidowHeight { get; set; } = 300;
		public int WidowWidth { get; set; } = 500;				
		public int WordWidowHeight { get; set; } = 250;//Не впливає на висоту вікна, так як вона встановлюється з розміру тексту 
		public int WordWidowWidth { get; set; } = 350;
		public int ListWidowHeight { get; set; } = 800;
		public int ListWidowWidth { get; set; } = 600;
		public int SettingsWidowHeight { get; set; } = 550;
		public int SettingsWidowWidth { get; set; } = 300;

		[Comment("Time between shows (in seconds)")]
		public int SleepBetweenShows { get; set; } = 3600;		

		public Configurator()
		{
			ListWidowHeight = (int)WindowRect.GetWorkAreaHeight();
		}
	}
}
