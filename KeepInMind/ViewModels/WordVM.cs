using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Prism.Mvvm;
using KeepInMind.Models;
using KeepInMind.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MaterialDesignThemes.Wpf;

namespace KeepInMind.ViewModels
{
	class WordVM : IMultiValueConverter
	{
		private WordModel model = new WordModel();
		public int LevelEvent { get; set; }		
		public WordVM() {			
			LevelEvent = 1;
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			var a = LevelEvent;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
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
