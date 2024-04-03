/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 22:05:50
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 角色模块项
	/// </summary>
	public class ActorModuleItem
	{
		/// <summary>
		/// 资源地址
		/// </summary>
		public string path;

		/// <summary>
		/// 模块部位列表
		/// </summary>
		public readonly List<ActorPartItem> Parts = new List<ActorPartItem>();

		/// <summary>
		/// 是否是固定模块
		/// </summary>
		public bool IsFixed { get; private set; }

		/// <summary>
		/// 是否是皮肤模块
		/// </summary>
		public bool IsSkin { get; private set; }

		public void AddParts(List<ActorPartItem> parts)
		{
			Parts.Clear();

			IsFixed = false;
			IsSkin = false;

			foreach (var part in parts)
			{
				AddPart(part);
			}
		}

		public void AddPart(ActorPartItem part)
		{
			Parts.Add(part);

			if (!IsFixed && part.IsFixed)
				IsFixed = true;

			if (!IsSkin && part.IsSkin)
				IsSkin = true;
		}

		public bool CheckMutex(ActorModuleItem data)
		{
			foreach (var part in data.Parts)
			{
				if (Parts.Contains(part))
					return true;
			}

			return false;
		}
	}
}