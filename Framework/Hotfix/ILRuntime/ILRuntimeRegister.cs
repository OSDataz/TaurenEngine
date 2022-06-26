/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/6 18:13:43
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 跨域适配注册
	/// </summary>
	public partial class ILRuntimeRegister
	{
		public void InitAdaptation(ILRuntime.Runtime.Enviorment.AppDomain domain)
		{
			InitAdaptor(domain);
			InitFunctionDelegates(domain);
		}

		/// <summary>
		/// 热更传委托到本地适配
		/// </summary>
		/// <param name="appdomain"></param>
		public void InitFunctionDelegates(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
		{
			appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
			{
				return new UnityEngine.Events.UnityAction(() =>
				{
					((Action)act)();
				});
			});
		}
	}
}