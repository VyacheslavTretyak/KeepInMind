using KeepInMind.Views;
using System;

namespace KeepInMind.Classes
{
	internal class WordWindowView:BaseViewModel
	{
		public event Action Closed;
		public WordWindowView()
		{

		}

		public void Show(Word word)
		{
			AddUserViewModel vm = new AddUserViewModel(personId);
			vm.Closed += ChildWindow_Closed;
			ChildWindowManager.Instance.ShowChildWindow(new WordView() { DataContext = vm });
		}

		void ChildWindow_Closed()
		{
			if (Closed != null)
				Closed();
			ChildWindowManager.Instance.CloseChildWindow();
		}
	}	
}