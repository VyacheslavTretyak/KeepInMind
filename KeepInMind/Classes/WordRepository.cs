using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using KeepInMind.Classes;

namespace KeepInMind.Models
{
	class WordRepository
	{
		private readonly ObservableCollection<Word> words = new ObservableCollection<Word>();
		public ReadOnlyObservableCollection<Word> Words { get; }
		public WordRepository(ObservableCollection<Word> words)
		{
			this.words = words;
			Words = new ReadOnlyObservableCollection<Word>(this.words);
		}
		public int LastId()
		{
			if(words.Count == 0)
			{
				return 1;
			}
			return words.Max(w => w.Id);
		}

		public Word Get(int id)
		{
			return words.FirstOrDefault(w => w.Id == id);
		}

		public void Add(Word word)
		{
			word.Id = LastId() + 1;
			words.Add(word);
		}

		public void Update(Word word)
		{
			var found = words.FirstOrDefault(w => w.Id == word.Id);
			if (found == null)
			{
				throw new NotFoundException();
			}
			found = word;
		}

		public void Delete(Word word)
		{
			var found = words.FirstOrDefault(w => w.Id == word.Id);
			if (found == null)
			{
				throw new NotFoundException();
			}
			words.Remove(found);
		}		
	}
}
