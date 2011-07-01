using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper.Functions {
	public class FunctionCompiler {
		
		public FunctionCompiler() {
		}

		public string Evaluate(string name, string[] args) {
			Func<string> func = null;
			switch (name) {
				case "FullFileName":
					func = FunctionBuilder.Build<string, string>(System.IO.Path.GetFullPath,args);
					break;
				case "GetFileName":
					func = FunctionBuilder.Build<string, string>(System.IO.Path.GetFileName, args);
					break;
			}
			if(func == null)
				throw new FunctionException(string.Format("Function not found: {0}"));

			try {
				return func();
			} catch (Exception ex){
				throw new FunctionException(string.Format("Error executing the function '{0}': {1}", name, ex.Message, ex));
			}
		}
	}
}
