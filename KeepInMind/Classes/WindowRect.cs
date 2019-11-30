using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepInMind.Classes
{
	public class WindowRect
	{
		public double Height { get; set; }
		public double Width { get; set; }
		public double Left { get; set; }
		public double Top { get; set; }

		public void SetRightBottom()
		{
			Left = GetWorkAreaWidth() - Width;
			Top = GetWorkAreaHeight() - Height;
		}

		public static double GetWorkAreaHeight()
		{
			
			var h = SystemParameters.WorkArea.Height;
			return h;
		}
		public static double GetWorkAreaWidth()
		{
			var w = SystemParameters.WorkArea.Width;
			return w;
		}
	}
}
