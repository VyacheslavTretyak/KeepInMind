using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepInMind.Classes
{
	internal class WindowRect
	{
		public double Height { get; set; }
		public double Width { get; set; }
		public double Left { get; set; }
		public double Top { get; set; }

		public void SetRightBottom()
		{
			Left = GetWordAreaWidth() - Width;
			Top = GetWordAreaHeight() - Height;
		}

		public static double GetWordAreaHeight()
		{
			return SystemParameters.WorkArea.Height;
		}
		public static double GetWordAreaWidth()
		{
			return SystemParameters.WorkArea.Width;
		}
	}
}
