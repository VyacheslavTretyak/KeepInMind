using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interactivity;

namespace KeepInMind.Classes
{
	public class CloseWindowBehavior : Behavior<Window>
	{
		//public static readonly DependencyProperty CancelCloseProperty =
		//  DependencyProperty.Register("CancelClose", typeof(bool),
		//	typeof(CancelCloseWindowBehavior), new FrameworkPropertyMetadata(false));

		//public bool CancelClose
		//{
		//	get { return (bool)GetValue(CancelCloseProperty); }
		//	set { SetValue(CancelCloseProperty, value); }
		//}

		protected override void OnAttached()
		{
			AssociatedObject.Closing += (sender, args) =>
			{
				args.Cancel = true;
				(sender as Window).Hide();
			};			
		}
	}
}