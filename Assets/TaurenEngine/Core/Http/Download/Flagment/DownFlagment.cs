/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/11/12 17:38:20
 *└────────────────────────┘*/

using System.Threading;

namespace TaurenEngine.Core
{
	public class DownFlagment
	{
		/// <summary> 分配索引 </summary>
		public int Index { get; private set; }

		/// <summary> 分片尺寸 </summary>
		public int Size { get; private set; }

		/// <summary> 分片开始位置 </summary>
		public int StartRange { get; private set; }

		/// <summary> 分片结束位置 </summary>
		public int EndRange { get; private set; }

		private int _downloadedSize;
		/// <summary> 已下载大小（字节数） </summary>
		public int DownloadedSize
		{
			get => _downloadedSize;
			set => Interlocked.Add(ref _downloadedSize, value);
		}

		/// <summary> 是否加载完成 </summary>
		public bool IsComplete => _downloadedSize >= Size;

		/// <summary> 是否正在下载 </summary>
		public bool downloading;

		public DownFlagment(int index, int size, int startRange, int endRange, int downloadedSize)
		{
			Index = index;
			Size = size;
			StartRange = startRange;
			EndRange = endRange;
			_downloadedSize = downloadedSize;
		}
	}
}