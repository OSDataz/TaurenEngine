/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/10 14:35:45
 *│
 *│  该文件由工具自动生成，切勿自行修改。
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	public partial class ILRuntimeRegister
	{
		/// <summary>
		/// 热更继承本地基类适配
		/// </summary>
		public void InitAdaptor(ILRuntime.Runtime.Enviorment.AppDomain domain)
		{
			domain.RegisterCrossBindingAdaptor(new UIBaseAdapter());
			domain.RegisterCrossBindingAdaptor(new UIPanelAdapter());
		}
	}
}