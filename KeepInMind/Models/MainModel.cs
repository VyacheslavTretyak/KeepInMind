using KeepInMind.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KeepInMind.Models
{
	public class MainModel : INotifyPropertyChanged
	{
		private WordsManager wordsManager = WordsManager.getInstance();
		public MainModel()
		{
			
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
