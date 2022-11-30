﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/26 20:57:08
 *└────────────────────────┘*/

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

namespace TaurenEngine
{
	/// <summary>
	/// HTTP下载器，将远程资源加载并保存到本地指定路径
	/// </summary>
	internal class HttpDownloader
	{
		/// <summary>
		/// 下载资源
		/// </summary>
		/// <param name="fileURL">资源地址</param>
		/// <param name="savePath">保存地址</param>
		/// <param name="flagment">分片片段</param>
		/// <param name="flagmentManager">分片管理器</param>
		/// <param name="retryTimes">重试次数</param>
		/// <returns></returns>
		public bool Download(string fileURL, string savePath, FlagmentCell flagment, FlagmentManager flagmentManager, int retryTimes = 3)
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
					Debug.LogError($"打开本地文件异常，Flagment Index：{flagment.index}");
					return false;// 下载失败
				}

				// 打开已经下载的文件尺寸
				int seekPosition = flagment.startRange + flagment.DownloadSize;
				if (seekPosition > 0)
				{
					fileStream.Seek(seekPosition, SeekOrigin.Current);
				}

				// 打开网络连接
				HttpWebResponse response;
				netStream = GetNetStream(fileURL, seekPosition, flagment.endRange, out response, retryTimes);
				if (netStream == null)
				{
					Debug.LogError($"[ThreadID:{Thread.CurrentThread.ManagedThreadId}] [Flagment Index:{flagment.index}] 打开网络链接异常");
					return false;
				}

				// 定义读取buff
				int buffSize = 128 * 1024;
				byte[] bytes = new byte[buffSize];
				while (!flagmentManager.IsExit)
				{
					// 剩余字节数
					int leaveSize = flagment.sizeTotal - flagment.DownloadSize;

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

					if (flagmentManager.IsExit)
						break;

					if (readSize <= 0)// 异常，数据流已经到末尾，但任务字节数还未完成
						throw new Exception($"[startDownloadFile] readSize is {readSize}, but DownloadedCount is not full!");

					fileStream.Write(bytes, 0, readSize);
					fileStream.Flush();// 影响效率
					flagment.AddDownloadSize(readSize);
				}

				result = true;// 下载成功
			}
			catch (Exception e)
			{
				result = false;// 下载失败
				Debug.LogError($"[ThreadID:{Thread.CurrentThread.ManagedThreadId}] [Flagment Index:{flagment.index}] [ERROR]HttpDownloader url={fileURL} Exception:{e}");
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
				Debug.LogError($"GetFileStreamShareWrite Path:{path} Error:{e}");
			}

			return fileStream;
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
			httpWebRequest.Timeout = 8 * 1000;// 超时
			httpWebRequest.ReadWriteTimeout = 8 * 1000;// 超时
			httpWebRequest.KeepAlive = false;

			// 向服务器请求,获得服务器的回应数据流
			try
			{
				response = httpWebRequest.GetResponse() as HttpWebResponse;
			}
			catch (WebException e)
			{
				Debug.LogWarning("GetResponse Exception:" + e.ToString());
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
				Debug.Log($"HttpDownloader url={url} response={statusCode} Redirec_tUrL={newURL}");
				response.Close();
				return GetNetStream(newURL, startRange, endRange, out response, retryTimes);
			}
			else if (statusCode >= HttpStatusCode.BadRequest && statusCode <= HttpStatusCode.BadRequest + 17)// [400, 417] 请求异常
			{
				Debug.Log($"HttpDownloader url={url} response={statusCode}");
				response.Close();
				if (retryTimes > 0)// 重试
					return GetNetStream(url, startRange, endRange, out response, retryTimes - 1);

				return null;
			}
			else if (statusCode >= HttpStatusCode.InternalServerError && statusCode <= HttpStatusCode.InternalServerError + 5)// [500, 505] 服务器错误
			{
				Debug.Log($"HttpDownloader url={url} response={statusCode}");
				response.Close();
				if (retryTimes > 0)// 重试
					return GetNetStream(url, startRange, endRange, out response, retryTimes - 1);

				return null;
			}
			else
			{
				Debug.Log($"HttpDownloader url={url} response={statusCode}");
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

			Debug.LogError("The network is abnormal. The authentication page may appear.\r\n" + sb.ToString());
		}
		#endregion

		#region 获取文件流总尺寸
		/// <summary>
		/// 获取文件流总字节尺寸
		/// </summary>
		public int GetTotalSizeInNetStream(string fileURL, int retryTimes = 3)
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

			Debug.Log($"ContentLength:{contentLength} RangeSize:{rangeTotalSize}");

			//优先区间范围
			int totalSize = 0;
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
					Debug.LogError($"[ERROR]GetTotalSizeInRange:{rangeStr} at:{e}");
				}
			}

			return -1;
		}
		#endregion
	}
}