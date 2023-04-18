/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/11/12 17:38:20
 *└────────────────────────┘*/

using System.Threading;

namespace TaurenEngine.Core
{
	public class Flagment
	{
		/// <summary>
		/// 分配索引
		/// </summary>
		public int index;

		/// <summary>
		/// 分片尺寸
		/// </summary>
		public int sizeTotal;

		/// <summary>
		/// 分片开始位置
		/// </summary>
		public int startRange;

		/// <summary>
		/// 分片结束位置
		/// </summary>
		public int endRange;

		/// <summary>
		/// 是否正在下载
		/// </summary>
		public bool downloading;

		private int _downloadSize;
		/// <summary>
		/// 已下载大小
		/// </summary>
		public int DownloadSize => _downloadSize;

		/// <summary>
		/// 累计已下载的字节数
		/// </summary>
		/// <param name="size"></param>
		public void AddDownloadSize(int size)
		{
			Interlocked.Add(ref _downloadSize, size);
		}
	}
}