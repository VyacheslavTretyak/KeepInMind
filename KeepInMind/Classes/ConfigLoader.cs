using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace KeepInMind.Classes
{
	class ConfigLoader
	{
		private string fileName = "config.ini";
		private Configurator config;
		public ConfigLoader()
		{
			fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
			config = LoadConfig();
		}
		
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
					if(p.Name == "AutoRun")
					{
						if ((bool)p.GetValue(configurator))
						{
							AutoRunSet();
						}
						else
						{
							AutoRunUnset();
						}
					}
				}
			}
		}
		public void AutoRunSet()
		{
			RegistryKey curUserkey = Registry.CurrentUser;
			
			using (RegistryKey autoRunKey = curUserkey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
			{
				autoRunKey.SetValue(config.AppName, Assembly.GetEntryAssembly().Location);				
			}			
			curUserkey.Close();
		}
		public void AutoRunUnset()
		{
			RegistryKey curUserkey = Registry.CurrentUser;			
			using (RegistryKey autoRunKey = curUserkey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
			{
				if (autoRunKey.GetValue(config.AppName) != null)
				{
					autoRunKey.DeleteValue(config.AppName);
				}
			}
			curUserkey.Close();
		}
	}
}
