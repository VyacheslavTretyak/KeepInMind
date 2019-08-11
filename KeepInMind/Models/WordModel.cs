using KeepInMind.Classes;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.Models
{
	class WordModel
	{
		private WordsManager wordsManager = WordsManager.Instance;
		public  Word Word { get; set; }
		public WordModel()
		{
			
		}

		internal void CloseWord(Word word)
		{
			word.CountShow++;
			word.TimeShow = DateTime.Now;
			wordsManager.UpdateWord(word);
		}
	}
}
