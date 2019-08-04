using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KeepInMind.Classes
{
	class ChildWindowManager:BaseViewModel
	{
		private ChildWindowManager()
		{
			WindowVisibility = Visibility.Collapsed;
			XmlContent = null;
		}

		//Singleton pattern implementation
		private static ChildWindowManager instance;
		public static ChildWindowManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ChildWindowManager();
				}
				return instance;
			}
		}

		private Visibility windowVisibility;
		public Visibility WindowVisibility
		{
			get { return windowVisibility; }
			set
			{
				windowVisibility = value;
				OnPropertyChanged("WindowVisibility");
			}
		}

		private FrameworkElement xmlContent;
		public FrameworkElement XmlContent
		{
			get { return xmlContent; }
			set
			{
				xmlContent = value;
				OnPropertyChanged("XmlContent");
			}
		}
		public void ShowChildWindow(FrameworkElement content)
		{
			XmlContent = content;
			//OnPropertyChanged("XmlContent");
			WindowVisibility = Visibility.Visible;
			//OnPropertyChanged("WindowVisibility");
		}

		public void CloseChildWindow()
		{
			WindowVisibility = Visibility.Collapsed;
			//OnPropertyChanged("WindowVisibility");
			XmlContent = null;
			//OnPropertyChanged("XmlContent");
		}
	}
}
