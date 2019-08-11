using KeepInMind.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static KeepInMind.Models.Configurator;

namespace KeepInMind.Models
{
	class ConfigLoader
	{
		private string fileName = "config.ini";
		private Configurator config;
		public ConfigLoader()
		{
			config = new Configurator();
		}

		public Configurator LoadConfig()
		{			
			if (!File.Exists(fileName))
			{
				SetDefaultConfig();
				SaveConfig();
			}
			else
			{
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
						SetValueToConfigurator(keyVal[0], keyVal[1]);
					}
				}
			}
			return config;
		}

		private void SetValueToConfigurator(string key, string val)
		{
			PropertyInfo prop = null;
			foreach (var p in typeof(Configurator).GetProperties())
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
					prop.SetValue(config, val);
					break;
				case "int32":
					prop.SetValue(config, int.Parse(val));
					break;
				case "boolean":
					prop.SetValue(config, bool.Parse(val));
					break;
				case "askwordstype":
					prop.SetValue(config, Enum.Parse(typeof(AskWordsType),val));
					break;
			}
				
			
		}

		public Configurator GetConfig()
		{
			return config;
		}

		private void SetDefaultConfig()
		{
			config = new Configurator();
		}

		public void SaveConfig()
		{
			using (StreamWriter sw = File.CreateText(fileName))
			{
				foreach (var p in typeof(Configurator).GetProperties())
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
					sw.WriteLine($"{p.Name}:{p.GetValue(new Configurator()).ToString()}");
				}
			}
		}
	}
}
