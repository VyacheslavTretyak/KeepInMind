using KeepInMind.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace KeepInMind.Classes
{
	public class PreviewKeyDownBehavior : Behavior<DataGrid>
	{
		protected override void OnAttached()
		{
			AssociatedObject.PreviewKeyDown += (sender, args) =>
			{
				if(args.Key == System.Windows.Input.Key.Enter)
				{
					Window wnd = (((DataGrid)sender).FindName("ThisWindow") as Window);
					var viewModel = wnd.DataContext as WordsListViewModel;
					viewModel.DoubleClick(wnd);
				}				
			};			
		}
	}
}