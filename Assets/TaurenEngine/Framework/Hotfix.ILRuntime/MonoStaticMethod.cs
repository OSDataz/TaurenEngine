/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/6 12:11:19
 *└────────────────────────┘*/

using System;
using System.Reflection;

namespace TaurenEngine.Framework
{
	public class MonoStaticMethod : StaticMethod
	{
		private readonly MethodInfo method;

		public MonoStaticMethod(Type type, string methodName)
		{
			method = type.GetMethod(methodName);
			paramters = new object[method.GetParameters().Length];
		}

		public override void Run(params object[] list)
		{
			base.Run(list);

			method.Invoke(null, paramters);
		}
	}
}