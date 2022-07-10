/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/12/19 16:42:33
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	internal enum LoaderType
	{
		/// <summary> 文件加载 </summary>
		File = 1,
		/// <summary> 本地持久化数据 </summary>
		PlayerPrefs = 2,
		/// <summary> 项目自身资源加载 </summary>
		Resources = 3,
	}
}