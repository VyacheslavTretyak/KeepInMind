using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using KeepInMind.Models;
using KeepInMind.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeepInMind.ViewModels
{
	class MainVM : INotifyPropertyChanged, IMultiValueConverter
	{
		private MainModel model = new MainModel();
		public string TextClearEvent { get; set; }
		public DelegateCommand AddWordCommand { get; }
		public MainVM()
		{
			//System.Diagnostics.Debug.WriteLine("CONSTRUCTOR");			
			AddWordCommand = new DelegateCommand(AddWordExecute, CanAddWordExecute);		
			
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{			
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
		
		private bool CanAddWordExecute(object param)
		{
			return true;
		}
		private void AddWordExecute(object param)
		{
			AddWord((object[])param);
		}
		private void AddWord(object[] objects)
		{
			model.AddWord(objects[0].ToString(), objects[1].ToString());
			TextClearEvent = "";
			OnPropertyChanged("TextClearEvent");
		}

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{		
			return values.Clone();
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
