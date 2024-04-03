/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 20:52:07
 *└────────────────────────┘*/

using Tauren.Core.Runtime;
using UnityEngine;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 角色容器
	/// </summary>
	public class ActorContainer
	{
		#region 容器对象
		public GameObject gameObject { get; private set; }// 整个actor的根节点
		public Transform transform => gameObject?.transform;// 整个actor的根节点
		public ActorBehaviour behaviour { get; private set; }
		public bool IsInit => gameObject != null;

		public void Init()
		{
			if (gameObject != null)
				return;

			gameObject = new GameObject(GetType().Name);
			gameObject.layer = _layer;
			gameObject.SetActive(_active);

			behaviour = gameObject.GetOrAddComponent<ActorBehaviour>();

			GameObject.DontDestroyOnLoad(gameObject);// 不随着场景加载而销毁
		}

		public void Clear()
		{
			if (gameObject == null)
				return;

			GameObject.Destroy(gameObject);
			gameObject = null;

			behaviour = null;
		}
		#endregion

		#region 显示层级
		private int _layer = 0;

		/// <summary>
		/// 显示层级
		/// </summary>
		public int Layer
		{
			get
			{
				if (gameObject != null)
					return gameObject.layer;

				return _layer;
			}
			set
			{
				if (_layer == value)
					return;

				_layer = value;

				gameObject?.SetLayerRecursively(_layer);
			}
		}
		#endregion

		#region 激活显示
		private bool _active = true;

		/// <summary>
		/// 激活显示
		/// </summary>
		public bool Active
		{
			get
			{
				if (gameObject != null)
					return gameObject.activeSelf;

				return _active;
			}
			set
			{
				if (_active == value)
					return;

				_active = value;

				gameObject?.SetActive(_active);
			}
		}
		#endregion
	}
}