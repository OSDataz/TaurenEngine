/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/8 14:15:58
 *└────────────────────────┘*/

using System.Diagnostics;
using UnityEngine;

namespace TaurenEngine
{
	public static class DebugEx
	{
		/// <summary>
		/// 日志
		/// </summary>
		/// <param name="message"></param>
		/// <param name="saveFile">保存日志到本地文件</param>
		[Conditional("DEBUG")]
		public static void Log(object message, bool saveFile = false)
		{
			UnityEngine.Debug.Log(message);
		}

		/// <summary>
		/// 日志
		/// </summary>
		/// <param name="message"></param>
		/// <param name="color">文本颜色</param>
		/// <param name="saveFile">保存日志到本地文件</param>
		public static void Log(object message, Color color, bool saveFile = false)
		{

		}

		/// <summary>
		/// 警告
		/// </summary>
		/// <param name="message"></param>
		/// <param name="saveFile">保存日志到本地文件</param>
		[Conditional("DEBUG")]
		public static void Warning(object message, bool saveFile = true)
		{
			UnityEngine.Debug.LogWarning(message);
		}

		/// <summary>
		/// 警告
		/// </summary>
		/// <param name="message"></param>
		/// <param name="color">文本颜色</param>
		/// <param name="saveFile">保存日志到本地文件</param>
		public static void Warning(object message, Color color, bool saveFile = true)
		{
			
		}

		/// <summary>
		/// 错误
		/// </summary>
		/// <param name="message"></param>
		/// <param name="saveFile">保存日志到本地文件</param>
		public static void Error(object message, bool saveFile = true)
		{
			UnityEngine.Debug.LogError(message);

			
		}

		/// <summary>
		/// 错误
		/// </summary>
		/// <param name="message"></param>
		/// <param name="color">文本颜色</param>
		/// <param name="saveFile">保存日志到本地文件</param>
		public static void Error(object message, Color color, bool saveFile = true)
		{

		}
	}
}