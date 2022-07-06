/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/5/6 23:37:42
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 资源组件
	/// </summary>
	public class ResourceComponent : OnceComponent
	{
		/// <summary>
		/// 同时异步加载数量上限
		/// </summary>
		public int multLoadMaximum = 5;

		/// <summary>
		/// 一直开启自动释放资源
		/// </summary>
		public bool alwaysAutoRelease = true;
		/// <summary>
		/// 自动释放资源间隔时间
		/// </summary>
		public float autoReleaseInterval = 30000f;

		protected override void Awake()
		{
			base.Awake();

			if (TaurenFramework.Resource == null)
			{
				var resource = new ResourceManager();
				resource.asyncLoadMgr.multLoadMaximum = multLoadMaximum;
				resource.cacheMgr.alwaysAutoRelease = alwaysAutoRelease;
				resource.cacheMgr.autoReleaseInterval = autoReleaseInterval;
				TaurenFramework.Resource = resource;
			}
		}

		protected override void Start()
		{
			if (alwaysAutoRelease)
				TaurenFramework.Resource.cacheMgr.StartAutoRelease();

			base.Start();
		}
	}
}