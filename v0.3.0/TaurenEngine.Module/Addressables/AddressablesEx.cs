/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/11/8 0:04:28
 *└────────────────────────┘*/

using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 同步加载接口都是弃用的
	/// </summary>
	public static class AddressablesEx
	{
		/// <summary>
		/// 初始化可寻址加载器
		/// </summary>
		/// <param name="onInitComplete">初始化完成回调</param>
		public static async void Initialization(Action onInitComplete)
		{
			await Addressables.InitializeAsync().Task;
			onInitComplete?.Invoke();
		}

		/// <summary>
		/// 加载资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="path"></param>
		/// <param name="onLoadComplete"></param>
		public static async void LoadAsync<T>(string path, Action<T> onLoadComplete)
		{
			var handle = Addressables.LoadAssetAsync<T>(path);
			await handle.Task;

			if (handle.Status == AsyncOperationStatus.Succeeded)
				onLoadComplete.Invoke(handle.Task.Result);
			else
				onLoadComplete.Invoke(default(T));
		}

		/// <summary>
		/// 释放资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		public static void Release<T>(T obj) => Addressables.Release(obj);
	}
}