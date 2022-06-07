/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/6 11:27:05
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.Framework
{
	internal abstract class LoaderBase : IRecycle
	{
		/// <summary>
		/// 【不推荐，脱管】同步加载资源。
		/// <para>默认不缓存，无单独依赖资源，不加载AB包，不下载。</para>
		/// <para>资源自行管理，不需要调用管理器释放。</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public abstract T Load<T>(string path) where T : UnityEngine.Object;

		/// <summary>
		/// 同步加载资源
		/// <para>默认不下载</para>
		/// <para>注意：不管是否缓存，释放资源一定要调用管理器释放<c>Unload</c>。</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id"></param>
		/// <param name="path"></param>
		/// <param name="cacheType"></param>
		/// <returns></returns>
		public abstract T Load<T>(uint id, string path, CacheType cacheType) where T : UnityEngine.Object;

		public abstract void LoadAsync<T>(LoadTaskAsync<T> loadTask) where T : UnityEngine.Object;

		#region 其他模块调用
		/// <summary>
		/// 获取资源配置
		/// </summary>
		/// <param name="path"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		protected bool TryGetAssetConfig(string path, out AssetConfig config)
		{
			config = TaurenFramework.Resource.resourceConfig?.assetList?.Find(item => item.path == path);
			return config != null;
		}

		/// <summary>
		/// 获取AB包配置
		/// </summary>
		/// <param name="name"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		protected bool TryGetABConfig(string name, out ABConfig config)
		{
			config = TaurenFramework.Resource.resourceConfig.abList?.Find(item => item.name == name);
			return config != null;
		}

		private CacheManager _cacheMgr;
		protected CacheManager CacheMgr => _cacheMgr ??= TaurenFramework.Resource.cacheMgr;
		#endregion

		#region 对象池接口
		public virtual void Clear() { }
		public virtual void Destroy() { }
		#endregion
	}
}