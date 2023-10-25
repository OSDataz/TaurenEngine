/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/11/12 17:42:38
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TaurenEngine.Launch
{
	public class DownFlagmentManager
	{
		/// <summary> 分片列队 </summary>
		private List<DownFlagment> _flagments = new List<DownFlagment>();

		/// <summary> 分片数量 </summary>
		public int FlagmentCount => _flagments.Count;

		/// <summary>
		/// 是否已经退出
		/// </summary>
		public bool IsComplete { get; private set; }

		#region 分片
		public void InitDownFlagment(int totalSize)
		{
			// 计算合适的分片尺寸(分片太多会造成连接总次数多，分片太少会造成下载后期多线程作用减低，更优方案是采用动态分片)
			int flagmentSize;
			if (totalSize > 100 * 1024 * 1024) flagmentSize = 8 * 1024 * 1024;
			else if (totalSize > 40 * 1024 * 1024) flagmentSize = 4 * 1024 * 1024;
			else if (totalSize > 10 * 1024 * 1024) flagmentSize = 2 * 1024 * 1024;
			else if (totalSize > 4 * 1024 * 1024) flagmentSize = 1 * 1024 * 1024;
			else flagmentSize = Math.Min(512 * 1024, totalSize);

			var count = (totalSize + flagmentSize - 1) / flagmentSize;// 总分片数量
			var sIdx = 0;// 当前分片开始位置

			// 进行切割分片
			for (int index = 0; index < count; ++index)
			{
				var size = sIdx + flagmentSize > totalSize ? totalSize - sIdx : flagmentSize;

				_flagments.Add(new DownFlagment(index, size, sIdx, sIdx + size - 1, 0));

				sIdx += size;
			}
		}
		#endregion

		#region 文件 序列化/反序列化
		/// <summary>
		/// 序列化到文件
		/// </summary>
		/// <param name="data"></param>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public bool SerializeToFile(string filePath)
		{
			var ret = false;
			FileStream fileStream = null;
			BinaryWriter binaryWriter = null;

			try
			{
				fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
				binaryWriter = new BinaryWriter(fileStream);

				// 写入ID
				binaryWriter.Write(filePath.GetHashCode());

				// 写入分片数量
				var len = _flagments.Count;
				binaryWriter.Write(len);
				for (int i = 0; i < len; ++i)
				{
					var flagment = _flagments[i];
					binaryWriter.Write(flagment.Index);
					binaryWriter.Write(flagment.Size);
					binaryWriter.Write(flagment.StartRange);
					binaryWriter.Write(flagment.EndRange);
					binaryWriter.Write(flagment.DownloadedSize);
				}

				ret = true;
			}
			catch (Exception e)
			{
				Log.Error($"SerializeToFile Error, Path：{filePath} Exception：{e}");
			}
			finally
			{
				binaryWriter?.Close();
				fileStream?.Close();
			}

			return ret;
		}

		/// <summary>
		/// 从文件中反序列化
		/// </summary>
		public bool DeserializeFromFile(string filePath)
		{
			var ret = false;
			FileStream fileStream = null;
			BinaryReader binaryReader = null;

			try
			{
				fileStream = new FileStream(filePath, FileMode.Open);
				binaryReader = new BinaryReader(fileStream);

				// 读取ID
				var savedGuid = binaryReader.ReadInt32();
				if (savedGuid != filePath.GetHashCode())
				{
					Log.Error($"DeserializeFromFile文件Guid不一致，Path：{filePath} Guid：{filePath.GetHashCode()} SavedGuid：{savedGuid}");
					return false;
				}

				// 读取分片数量
				var count = binaryReader.ReadInt32();
				for (int i = 0; i < count; ++i)
				{
					_flagments.Add(new DownFlagment(
						binaryReader.ReadInt32(),
						binaryReader.ReadInt32(),
						binaryReader.ReadInt32(),
						binaryReader.ReadInt32(),
						binaryReader.ReadInt32()
						));
				}

				Log.Print($"DeserializeFromFile Path：{filePath} 解析进度：{(int)(GetDownloadedSize() * 100.0f / GetTotalSize())}%");
				ret = true;
			}
			catch (Exception e)
			{
				Log.Error($"DeserializeFromFile Error, Path：{filePath} Exception：{e}");
			}
			finally
			{
				binaryReader?.Close();
				fileStream?.Close();
			}

			return ret;
		}
		#endregion

		#region 大小（字节数）
		/// <summary>
		/// 获取已下载的字节数
		/// </summary>
		/// <returns></returns>
		public int GetDownloadedSize()
		{
			int size = 0;
			var len = _flagments.Count;
			for (int i = 0; i < len; ++i)
			{
				size += _flagments[i].DownloadedSize;
			}
			return size;
		}

		/// <summary>
		/// 获取总字节数
		/// </summary>
		/// <returns></returns>
		public int GetTotalSize()
		{
			int size = 0;
			var len = _flagments.Count;
			for (int i = 0; i < len; ++i)
			{
				size += _flagments[i].Size;
			}
			return size;
		}
		#endregion

		#region 获取待加载分片
		public DownFlagment GetNextFlagment()
		{
			lock (this)
			{
				for (int i = 0; i < _flagments.Count; ++i)
				{
					if (IsComplete)
						break;

					var flagment = _flagments.ElementAt(i);
					if (flagment == null || flagment.downloading || flagment.IsComplete)
						break;

					flagment.downloading = true;
					return flagment;
				}
			}

			return null;
		}
		#endregion
	}
}