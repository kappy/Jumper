using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace Jumper.Functions {
	public sealed class FunctionBuilder {

		private FunctionBuilder(){
		}

		public static Func<string> Build<TResult>(Func<TResult> func, string[] args) {
			return () => { 
				return func().ToString(); 
			};
		}

		public static Func<string> Build<T,TResult>(Func<T, TResult> func, string[] args){
			return () => {
				T a1 = (T)Convert.ChangeType(args[0], typeof(T), CultureInfo.InvariantCulture);
				return func(a1).ToString();
			};
		}

		public static Func<string> Build<T1, T2, TResult>(Func<T1, T2, TResult> func, string[] args) {
			return () => {
				T1 a1 = (T1)Convert.ChangeType(args[0], typeof(T1), CultureInfo.InvariantCulture);
				T2 a2 = (T2)Convert.ChangeType(args[1], typeof(T2), CultureInfo.InvariantCulture);
				return func(a1, a2).ToString();
			};
		}
	}
}
