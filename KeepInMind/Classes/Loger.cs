using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepInMind.Classes
{
	class Loger
	{
		private static string fileName = "log";
		private static string directoryName = "log";
		private static string extension = "log";
		private static string path = null;
		private static string GetPath()
		{
			if (path == null)
			{			
				directoryName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryName);
				path = $"{directoryName}/{fileName}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.{extension}";				
				FileInfo fi = new FileInfo(directoryName);
				if (!fi.Exists)
				{
					Directory.CreateDirectory(fi.FullName);
				}				
			}
			return path;
		}
		public static void Write(string str)
		{
			using (StreamWriter streamWriter = new StreamWriter(GetPath(), true)) 
			{
				streamWriter.WriteLine(str);
			}
		}
	}
}
