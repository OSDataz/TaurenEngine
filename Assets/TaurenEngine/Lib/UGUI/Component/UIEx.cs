/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/8 17:29:10
 *└────────────────────────┘*/

using TaurenEngine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TaurenEngine.Lib.UGUI
{
	public abstract class UIEx : MonoComponent
	{
		
	}

	[ExecuteInEditMode]
	public abstract class UIEx<T> : UIEx where T : UIBehaviour
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