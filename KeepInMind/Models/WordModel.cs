using KeepInMind.Views;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.Models
{
	class WordModel: INotifyPropertyChanged
	{
		WordsManager wordsManager = WordsManager.Instance();
		Word Word { get; set; }
		public WordModel()
		{
			GetWord();
		}

		public void GetWord()
		{
			Word = wordsManager.GetWord();
			if (Word != null)
			{
				WordView wordWindow = new WordView();
				OnPropertyChanged("Word");
				wordWindow.ShowDialog();
				
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

	
	}
}
