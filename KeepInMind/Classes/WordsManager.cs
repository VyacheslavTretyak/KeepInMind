using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace KeepInMind.Classes
{	
	class WordsManager
	{
		private WordRepository wordRepository;
		private WordsLoader wordsLoader;
		private Configurator configurator;
		private List<Word> showList;
		private int currentNum = 0;

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
			try
			{
				configurator = new ConfigLoader().LoadConfig();
				wordsLoader = new WordsLoader(configurator);
				wordRepository = new WordRepository(wordsLoader.LoadLastFile());
				GetNextWordsList();
			}
			catch (Exception ex)
			{
				Loger.Write(ex.Message);
				//MessageBox.Show(ex.Message);				
				Application.Current.Shutdown();				
			}
		}

		
		public Configurator GetConfig()
		{
			return configurator;
		}

		public void Rollback()
		{
			wordRepository = new WordRepository(wordsLoader.RollBack());
		}

		public ReadOnlyObservableCollection<Word> GetWords()
		{
			return wordRepository.Words;
		}

		public void GetNextWordsList()
		{
			currentNum = 0;			
			showList = GetWordsToShow();
			
		}
		public Word FindWord(int id)
		{
			return wordRepository.Get(id);
		}
		public Word GetWord(bool prevent = false) {
			if (prevent)
			{
				currentNum-=2;
				if (currentNum < 0)
				{
					currentNum = 0;
				}
			}
			if (currentNum >= showList.Count)
			{
				wordsLoader.Save(wordRepository.Words);
				showList.Clear();
				return null;
			}
			Word word = showList[currentNum++];
			return word;
		}
		public void Save()
		{
			wordsLoader.Save(wordRepository.Words);
		}
		public void UpdateWord(Word word)
		{
			wordRepository.Update(word);			
		}

		public void DeleteWord(Word word)
		{
			wordRepository.Delete(word);
		}

		public void AddWord(string original, string translate)
		{
			Word newWord = new Word()
			{
				Origin = original,
				Translate = translate
			};
			wordRepository.Add(newWord);
			wordsLoader.Save(wordRepository.Words);
		}
		public int GetCount(Word word, int count)
		{			
			switch (word.Level)
			{
				case Word.WordLevel.Easy:
					return (int)(count*0.3f);
				case Word.WordLevel.Hard:
					return (int)(count * 1.3f);					
			}
			return count;
		}
		private List<Word> GetWordsToShow()
		{
			List<Word> list = wordRepository.Words.OrderBy(e => e.Rate).ThenBy(e => e.TimeCreate).Take(configurator.MaxCountWordsInTurn).ToList();

			//Тасуємо список слів
			Random rnd = new Random();
			List<int> shuffle = new List<int>();
			List<Word> newList = new List<Word>();
			for (int i = 0; i < list.Count; i++)
			{
				int r = 0;
				do
				{
					r = rnd.Next(0, list.Count);
				} while (shuffle.Contains(r));
				shuffle.Add(r);
				newList.Add(list[r]);
			}
			if (newList.Count > configurator.MaxCountWordsInTurn)
			{
				newList.RemoveRange(configurator.MaxCountWordsInTurn, newList.Count - configurator.MaxCountWordsInTurn);
			}
			return newList;
		}
		public void Close()
		{
			wordsLoader.Save(wordRepository.Words);
		}
	}
}
