/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/6 12:11:23
 *└────────────────────────┘*/

using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;

namespace TaurenEngine.Framework
{
	public class ILRuntimeStaticMethod : StaticMethod
	{
		private readonly AppDomain appDomain;
		private readonly IMethod method;

		public ILRuntimeStaticMethod(AppDomain appDomain, string typeName, string methodName, int paramsCount)
		{
			this.appDomain = appDomain;
			method = appDomain.GetType(typeName).GetMethod(methodName, paramsCount);
			paramters = new object[paramsCount];
		}

		public override void Run(params object[] list)
		{
			base.Run(list);

			appDomain.Invoke(method, null, paramters);
		}
	}
}