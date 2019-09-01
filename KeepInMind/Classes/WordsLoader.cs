using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeepInMind.Classes;
using Microsoft.Win32;

namespace KeepInMind.Classes
{
	class WordsLoader
	{
		private Configurator configurator;
		public WordsLoader()
		{
			configurator = new Configurator();
			configurator.Load();
		}

		public ObservableCollection<Word> LoadLastFile()
		{
			FileInfo fi = new FileInfo(configurator.DirectoryName);
			if (!fi.Exists)
			{
				Directory.CreateDirectory(fi.FullName);
			}
			DirectoryInfo info = new DirectoryInfo(fi.FullName);
			FileInfo[] files = info.GetFiles();
			if (files.Length == 0)
			{
				return new ObservableCollection<Word>();
			}
			//Looking for newest file
			FileInfo newestFile = files[0];
			int index = newestFile.Name.IndexOf("__");
			string strTime = newestFile.Name.Substring(index + 2, 19);
			DateTime newest = DateTime.ParseExact(strTime, configurator.FormatInFile, CultureInfo.InvariantCulture);
			foreach (FileInfo file in files)
			{
				index = file.Name.IndexOf("__");
				strTime = file.Name.Substring(index + 2, 19);
				DateTime dateTime = DateTime.ParseExact(strTime, configurator.FormatInFile,
					CultureInfo.InvariantCulture);
				if (dateTime > newest)
				{
					newestFile = file;
					newest = dateTime;
				}
			}
			return ReadData(newestFile.FullName);
		}

		private ObservableCollection<Word> ReadData(string fileName)
		{
			//Read file			
			ObservableCollection<Word> words = new ObservableCollection<Word>();
			using (StreamReader sr = new StreamReader(fileName))
			{
				while (!sr.EndOfStream)
				{
					string[] line = sr.ReadLine().Split(Word.spliter.ToCharArray());
					int n = 0;
					Word word = new Word();
					word.Id = int.Parse(line[n++]);				
					word.Origin = line[n++];
					word.Translate = line[n++];
					word.CountShow = int.Parse(line[n++]);
					word.TimeShow = DateTime.ParseExact(line[n++], Word.formatInWord, System.Globalization.CultureInfo.InvariantCulture);
					word.TimeCreate = DateTime.ParseExact(line[n++], Word.formatInWord, System.Globalization.CultureInfo.InvariantCulture);
					word.Level = (Word.WordLevel)Enum.Parse(typeof(Word.WordLevel), line[n++]);
					words.Add(word);
				}
			}
			return words;
		}

		public ObservableCollection<Word> RollBack()
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			DirectoryInfo info = new DirectoryInfo(configurator.DirectoryName);
			fileDialog.InitialDirectory = info.FullName;
			if (fileDialog.ShowDialog() == true)
			{
				return ReadData(fileDialog.FileName);
			}
			return null;
		}

		public void Save(ReadOnlyObservableCollection<Word> words)
		{
			string time = DateTime.Now.ToString(configurator.FormatInFile);
			string fullpath = $"{configurator.DirectoryName}\\{configurator.FileName}__{time}.{configurator.FileExtension}";
			using (StreamWriter streamWriter = new StreamWriter(fullpath))
			{
				foreach (var row in words)
				{
					streamWriter.WriteLine(row.ToLine());
				}
			}
			FileInfo fi = new FileInfo(configurator.DirectoryName);
			DirectoryInfo info = new DirectoryInfo(fi.FullName);
			FileInfo[] files = info.GetFiles();
			while (files.Length > configurator.MaxCountFiles)
			{
				FileInfo latestFile = files[0];
				int index = latestFile.Name.IndexOf("__");
				string strTime = latestFile.Name.Substring(index + 2, 19);
				DateTime latest = DateTime.ParseExact(strTime, configurator.FormatInFile, CultureInfo.InvariantCulture);

				foreach (FileInfo file in files)
				{
					index = file.Name.IndexOf("__");
					strTime = file.Name.Substring(index + 2, 19);
					DateTime dateTime = DateTime.ParseExact(strTime, configurator.FormatInFile, CultureInfo.InvariantCulture);
					if (dateTime < latest)
					{
						latestFile = file;
						latest = dateTime;
					}
				}
				if (File.Exists(latestFile.FullName))
				{
					File.Delete(latestFile.FullName);
				}
				files = info.GetFiles();
			}
		}
	}
}
