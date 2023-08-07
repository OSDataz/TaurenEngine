/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/8/9 20:48:31
 *└────────────────────────┘*/

using TaurenEngine.Runtime.Unity;

namespace TaurenEngine.Runtime.Framework
{
	/// <summary>
	/// 显示对象基类
	/// 
	/// 无法被Controller直接持有控制
	/// </summary>
	public abstract class ViewBase : MonoComponent
	{
		protected readonly RefrenceList<IRefrenceObject> refList = new RefrenceList<IRefrenceObject>();

		#region 控制器
		/// <summary>
		/// 获取控制器
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		protected static T GetController<T>() where T : ControllerBase, new()
			=> InstanceManager.Instance.Get<T>();
		#endregion

		#region 数据模型
		/// <summary>
		/// 获取数据模型
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		protected static T GetModel<T>() where T : ModelBase, new()
			=> InstanceManager.Instance.Get<T>();
		#endregion
	}
}