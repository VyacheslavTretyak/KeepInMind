using KeepInMind.Classes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepInMind.Models
{
	class WordModel: BindableBase
	{
		public int LevelEvent { get; set; } = 1;
		public WordModel()
		{
		}		
	}
}
