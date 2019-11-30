using KeepInMind.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static KeepInMind.Classes.Configurator;

namespace KeepInMind.Classes
{
	class ConfigLoader
	{
		private string fileName = "config.ini";
		private Configurator config;
		public ConfigLoader()
		{
			
		}

		private string appName = "KeepInMind";
		private ConfigLoader configLoader;		

		private Configurator SetDefaultConfig()
		{
			return new Configurator();
		}	
		

		public Configurator LoadConfig()
		{			
			if (!File.Exists(fileName))
			{
				config = SetDefaultConfig();
				SaveConfig(config);
			}
			else
			{
				config = new Configurator();
				using (StreamReader sr = new StreamReader(fileName))
				{
					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						if (line.TrimStart()[0] == '#')
						{
							continue;
						}
						string[] keyVal = line.Split(":".ToCharArray());
						SetValueToConfigurator(config, keyVal[0], keyVal[1]);
					}
				}
			}
			return config;
		}

		private void SetValueToConfigurator(Configurator configurator, string key, string val)
		{
			PropertyInfo prop = null;
			foreach (var p in configurator.GetType().GetProperties())
			{
				if(key == p.Name)
				{
					prop = p;
					break;
				}
			}
			if(prop == null)
			{
				return;
			}
			var str = prop.PropertyType.Name;
			switch (str.ToLower())
			{
				case "string":
					prop.SetValue(configurator, val);
					break;
				case "int32":
					prop.SetValue(configurator, int.Parse(val));
					break;
				case "boolean":
					prop.SetValue(configurator, bool.Parse(val));
					break;
				case "askwordstype":
					prop.SetValue(configurator, Enum.Parse(typeof(AskWordsType),val));
					break;
			}
		}	

		public void SaveConfig(Configurator configurator)
		{
			using (StreamWriter sw = File.CreateText(fileName))
			{
				foreach (var p in configurator.GetType().GetProperties())
				{
					foreach (var attr in p.GetCustomAttributes())
					{
						if (attr is Comment)
						{
							sw.WriteLine($"# {(attr as Comment).CommentText}");
						}
						if(attr is CommentEnum)
						{
							sw.Write($"# ");
							foreach (var val in Enum.GetValues(p.PropertyType))
							{
								sw.Write($"{val}, ");
							}
							sw.Write("\n");
						}
					}
					var a = p.GetValue(configurator).ToString();
					sw.WriteLine($"{p.Name}:{a}");
				}
			}
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
