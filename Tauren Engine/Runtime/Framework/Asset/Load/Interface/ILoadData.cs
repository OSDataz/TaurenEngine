﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 20:13:14
 *└────────────────────────┘*/

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 加载数据
	/// </summary>
	public interface ILoadData
	{
		/// <summary>
		/// 资源对象
		/// </summary>
		IAsset Asset { get; }

		/// <summary>
		/// 加载结果（0：标识加载成功；其他值自定义加载状态）
		/// </summary>
		int Code { get; }
	}
}