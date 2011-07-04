using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Jumper.Functions {
	public class FunctionCompiler {

		private JumperSettings _Settings;

		public FunctionCompiler(JumperSettings settings) {
			this._Settings = settings;
		}

		public string Evaluate(string name, string[] args) {
			Delegate func = null;
			
			if (!_Settings.HasFunction(name))
				throw new FunctionException(string.Format("Function not found: {0}", name));

			try {
				Function function = _Settings.GetFunctionByName(name);
				func = _BuildDelegate(function.Class, function.Name);
			} catch (Exception ex) {
				throw new FunctionException(string.Format("Error building the function '{0}': {1}", name, ex.Message, ex));
			}

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
