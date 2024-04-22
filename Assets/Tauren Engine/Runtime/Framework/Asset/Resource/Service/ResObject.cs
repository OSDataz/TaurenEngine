/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/9 17:40:26
 *└────────────────────────┘*/

using System;
using Tauren.Core.Runtime;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 加载资源对象
	/// </summary>
	public class ResObject
	{
		/// <summary> 资源路径 </summary>
		public string Path { get; protected set; }

		/// <summary> 加载状态 </summary>
		public LoadStatus Status { get; protected set; }

		public bool IsLoading => Status == LoadStatus.Loading;
		public bool IsLoaded => Status is LoadStatus.Success or LoadStatus.Fail;
		public bool IsLoadSuccess => Status == LoadStatus.Success;

		public virtual void Clear()
		{
			StopLoad();
			ClearCloneObject();
			ClearLoadObject();

			Path = string.Empty;
			Status = LoadStatus.None;
		}

		#region 加载接口
		private ILoadHandler _loadHandler;

		public void Load(IRefrenceContainer container, string path, bool force, Action<bool> onLoadComplete)
		{
			if (!force && Path == path)
			{
				onLoadComplete?.Invoke(false);
				return;
			}

			StopLoad();// 停止之前加载

			LoadStart();// 开始加载

			// 加载资源
			_loadHandler = IResourceService.Instance.LoadAsync<GameObject>(container, path, LoadType.Asset, true, 10, 
				(ret, go) => 
				{
					LoadComplete(path, go, onLoadComplete);
				});
		}

		protected void LoadStart()
		{
			if (Status != LoadStatus.None)
				return;

			Status = LoadStatus.Loading;
		}

		protected void LoadComplete(string path, GameObject go, Action<bool> onLoadComplete)
		{
			if (!IsLoading)
			{
				onLoadComplete?.Invoke(false);
				return;
			}

			if (Path != path)
			{
				onLoadComplete?.Invoke(false);
				return;
			}

			_loadHandler = null;

			if (go != null)
			{
				Status = LoadStatus.Success;

				loadObject = go;

				ClearCloneObject();// 清理其他克隆对象
				CloneObject();// 克隆对象
				
				InitCloneObject();// 初始化克隆对象
			}
			else
			{
				Status = LoadStatus.Fail;
			}

			onLoadComplete?.Invoke(true);
		}

		protected virtual void InitCloneObject() 
		{
			CheckActive();// 检测显示
		}

		protected void StopLoad()
		{
			if (!IsLoading)
				return;

			if (_loadHandler != null)
			{
				_loadHandler.Unload();
				_loadHandler = null;
			}
		}
		#endregion

		#region 加载对象
		protected GameObject loadObject;

		protected void ClearLoadObject()
		{
			if (loadObject == null)
				return;

			loadObject = null;
		}
		#endregion

		#region 克隆对象
		public GameObject GameObject { get; protected set; }

		/// <summary>
		/// 克隆对象
		/// </summary>
		/// <returns></returns>
		protected bool CloneObject()
		{
			if (GameObject != null)
				return true;

			if (loadObject == null)
				return false;

			GameObject = GameObject.Instantiate(loadObject);
			GameObject.name = loadObject.name;
			return true;
		}

		protected void ClearCloneObject()
		{
			if (GameObject == null)
				return;

			GameObject.Destroy(GameObject);
			GameObject = null;
		}
		#endregion

		#region 激活显示
		protected bool _active = true;

		/// <summary>
		/// 设置模型显示
		/// </summary>
		public bool Active
		{
			get => _active;
			set 
			{
				if (_active == value)
					return;

				_active = value;

				CheckActive();
			}
		}

		/// <summary>
		/// 检测显示
		/// </summary>
		protected void CheckActive()
		{
			GameObject?.SetActive(Active);
		}
		#endregion
	}
}