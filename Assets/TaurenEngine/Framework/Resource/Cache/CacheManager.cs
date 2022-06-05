/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/6 23:54:48
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 缓存管理器
	/// </summary>
	internal class CacheManager
	{
		private readonly List<Cache> _caches;

		public CacheManager()
		{
			_caches = new List<Cache>();
		}

		/// <summary>
		/// 添加缓存
		/// </summary>
		/// <param name="loadTask"></param>
		public void AddCache(LoadTask loadTask)
		{
			var cache = new Cache();
			cache.SetData(loadTask);
			_caches.Add(cache);

			if (!alwaysAutoRelease && cache.CacheType == CacheType.ReferenceDelay)
				StartAutoRelease();
		}

		/// <summary>
		/// 获取缓存
		/// </summary>
		/// <typeparam name="T">资源类型</typeparam>
		/// <param name="path">资源路径</param>
		/// <param name="id">加载ID</param>
		/// <param name="data">返回资源</param>
		/// <returns></returns>
		public bool TryGetCache<T>(string path, uint id, out T data)
		{
			var cache = _caches.Find(item => item.Path == path);
			if (cache != null)
			{
				cache.holders.Add(id);
				if (cache.Asset.data is T assetData)
				{
					data = assetData;
					return true;
				}
			}

			data = default;
			return false;
		}

		public void Release(uint id)
		{
			var len = _caches.Count;
			for (int i = 0; i < len; ++i)
			{
				if (_caches[i].CheckRelease(id))
				{
					if (_caches[i].IsReleaseImmediate)
						_caches.RemoveAt(i);

					return;
				}
			}
		}

		/// <summary>
		/// 【谨慎使用】强制释放所有缓存资源
		/// </summary>
		public void ReleaseAll()
		{
			foreach (var data in _caches)
			{
				data.Release();
			}

			_caches.Clear();
		}

		#region 自动释放内存资源
		/// <summary> 一直开启自动检测释放资源 </summary>
		public bool alwaysAutoRelease;
		/// <summary> 检测间隔时间 </summary>
		public float autoReleaseInterval;

		private IFrame _timer;

		/// <summary>
		/// 开始自动释放缓存
		/// </summary>
		public void StartAutoRelease()
		{
			_timer ??= TaurenFramework.Frame.GetTimer(autoReleaseInterval, OnCheckRelease);
			_timer.Start();
		}

		/// <summary>
		/// 停止自动释放缓存
		/// </summary>
		public void StopAutoRelease()
		{
			_timer?.Stop();
		}

		private void OnCheckRelease()
		{
			bool needAutoRealease = false;

			var time = Time.time;
			for (int i = _caches.Count - 1; i >= 0; --i)
			{
				var data = _caches[i];
				if (data.CacheType != CacheType.ReferenceDelay)
					continue;

				if (data.holders.Count > 0)
				{
					needAutoRealease = true;
					continue;
				}

				if (time - data.LastUseTime > autoReleaseInterval)
				{
					_caches[i].Release();
					_caches.RemoveAt(i);
				}
			}

			if (!alwaysAutoRelease && !needAutoRealease)
				StopAutoRelease();
		}
		#endregion
	}
}