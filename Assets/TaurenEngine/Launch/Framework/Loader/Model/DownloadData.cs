/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/9/4 20:55:37
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Launch
{
	public enum DownloadCompressType
	{
		/// <summary> 无压缩 </summary>
		None,
	}

	/// <summary>
	/// 下载数据
	/// </summary>
	public class DownloadData
	{
		/// <summary> 下载资源的压缩类型 </summary>
		public DownloadCompressType compressType;

		/// <summary> 开始下载回调 </summary>
		public Action onStart;

		/// <summary> 下载进度回调 </summary>
		public Action<int, int> onProgress;

		public void Clear()
		{
			onStart = null;
			onProgress = null;
		}
	}
}