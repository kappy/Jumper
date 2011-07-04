using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper.Functions {
	public class FunctionParser {

		private string _Command;
		private FunctionCompiler _Compiler;

		public FunctionParser(string command, JumperSettings settings) {
			this._Command = command;
			this._Compiler = new FunctionCompiler(settings);
		}

		//Parse the command from the end to start searching for function
		public string Parse() {
			int currentIndex = _Command.Length;
			do{
				currentIndex = _Command.LastIndexOf('#');
				if(currentIndex < 0)
					break;
				_ParseFunction(currentIndex);
			} while(currentIndex >= 0);

			return _Command;
		}

		//
		private void _ParseFunction(int currentIndex) {
			var startFunctionIndex = currentIndex + 1; // index 0 is the # char
			var endBracersIndex = _Command.IndexOf(')', startFunctionIndex);

			var functionText = _Command.Substring(currentIndex, endBracersIndex - currentIndex + 1);

			var startBracersIndex = functionText.IndexOf('(');
			var functionName = functionText.Substring(1, startBracersIndex - 1).Trim();
			var argsStart = startBracersIndex + 1;
			var argsEnd = functionText.Length - 1;
			var args = functionText.Substring(argsStart, argsEnd - argsStart).Split(',');

			string result = _Compiler.Evaluate(functionName, args);

			_Command = _Command.Replace(functionText, result);
		}
	}
}
 