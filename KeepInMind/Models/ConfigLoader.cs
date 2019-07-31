using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepInMind.Models
{
	class ConfigLoader
	{
		private string fileName = "config.ini";
		private Dictionary<string, string> config;
		public ConfigLoader()
		{
			config = new Dictionary<string, string>();
		}

		public void LoadConfig()
		{
			config = new Dictionary<string, string>();
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
						config[keyVal[0]] = keyVal[1];
					}
				}
			}
		}

		public Dictionary<string, string> GetConfig()
		{
			return config;
		}

		private void SetDefaultConfig()
		{
			config["hours"] = "8";
			config["days"] = "10";
			config["weeks"] = "6";
			config["ask"] = "2";
			config["autorun"] = "1";
			config["directoryName"] = "data";
			config["fileName"] = "words";
			config["fileExtension"] = "wrd";
			config["formatInFile"] = "yyyy_MM_dd_HH_mm_ss";
			config["maxCountFiles"] = 20.ToString();
		}

		public void SaveConfig()
		{
			using (StreamWriter sw = File.CreateText(fileName))
			{
				foreach (var pair in config)
				{
					if (pair.Key == "ask")
					{
						sw.WriteLine($"# 0 - Word, 1 - Translate, 2 - Both");
					}
					sw.WriteLine($"{pair.Key}:{pair.Value}");
				}
			}
		}
	}
}
