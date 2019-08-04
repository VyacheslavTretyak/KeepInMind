using KeepInMind.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeepInMind.Models
{
	class WordsManager
	{	
		private WordRepository wordRepository;
		private WordsLoader wordsLoader;
		private Configurator configurator;
		private List<Word> showList;
		public int LevelEvent => 2;
		static private WordsManager instance = null;
		static public WordsManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new WordsManager();
				}
				return instance;
			}
		}
		private WordsManager()
		{		
			wordsLoader = new WordsLoader();
			wordRepository = new WordRepository(wordsLoader.LoadLastFile());
			configurator = new Configurator();
			configurator.Load();
			showList = GetWordsToShow();			
		}

		public Word GetWord() { 
			if(showList.Count == 0)
			{
				return null;
			}
			Word word = showList[0];
			showList.RemoveAt(0);
			return word;
		}

		public void AddWord(string word, string translate)
		{
			Word newWord = new Word()
			{
				Origin = word,
				Translate = translate
			};
			wordRepository.Add(newWord);
			wordsLoader.Save(wordRepository.Words);
		}
		public List<Word> GetWordsToShow()
		{
			List<Word> list = new List<Word>();
			DateTime now = DateTime.Now;

			//Шукаємо слова, які маємо показувати кожну годину
			int to = configurator.Hours;
			int from = 0;
			list.AddRange(wordRepository.Words.Where(a => a.CountShow <= to));

			//Шукаємо слова, які маємо показувати раз в день
			from = to;
			to = configurator.Days + from;
			list.AddRange(wordRepository.Words.Where(w => 
										w.CountShow > from &&
										w.CountShow <= to &&
										(now - w.TimeShow).TotalHours > 24));

			//Шукаємо слова, які маємо показувати раз в тиждень
			from = to;
			to = configurator.Weeks + from;
			list.AddRange(wordRepository.Words.Where(w =>
										w.CountShow > from &&
										w.CountShow <= to &&
										(now - w.TimeShow).TotalDays > 7));

			//Шукаємо слова, які не показували більше місяця тому			
			var oldWords = wordRepository.Words.Where(w =>
										w.CountShow > to &&									
										(now - w.TimeShow).TotalDays > 30)
								.OrderBy(w=>w.CountShow).Take(configurator.CountOldWords*3).ToList();
			//Виподково вибираємо CountOldWords старих слів для показу
			Random rnd = new Random();
			List<int> indexes = new List<int>();
			for (int i = 0; i < configurator.CountOldWords; i++)
			{
				int index = 0; ;
				do
				{
					index = rnd.Next(0, oldWords.Count);
				} while (indexes.Contains(index));
				indexes.Add(index);				
			}
			for(int i = 0; i<oldWords.Count; i++)
			{
				if (indexes.Contains(i))
				{
					list.Add(oldWords[i]);
				}
			}
			//Тасуємо список слів
			List<int> shuffle = new List<int>();
			List<Word> newList = new List<Word>();
			for (int i = 0; i<list.Count; i++)
			{
				int r = 0;
				do
				{
					r = rnd.Next(0, list.Count);
				} while (shuffle.Contains(r));
				shuffle.Add(r);
				newList.Add(list[r]);
			}
			return newList;
		}
	}
}
