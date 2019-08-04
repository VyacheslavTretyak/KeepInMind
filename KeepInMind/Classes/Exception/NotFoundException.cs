using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepInMind
{
	class NotFoundException: RepositoryException
	{
		public NotFoundException() { }
		public NotFoundException(string message) : base(message) { }
	}
}
