/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/10/6 18:09:57
 *└────────────────────────┘*/

using System;
using System.Reflection;

namespace TaurenEngine.ILRuntime
{
	public sealed class MonoStaticMethod : IStaticMethod
	{
		private readonly MethodInfo _method;
		private readonly object[] _paramters;

		public MonoStaticMethod(Type type, string methodName)
		{
			_method = type.GetMethod(methodName);
			_paramters = new object[_method.GetParameters().Length];
		}

		public void Run()
		{
			_method.Invoke(null, _paramters);
		}
	}
}