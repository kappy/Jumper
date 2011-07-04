using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Jumper.Functions {
	public class FunctionCompiler {
		
		public FunctionCompiler() {
		}

		public string Evaluate(string name, string[] args) {
			Delegate func = null;
			switch (name) {
				case "FullFileName":
					func = _BuildDelegate("System.IO.Path", "GetFullPath");
					break;
				case "GetFileName":
					func = _BuildDelegate("System.IO.Path", "GetFileName");
					break;
			}
			if(func == null)
				throw new FunctionException(string.Format("Function not found: {0}"));

			try {
				object o = func.DynamicInvoke(args);
				return o.ToString();
			} catch (Exception ex){
				throw new FunctionException(string.Format("Error executing the function '{0}': {1}", name, ex.Message, ex));
			}
		}

		private static Delegate _BuildDelegate(string typeName, string methodName) {
			Type type = Type.GetType(typeName);
			var method = type.GetMethod(methodName);
			var methodArgs = method.GetParameters().Select(t => t.ParameterType).ToList();

			Type delegateType;
			if (method.ReturnType == typeof(void)) {
				delegateType = Expression.GetActionType(methodArgs.ToArray());
			} else {
				methodArgs.Add(method.ReturnType);
				delegateType = Expression.GetFuncType(methodArgs.ToArray());
			}

			return Delegate.CreateDelegate(delegateType, method);
		}
	}
}
