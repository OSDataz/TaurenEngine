/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/28 11:52:41
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 一次性设置组件
	/// </summary>
	public abstract class OnceComponent : MonoComponent
	{
		/// <summary>
		/// 是否销毁对象
		/// </summary>
		[Tooltip("True 销毁组件对象；False 仅销毁组件")]
		public bool destroyGameObject = false;

		protected virtual void Start()
		{
			if (destroyGameObject) GameObject.Destroy(gameObject);
			else Destroy();
		}
	}
}