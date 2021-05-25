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

		internal void CloseWord(Word word, bool isSkip)
		{
			if (isSkip)
			{
				word.Rate = 0;
            }
            else
            {
				word.Rate++;
            }
			word.CountShow++;			
			word.TimeShow = DateTime.Now;
			wordsManager.UpdateWord(word);			
		}

		internal Word GetWordShowType(Word word)
		{
			if(wordsManager.GetConfig().AskWords == AskWordsType.Translate)
			{
				word = WrapWord(word);
			}
			else if(wordsManager.GetConfig().AskWords == AskWordsType.Both)
			{
				Random rnd = new Random();
				if(rnd.Next(0, 2) == 0)
				{
					word = WrapWord(word);
				}				
			}
			return word;
		}
		private Word WrapWord(Word word)
		{
			string tmp = word.Translate;
			word.Translate = word.Origin;
			word.Origin = tmp;
			return word;
		}
	}
}
