using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace KeepInMind.Classes
{
	public class TextInputBehavior : Behavior<TextBox>
	{
		protected override void OnAttached()
		{
			AssociatedObject.PreviewTextInput += (sender, args) =>
			{
				args.Handled = Regex.IsMatch(args.Text, "[^0-9.]");
			};			
		}
	}
}