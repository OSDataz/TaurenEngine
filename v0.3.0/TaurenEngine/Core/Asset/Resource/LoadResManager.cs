/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/21 13:47:39
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 资源管理器
	/// </summary>
	internal class LoadResManager
	{
		private readonly Dictionary<Type, TypePool> _resPoolMap = new Dictionary<Type, TypePool>();
		private readonly ObjectPool<ObjectRes> _defaultResPool = new ObjectPool<ObjectRes>();

		private LoaderManager _loaderManager;

		public LoadResManager(LoaderManager loaderManager)
		{
			_loaderManager = loaderManager;
		}

		public void InitRegisterResource()
		{
			RegisterResource<GameObject, PrefabRes>();
			RegisterResource<string, TextRes>();
			RegisterResource<AudioClip, AudioClipRes>();
			RegisterResource<Texture2D, Texture2DRes>();
			RegisterResource<Material, MaterialRes>();
			RegisterResource<Scene, SceneRes>();
		}

		/// <summary>
		/// 注册资源类型
		/// </summary>
		/// <typeparam name="TRes">资源类型</typeparam>
		/// <typeparam name="TLoadRes">加载资源类型</typeparam>
		public void RegisterResource<TRes, TLoadRes>() where TLoadRes : LoadRes<TRes>, new()
		{
			_resPoolMap.Set(typeof(TRes), new TypePool(typeof(TLoadRes)));
		}

		/// <summary>
		/// 获取加载资源
		/// </summary>
		/// <typeparam name="TRes"></typeparam>
		/// <returns></returns>
		public LoadRes GetLoadRes<TRes>()
		{
			return _resPoolMap.TryGetValue(typeof(TRes), out var resPool) ?
				resPool.Get() as LoadRes : _defaultResPool.Get();
		}

		/// <summary>
		/// 完整的释放加载资源
		/// </summary>
		/// <param name="loadPath"></param>
		/// <param name="loadRes"></param>
		/// <returns></returns>
		public bool ReleaseLoadRes(LoadPath loadPath, LoadRes loadRes)
		{
			if (loadRes != null)
			{
				var loader = _loaderManager.GetLoader(loadPath.loaderType);
				if (loader == null)
					return false;

				// 释放加载器中的内存
				loader.ReleaseRes(loadRes);
				// 释放资源自身内存
				loadRes.Release();

				// 回收到对象池
				if (_resPoolMap.TryGetValue(loadRes.ResType, out var resPool))
					resPool.Add(loadRes);
				else
					resPool.Add(loadRes);
			}

			loadPath?.Recycle();
			return true;
		}
	}
}