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
	public class ILRuntimeRegister
	{
		public void Init(ILRuntime.Runtime.Enviorment.AppDomain domain)
		{
			InitAdaptor(domain);
			InitFunctionDelegates(domain);
		}

		/// <summary>
		/// 热更继承本地基类适配
		/// </summary>
		/// <param name="domain"></param>
		public void InitAdaptor(ILRuntime.Runtime.Enviorment.AppDomain domain)
		{
			domain.RegisterCrossBindingAdaptor(new UIBaseAdapter());
			domain.RegisterCrossBindingAdaptor(new UIPanelAdapter());
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