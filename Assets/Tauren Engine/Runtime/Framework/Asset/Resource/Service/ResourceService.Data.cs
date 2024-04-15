/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/9 18:07:35
 *└────────────────────────┘*/

using System.Collections.Generic;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public partial class ResourceService
	{
		/// <summary> 所有资源数据（path, ResItem） </summary>
		private readonly Dictionary<string, ResItem> resMap = new Dictionary<string, ResItem>();

		private bool FindResItem(string path, out ResItem resItem)
		{
			if (resMap.TryGetValue(path, out resItem))
				return true;

			Log.Error($"未找到资源数据，Path：{path}");
			return false;
		}
	}
}