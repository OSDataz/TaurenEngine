/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 20:49:23
 *└────────────────────────┘*/

using System;
using Tauren.Framework.Runtime;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 角色单个模块
	/// </summary>
	public abstract class ActorModCellBase : LoadObject
	{
		protected ActorX actor;

		/// <summary>
		/// 模块数据
		/// </summary>
		public ActorModuleItem Data { get; private set; }

		public ActorModCellBase(ActorX actor)
		{
			this.actor = actor;
		}

		public void Load(string path, Action<ActorModCellBase> onLoadComplete)
		{
			Load(path, false, result => onLoadComplete.Invoke(this));
		}

		/// <summary>
		/// 是否已经组合合并
		/// </summary>
		public abstract bool IsCombined { get; }
	}
}