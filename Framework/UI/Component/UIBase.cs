/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/2 16:18:34
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Framework
{
	public abstract class UIBase
	{
		public Transform Root { get; private set; }

		public virtual void Init(Transform root)
		{
			Root = root;
		}
	}
}