using KeepInMind.Classes;
using KeepInMind.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.Models
{
	public class MainModel 
	{
		private WordsManager wordsManager;
		public MainModel()
		{
			System.Diagnostics.Debug.WriteLine("\nCONSTRUCTOR\n");
			wordsManager = WordsManager.Instance;
			GetWord();
		}
		public void AddWord(string original, string translate)
		{
			wordsManager.AddWord(original, translate);
		}

		public void GetWord()
		{
			Word word = wordsManager.GetWord();	
			if(word != null)
			{
				WordWindow wordWindow = new WordWindow();
				WordViewModel wordViewModel = wordWindow.DataContext as WordViewModel;				
				wordViewModel.Word = word;
				wordWindow.ShowDialog();
				GetWord();
			}
		}		
		
		public string ClearTextBox()
		{
			return "";
		}		
	}
	
}
