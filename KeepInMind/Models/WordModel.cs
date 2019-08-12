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

		internal void CloseWord(Word word)
		{
			word.CountShow++;
			word.TimeShow = DateTime.Now;
			wordsManager.UpdateWord(word);
		}

		internal void SetLevel(Word word, Word.WordChangeLevel changeLevel)
		{
			if(changeLevel == Word.WordChangeLevel.Down)
			{
				word.LevelDown();
			}
			else
			{
				word.LevelUp();
			}
			wordsManager.UpdateWord(word);
		}
	}
}
