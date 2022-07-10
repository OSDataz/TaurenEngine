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
		private readonly AppDomain _appDomain;
		private readonly IMethod _method;

		public ILRuntimeStaticMethod(AppDomain appDomain, string typeFullName, string methodName, int paramsCount)
		{
			this._appDomain = appDomain;
			_method = appDomain.GetType(typeFullName).GetMethod(methodName, paramsCount);
			paramters = new object[paramsCount];
		}

		public override void Run(params object[] paramList)
		{
			base.Run(paramList);

			_appDomain.Invoke(_method, null, paramters);
		}
	}
}