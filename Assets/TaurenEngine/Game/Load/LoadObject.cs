/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/9 17:40:26
 *└────────────────────────┘*/

using System;
using TaurenEngine.ModLoad;
using UnityEngine;

namespace TaurenEngine.Game
{
	/// <summary>
	/// 加载对象
	/// </summary>
	public class LoadObject
	{
		/// <summary> 资源路径 </summary>
		public string Path { get; protected set; }

		/// <summary> 加载状态 </summary>
		public LoadStatus Status { get; protected set; }

		public bool IsLoading => Status == LoadStatus.Loading;
		public bool IsLoaded => Status is LoadStatus.Success or LoadStatus.Fail;
		public bool IsLoadSuccess => Status == LoadStatus.Success;

		public void Clear()
		{
			StopLoad();
			ClearCloneObject();
			ClearLoadObject();

			Path = string.Empty;
			Status = LoadStatus.None;
		}

		#region 加载接口
		public void Load(string path, bool force, Action<bool> onLoadComplete)
		{
			if (!force && Path == path)
			{
				onLoadComplete?.Invoke(false);
				return;
			}

			StopLoad();// 停止之前加载

			OnLoadStart();// 开始加载

			// 加载资源

		}

		protected void OnLoadStart()
		{
			if (Status != LoadStatus.None)
				return;

			Status = LoadStatus.Loading;
		}

		protected void OnLoadComplete(string path, GameObject go, Action<bool> onLoadComplete)
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
		public GameObject gameObject { get; protected set; }

		/// <summary>
		/// 克隆对象
		/// </summary>
		/// <returns></returns>
		protected bool CloneObject()
		{
			if (gameObject != null)
				return true;

			if (loadObject == null)
				return false;

			gameObject = GameObject.Instantiate(loadObject);
			gameObject.name = loadObject.name;
			return true;
		}

		protected void ClearCloneObject()
		{
			if (gameObject == null)
				return;

			GameObject.Destroy(gameObject);
			gameObject = null;
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
			gameObject?.SetActive(Active);
		}
		#endregion
	}
}