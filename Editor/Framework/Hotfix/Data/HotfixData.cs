/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/9 17:08:06
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public sealed class HotfixData : ScriptableObject
	{
		/// <summary>
		/// 修改代码后是否自动更新热更DLL
		/// </summary>
		public bool isDidReloadScripts;
		/// <summary>
		/// 热更DLL保存路径
		/// </summary>
		public string hotfixDllSavePath;

		public List<HotfixDll> dlls;
	}

	[Serializable]
	public sealed class HotfixDll
	{
		public string name;
		/// <summary>
		/// 是否在Mono组件使用，热更组件挂载了Mono GameObejct对象上
		/// </summary>
		public bool useByMono;
	}
}