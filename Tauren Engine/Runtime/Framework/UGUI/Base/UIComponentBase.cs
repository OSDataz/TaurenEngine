﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/8 17:29:10
 *└────────────────────────┘*/

using Tauren.Core.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tauren.Framework.Runtime
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