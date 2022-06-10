/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/10 19:49:29
 *└────────────────────────┘*/

using System.Reflection;

namespace TaurenEngine.Framework
{
	public class DefaultStaticMethod : StaticMethod
	{
		private readonly MethodInfo _method;

		public DefaultStaticMethod(Assembly assembly, string typeFullName, string methodName)
		{
			_method = assembly.GetType(typeFullName).GetMethod(methodName);
			paramters = new object[_method.GetParameters().Length];
		}

		public override void Run(params object[] paramList)
		{
			base.Run(paramList);

			_method.Invoke(null, paramters);
		}
	}
}