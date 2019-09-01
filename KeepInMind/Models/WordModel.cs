using KeepInMind.Classes;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.Models
{
	class WordModel
	{
		private WordsManager wordsManager = WordsManager.Instance;
		//public  Word Word { get; set; }
		private Configurator config = new Configurator();
		public WordModel()
		{
			
		}

		internal WindowRect GetRect()
		{
			WindowRect rect = new WindowRect();
			rect.Height = config.WordWidowHeight;
			rect.Width = config.WordWidowWidth;
			rect.SetRightBottom();
			return rect;
		}		

		internal void DeleteWord(Word word)
		{
			wordsManager.DeleteWord(word);
		}

		internal void CloseWord(Word word)
		{
			word.CountShow++;
			word.TimeShow = DateTime.Now;
			wordsManager.UpdateWord(word);
		}
	}
}
