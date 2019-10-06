using KeepInMind.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.Models
{
	class WordsListModel
	{
		private WordsManager wordsManager = WordsManager.Instance;		
		private Configurator config = new Configurator();
		public WordsListModel()
		{
			
		}

		internal WindowRect GetRect()
		{
			WindowRect rect = new WindowRect();
			rect.Height = config.ListWidowHeight;
			rect.Width = config.ListWidowWidth;
			rect.SetRightBottom();
			return rect;
		}

		internal ReadOnlyObservableCollection<Word> GetList()
		{
			return wordsManager.GetWords();
		}
	}
}
