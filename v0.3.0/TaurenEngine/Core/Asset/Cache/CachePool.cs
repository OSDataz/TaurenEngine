/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/11/29 14:23:10
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Core
{
	internal class CachePool : AssetBase
	{
		private readonly List<CacheData> _caches = new List<CacheData>();

		public void AddCache(LoadTask loadTask)
		{
			if (!TryGetCache(loadTask.path, out var cache))
			{
				cache = new CacheData();
				_caches.Add(cache);
			}

			cache.SetData(loadTask);
		}

		public bool TryGetCache(LoadPath pathData, out CacheData cache)
		{
			foreach (var data in _caches)
			{
				if (data.LoadPath.Equals(pathData))
				{
					cache = data;
					return true;
				}
			}

			cache = null;
			return false;
		}

		public void Release(int id)
		{
			foreach (var data in _caches)
			{
				if (data.Release(id))
					return;
			}
		}

		/// <summary>
		/// 【谨慎使用】强制释放所有缓存资源
		/// </summary>
		public void ReleaseAll()
		{
			foreach (var data in _caches)
			{
				Release(data);
			}

			_caches.Clear();
		}

		private void Release(CacheData cache)
		{
			core.LoadRes.ReleaseLoadRes(cache.LoadPath, cache.LoadRes);
			cache.Release();
		}

		#region 自动释放内存资源
		private IFrameUpdate _timer;
		/// <summary> 检测间隔时间 </summary>
		public float checkIntervalTime = 30000f;

		/// <summary>
		/// 开始自动释放缓存
		/// </summary>
		public void StartAutoRelease()
		{
			_timer ??= FrameManager.Instance.GetTimer(checkIntervalTime, OnCheckRelease);
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
			var time = Time.time;
			for (int i = _caches.Count - 1; i >= 0; --i)
			{
				var data = _caches[i];
				if (data.IsPermanent || data.holders.Count > 0)
					continue;

				if (time - data.LastUseTime > checkIntervalTime)
				{
					Release(_caches[i]);
					_caches.RemoveAt(i);
				}
			}
		}
		#endregion
	}
}