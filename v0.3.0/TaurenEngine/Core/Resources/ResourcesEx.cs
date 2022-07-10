/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/10/18 14:06:54
 *└────────────────────────┘*/

using System;
using System.Collections;
using UnityEngine;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 资源路径：Resources文件夹后路径，且不带后缀名。Resources/【XXX/XXX】.ext。
	/// </summary>
	public static class ResourcesEx
	{
		#region 同步加载
		public static T Load<T>(string filePath) where T : UnityEngine.Object 
			=> Resources.Load<T>(filePath);

		public static T LoadInstantiate<T>(string filePath) where T : UnityEngine.Object
		{
			var data = Resources.Load<T>(filePath);
			if (data == null)
				return default(T);

			var instance = GameObject.Instantiate(data);
			Resources.UnloadAsset(data);
			return instance;
		}

		public static T[] LoadAll<T>(string directoryPath) where T : UnityEngine.Object 
			=> Resources.LoadAll<T>(directoryPath);
		#endregion

		#region 异步加载
		public static IEnumerator LoadAsync<T>(string filePath, Action<T> onLoadComplete) where T : UnityEngine.Object
		{
			var request = Resources.LoadAsync<T>(filePath);
			yield return request;
			onLoadComplete?.Invoke(request.asset as T);
		}

		public static IEnumerator LoadInstantiateAsync<T>(string filePath, Action<T> onLoadComplete) where T : UnityEngine.Object
		{
			var request = Resources.LoadAsync<T>(filePath);
			yield return request;
			onLoadComplete?.Invoke(request.asset is T data ? GameObject.Instantiate(data) : default(T));
		}
		#endregion

		#region 释放资源
		/// <summary>
		/// 从内存中卸载，只能对存储在磁盘上的资源调用该函数。
		/// </summary>
		/// <param name="assetToUnload"></param>
		public static void UnloadAsset(UnityEngine.Object assetToUnload)
			=> Resources.UnloadAsset(assetToUnload);

		/// <summary>
		/// 释放所有没有引用的Asset对象
		/// </summary>
		public static void UnloadUnusedAssets() 
			=> Resources.UnloadUnusedAssets();

		/// <summary>
		/// 协成释放所有没有引用的Asset对象
		/// </summary>
		public static IEnumerator UnloadUnusedAssetsAsync()
		{
			yield return Resources.UnloadUnusedAssets();
		}
		#endregion

		#region 加载资源
		/// <summary>
		/// 此函数可以返回已加载的任何类型的 Unity 对象，包括游戏对象、预制件、材质、网格、纹理等。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T[] FindObjectsOfTypeAll<T>() where T : UnityEngine.Object 
			=> Resources.FindObjectsOfTypeAll<T>();
		#endregion
	}
}