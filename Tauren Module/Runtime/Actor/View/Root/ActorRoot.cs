/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 20:47:56
 *└────────────────────────┘*/

using System;
using Tauren.Framework.Runtime;
using UnityEngine;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 模型根对象
	/// </summary>
	public class ActorRoot : ResObject
	{
		protected ActorBase actor;

		public void Load(ActorBase actor, string path, bool force, Action<bool> onLoadComplete)
		{
			this.actor = actor;

			Load(null, path, force, onLoadComplete);
		}

		protected override void InitCloneObject()
		{
			base.InitCloneObject();

			var container = actor.container;
			if (!container.IsInit)
				return;

			var rootTr = GameObject.transform;
			var scale = rootTr.localScale;

			rootTr.SetParent(container.transform);
			rootTr.localPosition = Vector3.zero;
			rootTr.localRotation = Quaternion.identity;
			rootTr.localScale = scale;// 保留预制体上的scale
		}

		internal string ToLog() => $"RootPath:{Path} LoadStatus:{Status}";
	}
}