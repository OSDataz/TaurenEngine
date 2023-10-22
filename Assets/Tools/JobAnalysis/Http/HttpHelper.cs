/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/3 12:11:12
 *└────────────────────────┘*/

using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Tools.JobAnalysis
{
	public class HttpHelper
	{
		public void SendWebRequest(string url, Action<bool, string> callback)
		{
			UnityWebRequest webRequest = UnityWebRequest.Get(url);

			// 异步发送请求，并在请求完成后调用回调函数
			webRequest.SendWebRequest().completed += (operation) =>
			{
				Debug.Log(operation.isDone);

				if (webRequest.result == UnityWebRequest.Result.Success)
				{
					// 请求成功，调用回调函数并传递响应文本内容
					callback(true, webRequest.downloadHandler.text);
				}
				else
				{
					// 请求失败，调用回调函数并传递错误信息
					callback(false, webRequest.error);
				}

				// 完成后释放资源
				webRequest.Dispose();
			};
		}
	}
}