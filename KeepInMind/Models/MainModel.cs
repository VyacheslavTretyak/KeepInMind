using KeepInMind.Classes;
using KeepInMind.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.Models
{
	public class MainModel : INotifyPropertyChanged
	{
			
		public Word Word { get; set; }
		public MainModel()
		{
			GetWord();
		}

		public void GetWord()
		{
			Word = wordsManager.GetWord();
			if (Word != null)
			{				
				OnPropertyChanged("Word");
			}
		}		
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
		public void AddWord(string word, string translate)
		{			
			wordsManager.AddWord(word, translate);
		}
	}
	
}
