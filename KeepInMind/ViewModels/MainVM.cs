using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Prism.Commands;
using Prism.Mvvm;
using KeepInMind.Models;

namespace KeepInMind.ViewModels
{
	class MainVM : BindableBase, IMultiValueConverter
	{
		private WordsManager model = new WordsManager();
		public string TextWordClear => "";
		public string TextTranslateClear => "";
		public DelegateCommand<object[]> AddWordCommand { get; }
		public MainVM() {			
			model.PropertyChanged += (s, e) =>
			  {
				  RaisePropertyChanged(e.PropertyName);
			  };
			AddWordCommand= new DelegateCommand<object[]>(AddWord);

		}

		public void AddWord(object[] objects)
		{
			model.AddWord(objects[0].ToString(), objects[1].ToString());
			RaisePropertyChanged("TextWordClear");
			RaisePropertyChanged("TextTranslateClear");
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
