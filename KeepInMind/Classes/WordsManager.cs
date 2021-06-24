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
			bool isFirst = false;
			if (prevent)
			{
				currentNum-=2;
				if (currentNum < 0)
				{
					currentNum = 0;
					isFirst = true;
				}
			}
			if (currentNum >= showList.Count)
			{
				wordsLoader.Save(wordRepository.Words);
				showList.Clear();
				return null;
			}
			Word word = showList[currentNum++];
            if (!isFirst && prevent && word.Rate>0)
            {
				word.Rate--;
				word.CountShow--;
            }
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
			List<Word> list = wordRepository.Words.OrderBy(e => e.Rate).ThenByDescending(e => e.TimeCreate).ToList();
			List<Word> takenWords = new List<Word>();
			DateTime now = DateTime.Now;
			foreach(Word word in list)
            {
				var diff = now - word.TimeShow;
                if (diff.TotalDays >= word.Rate)
                {
					takenWords.Add(word);
                }
                if (takenWords.Count >= configurator.MaxCountWordsInTurn)
                {
					break;
                }
            }

			Random rnd = new Random();
			int n = takenWords.Count;
			while (n > 1)
			{
				n--;
				int k = rnd.Next(n + 1);
				var value = takenWords[k];
				takenWords[k] = takenWords[n];
				takenWords[n] = value;
			}
			return takenWords;
		}
		public void Close()
		{
			wordsLoader.Save(wordRepository.Words);
		}
	}
}
