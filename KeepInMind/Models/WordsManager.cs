using System;
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
		private WordRepository wordRepository;
		private WordsLoader wordsLoader;
		private Configurator configurator;
		public WordsManager()
		{
			wordsLoader = new WordsLoader();
			wordRepository = new WordRepository(wordsLoader.LoadLastFile());
			configurator = new Configurator();
			configurator.Load();
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
		public void GetWordsToShow()
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



		}
	}
}
