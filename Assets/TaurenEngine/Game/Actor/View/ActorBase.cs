/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 20:45:36
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.Game
{
	public class ActorBase
	{
		/// <summary> 角色容器 </summary>
		internal readonly ActorContainer container;
		/// <summary> 角色根对象 </summary>
		internal readonly ActorRoot root;
		/// <summary> 角色动作控制器 </summary>
		internal readonly ActorAnimator animator;

		public ActorBase()
		{
			container = new ActorContainer();
			container.Init();

			root = new ActorRoot();

			animator = new ActorAnimator(this);
		}

		/// <summary>
		/// 清理根和皮肤模块
		/// </summary>
		public virtual void Clear()
		{
			ClearRoot();
		}

		/// <summary>
		/// 销毁最外层对象，根和皮肤模块（包含Clear逻辑）
		/// </summary>
		public virtual void Destroy()
		{
			Clear();

			container.Clear();
		}

		#region 容器
		public Transform transform => container.transform;
		public GameObject gameObject => container.gameObject;

		public bool Active
		{
			get => container.Active;
			set => container.Active = value;
		}

		public int Layer
		{
			get => container.Layer;
			set => container.Layer = value;
		}

		/// <summary>
		/// 实例化子对象使用
		/// </summary>
		/// <param name="gameObject"></param>
		/// <returns></returns>
		internal GameObject Instantiate(GameObject gameObject)
		{
			var go = GameObject.Instantiate(gameObject);
			go.SetLayerRecursively(Layer);
			return go;
		}
		#endregion

		#region 模型根对象
		public void LoadRoot(string path, bool force)
		{
			root.Load(path, force, OnLoadRootComplete);
		}

		protected virtual void ClearRoot()
		{
			animator.Clear();
			root.Clear();
		}

		protected virtual void OnLoadRootComplete(bool result) 
		{
			if (result)
			{
				animator.ParseRootAnimator();// 解析动画控制器
			}
		}
		#endregion
	}
}