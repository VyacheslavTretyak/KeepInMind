using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepInMind.Classes
{
	internal class Comment: System.Attribute
	{
		public string CommentText { get;  set; }
		public Comment() { }
		public Comment(string comment)
		{
			CommentText = comment;
		}
	}
}
