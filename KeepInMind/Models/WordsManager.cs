﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace KeepInMind.Models
{
	class WordsManager:BindableBase
	{
		WordRepository wordRepository;
		public WordsManager()
		{
			wordRepository = new WordRepository();
		}
		public void AddWord(string word, string translate)
		{
			Word newWord = new Word()
			{
				Origin = word,
				Translate = translate
			};
			wordRepository.Add(newWord);
		}
	}
}
