/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/18 20:16:06
 *└────────────────────────┘*/

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace TaurenEngine.Core
{
	/// <summary>
	/// HTTP下载器，将远程资源加载并保存到本地指定路径
	/// </summary>
	public class HttpDownloader
	{
		private const string tempExt = ".temp";

		/// <summary> 分片下载管理器 </summary>
		private DownFlagmentManager _flagMgr;

		public void Download(string fileUrl, string saveFilePath, int totalSize, int maxThreadCount, Action<bool> callback)
		{
			// 检测创建本地的文件夹路径
			Filex.CreateDirectoryByFilePath(saveFilePath);

			// 创建分片管理器
			_flagMgr = CreateDownFlagmentManager(fileUrl, totalSize);
			if (_flagMgr == null || _flagMgr.FlagmentCount == 0)
			{
				callback?.Invoke(false);
				return;
			}

			// 多线程
			if (maxThreadCount < 1)
			{
				maxThreadCount = _flagMgr.FlagmentCount / 3;
				if (maxThreadCount < 1) maxThreadCount = 1;
			}
			else
			{
				if (maxThreadCount > _flagMgr.FlagmentCount) maxThreadCount = _flagMgr.FlagmentCount;
				if (maxThreadCount > 8) maxThreadCount = 8;
			}

			for (int i = 0; i < maxThreadCount; ++i)
			{

			}
		}

		#region 创建分片管理器
		private DownFlagmentManager CreateDownFlagmentManager(string fileUrl, int totalSize)
		{
			var tempPath = fileUrl + tempExt;

			// 断点续传（延续上次保存继续下载）
			if (File.Exists(tempPath) && File.Exists(fileUrl))
			{
				var flagMgr = new DownFlagmentManager();
				if (flagMgr.DeserializeFromFile(fileUrl))
					return flagMgr;
			}

			// 未知文件尺寸
			if (totalSize <= 0)
			{
				totalSize = GetTotalSizeInNetStream(fileUrl, 3); // 参数：重试次数

				//获取文件大小出错
				if (totalSize <= 0)
				{
					Log.Error($"获取文件大小失败，Path：{fileUrl}");
					return null;
				}
			}

			// 初始化文件尺寸
			if (!File.Exists(tempPath))
			{
				var fileStream = new FileStream(tempPath, FileMode.Create);
				fileStream.SetLength(totalSize); // 设置文件大小
				fileStream.Close();
			}

			// 对即将下载的文件进行预先分片
			var newFlagMgr = new DownFlagmentManager();
			newFlagMgr.InitDownFlagment(totalSize);

			return newFlagMgr;
		}
		#endregion

		#region 获取文件流总大小
		/// <summary>
		/// 获取文件流总字节大小
		/// </summary>
		private int GetTotalSizeInNetStream(string fileURL, int retryTimes)
		{
			HttpWebResponse response;
			Stream netStream = GetNetStream(fileURL, 0, -1, out response, retryTimes);

			if (netStream == null)
				return 0;

			// 内容长度
			int contentLength = (int)response.ContentLength;

			// 区间范围
			string rangeStr = response.GetResponseHeader("Content-Range");
			int rangeTotalSize = GetTotalSizeInRange(rangeStr);

			Log.Info($"ContentLength:{contentLength} RangeSize:{rangeTotalSize}");

			//优先区间范围
			int totalSize;
			if (rangeTotalSize > 0)
			{
				totalSize = rangeTotalSize;
			}
			else
			{
				totalSize = contentLength;
			}
			netStream.Close();
			return totalSize;
		}

		/// <summary>
		/// 获取range属性中总字节数量
		/// </summary>
		private int GetTotalSizeInRange(string rangeStr)
		{
			if (rangeStr != null && rangeStr.Length > 0)
			{
				try
				{
					int ptIndex = rangeStr.LastIndexOf("/");
					if (ptIndex > 0)
					{
						string rangeSizeStr = rangeStr.Substring(ptIndex + 1);
						int size = int.Parse(rangeSizeStr);//解析总字节数量
						return size;
					}
				}
				catch (Exception e)
				{
					Log.Error($"[ERROR]GetTotalSizeInRange:{rangeStr} at:{e}");
				}
			}

			return -1;
		}
		#endregion

		#region 网络文件流
		private Stream GetNetStream(string url, int startRange, int endRange, out HttpWebResponse response, int retryTimes)
		{
			// 打开网络连接
			var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
			if (startRange >= 0)
			{
				if (endRange >= startRange)// 设置Range区域
				{
					httpWebRequest.AddRange(startRange, endRange);// 设置Range值，用于断点续传
				}
				else// 设置Range开始区域，默认到文件末尾
				{
					httpWebRequest.AddRange(startRange);// 设置Range值，用于断点续传
				}
			}

			// 设置请求参数
			httpWebRequest.Timeout = 8 * 1000;// 参数：超时
			httpWebRequest.ReadWriteTimeout = 8 * 1000;// 参数：超时
			httpWebRequest.KeepAlive = false;

			// 向服务器请求,获得服务器的回应数据流
			try
			{
				response = httpWebRequest.GetResponse() as HttpWebResponse;
			}
			catch (WebException e)
			{
				Log.Warn("GetResponse Exception:" + e.ToString());
				response = e.Response as HttpWebResponse;
			}

			if (response == null)
				return null;

			HttpStatusCode statusCode = response.StatusCode;

			if (statusCode >= HttpStatusCode.OK && statusCode <= HttpStatusCode.OK + 6)// [200, 206]
			{
				var contentType = response.Headers["Content-Type"];
				if (contentType != null && contentType.Contains("text"))
				{
					// 返回类型不正确，可能联网有问题，比如弹出认证页面
					ToLogNetError(response);
					response.Close();
					return null;
				}

				return response.GetResponseStream();
			}
			else if (statusCode >= HttpStatusCode.Redirect - 2 && statusCode <= HttpStatusCode.Redirect + 5)// [300, 307] 重定向
			{
				var newURL = response.GetResponseHeader("Location");
				Log.Info($"HttpDownloader url={url} response={statusCode} Redirec_tUrL={newURL}");
				response.Close();

				return GetNetStream(newURL, startRange, endRange, out response, retryTimes);
			}
			else if (statusCode >= HttpStatusCode.BadRequest && statusCode <= HttpStatusCode.BadRequest + 17)// [400, 417] 请求异常
			{
				Log.Info($"HttpDownloader url={url} response={statusCode}");
				response.Close();

				if (retryTimes > 0)// 重试
					return GetNetStream(url, startRange, endRange, out response, retryTimes - 1);

				return null;
			}
			else if (statusCode >= HttpStatusCode.InternalServerError && statusCode <= HttpStatusCode.InternalServerError + 5)// [500, 505] 服务器错误
			{
				Log.Info($"HttpDownloader url={url} response={statusCode}");
				response.Close();

				if (retryTimes > 0)// 重试
					return GetNetStream(url, startRange, endRange, out response, retryTimes - 1);

				return null;
			}
			else
			{
				Log.Info($"HttpDownloader url={url} response={statusCode}");
				response.Close();

				if (retryTimes > 0)// 重试
					return GetNetStream(url, startRange, endRange, out response, retryTimes - 1);

				return null;
			}
		}

		private void ToLogNetError(HttpWebResponse response)
		{
			var headers = response.Headers;
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < headers.Count; i++)
			{
				sb.Append("<Header " + i + ">: " + headers.GetKey(i) + "\t=\t");
				String[] val = headers.GetValues(i);
				if (val != null)
				{
					for (int j = 0; j < val.Length; j++)
					{
						sb.Append(val[j]);
						if (j < val.Length - 1) sb.Append("#");
					}
				}
				sb.Append("\r\n");
			}

			Log.Error("The network is abnormal. The authentication page may appear.\r\n" + sb.ToString());
		}
		#endregion
























		/// <summary>
		/// 下载资源
		/// </summary>
		/// <param name="fileUrl">资源地址</param>
		/// <param name="savePath">保存地址</param>
		/// <param name="flagment">分片片段</param>
		/// <param name="flagmentManager">分片管理器</param>
		/// <param name="retryTimes">重试次数</param>
		/// <returns></returns>
		public bool Download(string fileUrl, string savePath, DownFlagment flagment, DownFlagmentManager flagmentManager, int retryTimes = 3)
		{
			bool result = false;// 是否成功完成下载

			// 获取文件流
			Stream netStream = null;
			FileStream fileStream = null;

			try
			{
				fileStream = GetFileStreamShareWrite(savePath);
				if (fileStream == null)
				{
					Log.Error($"打开本地文件异常，Flagment Index：{flagment.Index}");
					return false;// 下载失败
				}

				// 打开已经下载的文件尺寸
				int seekPosition = flagment.StartRange + flagment.DownloadedSize;
				if (seekPosition > 0)
				{
					fileStream.Seek(seekPosition, SeekOrigin.Current);
				}

				// 打开网络连接
				HttpWebResponse response;
				netStream = GetNetStream(fileUrl, seekPosition, flagment.EndRange, out response, retryTimes);
				if (netStream == null)
				{
					Log.Error($"[ThreadID:{Thread.CurrentThread.ManagedThreadId}] [Flagment Index:{flagment.Index}] 打开网络链接异常");
					return false;
				}

				// 定义读取buff
				int buffSize = 128 * 1024;
				byte[] bytes = new byte[buffSize];
				while (flagmentManager.IsComplete)
				{
					// 剩余字节数
					int leaveSize = flagment.Size - flagment.DownloadedSize;

					// 正常下载完成
					if (leaveSize <= 0)
						break;

					// 读取数据
					int readSize = 0;
					var len = leaveSize < buffSize ? leaveSize : buffSize;
					int off = 0;
					while (len > 0)
					{
						int n = netStream.Read(bytes, off, len);
						if (n <= 0)
							break;//数据流末尾

						off += n;
						len -= n;
						readSize += n;
					}

					if (!flagmentManager.IsComplete)
						break;

					if (readSize <= 0)// 异常，数据流已经到末尾，但任务字节数还未完成
						throw new Exception($"[startDownloadFile] readSize is {readSize}, but DownloadedCount is not full!");

					fileStream.Write(bytes, 0, readSize);
					fileStream.Flush();// 影响效率
					flagment.DownloadedSize = readSize;
				}

				result = true;// 下载成功
			}
			catch (Exception e)
			{
				result = false;// 下载失败
				Log.Error($"[ThreadID:{Thread.CurrentThread.ManagedThreadId}] [Flagment Index:{flagment.Index}] [ERROR]HttpDownloader url={fileUrl} Exception:{e}");
			}
			finally
			{
				// 关闭流
				if (fileStream != null)
				{
					try
					{
						fileStream.Close();
					}
					catch (Exception) { }

				}

				if (netStream != null)
				{
					try
					{
						netStream.Close();
					}
					catch (Exception) { }
				}
			}

			return result;
		}

		#region 本地文件流
		/// <summary>
		/// 获取本地共享写入文件流
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		private FileStream GetFileStreamShareWrite(string path)
		{
			// 实例化文件流对象，保存下载数据
			FileStream fileStream = null;
			try
			{
				// 打开要下载的文件
				fileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
			}
			catch (Exception e)
			{
				Log.Error($"GetFileStreamShareWrite Path:{path} Error:{e}");
			}

			return fileStream;
		}
		#endregion

		

		
	}
}