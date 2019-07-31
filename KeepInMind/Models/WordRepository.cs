using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace KeepInMind.Models
{
	class WordRepository
	{
		private readonly ObservableCollection<Word> words = new ObservableCollection<Word>();
		public ReadOnlyObservableCollection<Word> Words { get; }
	}
}
