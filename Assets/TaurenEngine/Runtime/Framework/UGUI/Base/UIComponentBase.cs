/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/8 17:29:10
 *└────────────────────────┘*/

using TaurenEngine.Runtime.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TaurenEngine.Runtime.Framework
{
	public abstract class UIComponentBase : MonoComponent
	{
		
	}

	[ExecuteInEditMode]
	public abstract class UIComponentBase<T> : UIComponentBase where T : UIBehaviour
	{
		#region 静态扩展
		#endregion

		public T UI { get; protected set; }

		protected virtual void Awake()
		{
			UI ??= GetComponent<T>();
		}
	}
}