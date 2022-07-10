/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/10/6 18:19:15
 *└────────────────────────┘*/

using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;

namespace TaurenEngine.ILRuntime
{
	public sealed class ILRuntimeStaticMethod : IStaticMethod
	{
		private readonly AppDomain _appDomain;
		private readonly IMethod _method;
		private readonly object[] _paramters;

		public ILRuntimeStaticMethod(AppDomain appdomain, string typeName, string methodName, int paramsCount)
		{
			_appDomain = appdomain;
			_method = appdomain.GetType(typeName).GetMethod(methodName, paramsCount);
			_paramters = new object[paramsCount];
		}

		public void Run()
		{
			_appDomain.Invoke(_method, null, _paramters);
		}
	}
}