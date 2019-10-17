using KeepInMind.Classes;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.Models
{
	class WordModel
	{
		private WordsManager wordsManager = WordsManager.Instance;				
		public WordModel()
		{
			
		}

		internal WindowRect GetRect()
		{
			WindowRect rect = new WindowRect();
			rect.Height = wordsManager.GetConfig().WordWidowHeight;
			rect.Width = wordsManager.GetConfig().WordWidowWidth;
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
