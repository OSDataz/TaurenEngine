/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/9/4 20:43:23
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.Launch
{
	/// <summary>
	/// 加载AB包数据
	/// </summary>
	public class LoadAssetBundleData
	{
		/// <summary> 是否是AB包资源 </summary>
		public bool isAssetBundle;

		/// <summary> 依赖该AB包等待加载的资源列表 </summary>
		//public readonly List<LoadData> loadDatas = new List<LoadData>();

		public void Clear()
		{
			isAssetBundle = false;
			//loadDatas.Clear();
		}
	}
}