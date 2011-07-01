using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper.Functions {
	public class FunctionException : Exception {
		public FunctionException(string message)
			: base(message) {
		}

		public FunctionException()
			: base() {
		}

		public FunctionException(string message, Exception innerException)
			: base(message, innerException) {
		}
	}
}
