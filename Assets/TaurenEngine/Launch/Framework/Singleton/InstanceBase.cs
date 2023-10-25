/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/9/20 11:12:41
 *└────────────────────────┘*/

namespace TaurenEngine.Launch
{
	public abstract class InstanceBase
	{
		public virtual int Tag { get; }

		public virtual void Clear()
		{

		}

		public virtual void Destroy()
		{

		}
	}

	/// <summary>
	/// 管理器管理的实例基础类
	/// </summary>
	public abstract class InstanceBase<T> : InstanceBase where T : InstanceBase<T>, new()
	{
		private static T _instance;
		/// <summary>
		/// 由管理器管理的单例
		/// </summary>
		public static T Instance
		{
			get
			{
				if (_instance == null)
					_instance = InstanceManager.Instance.Get<T>();

				return _instance;
			}
		}
	}
}