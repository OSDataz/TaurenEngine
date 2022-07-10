/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.AR
{
	public enum AREngineType
	{
		None,
		ARFoundation,
		EasyAR,
		BaseAR
	}

	internal abstract class AREngineBase
	{
		/// <summary> 引擎是否已启动 </summary>
		public bool isStartup;
		/// <summary> 当前引擎是否可用 </summary>
		public abstract bool IsAvailable { get; }
		/// <summary> 当前依赖的引擎 </summary>
		public abstract AREngineType EngineType { get; }
		public abstract Camera Camera { get; }

		/// <summary>
		/// 手动开启引擎
		/// </summary>
		public abstract void Startup();
		/// <summary>
		/// 手动关闭引擎
		/// </summary>
		public abstract void Close();

		public virtual void Update() { }
		public virtual void OnEnable() { }
		public virtual void OnDisable() { }

		public bool IsDestroyed { get; private set; }
		/// <summary>
		/// 销毁/关闭引擎执行
		/// </summary>
		public virtual void OnDestroy()
		{
			IsDestroyed = true;
		}

		/// <summary>
		/// AR世界方向重定位
		/// </summary>
		public virtual void ResetDirection() { }

		public virtual ISystemPlane SystemPlane => null;
		public virtual ISystemOcclusion SystemOcclusion => null;
		public virtual ISystemImage SystemImage => null;
		public virtual ISystemLight SystemLight => null;

		public abstract bool IsMotionDeviceTracking { get; }
	}
}