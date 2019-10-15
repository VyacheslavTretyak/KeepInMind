using KeepInMind.Classes;
using KeepInMind.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Threading;
using static KeepInMind.ViewModels.WordsListViewModel;

namespace KeepInMind.Models
{
	class WordsListModel
	{
		private WordsManager wordsManager = WordsManager.Instance;		
		private Configurator config = new Configurator();
		private String originText = "";
		private String translateText = "";
		public List<Word> List { get; set; }
		public WordsListModel()
		{
			Task<List<Word>> task = new Task<List<Word>>(() => {
				var v = wordsManager.GetWords().ToList<Word>();
				return v;
			});
			task.Start();
			List = task.Result;
		}

		internal WindowRect GetRect()
		{
			WindowRect rect = new WindowRect();
			rect.Height = config.ListWidowHeight;
			rect.Width = config.ListWidowWidth;
			rect.SetRightBottom();
			return rect;
		}
			

		internal List<Word> ListFilter(string text, WordsListViewModel.WordType wordType)
		{
			
			Task<List<Word>> task = new Task<List<Word>>(() =>
			{
				List<Word> list = new List<Word>();
				if (wordType == WordType.ORIGIN)
				{
					originText = text.ToLower();
				}
				else
				{
					translateText = text.ToLower();
				}
				list = List.Where(w => w.Origin.ToLower().Contains(originText)).Where(w => w.Translate.ToLower().Contains(translateText)).ToList();
				return list;
			});
			task.Start();			
			return task.Result;
		}

		internal void EditWorld(int id)
		{
			Word word = wordsManager.FindWord(id);
		}
	}
}
