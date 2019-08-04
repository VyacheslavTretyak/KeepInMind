using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepInMind
{
	class RepositoryException: SystemException
	{
		public RepositoryException() { }
		public RepositoryException(string message):base(message) { }

	}
}
