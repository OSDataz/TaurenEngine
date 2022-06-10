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
	public class HotfixData : ScriptableObject
	{
		public List<HotfixDll> dlls;
	}

	[Serializable]
	public sealed class HotfixDll
	{
		public string name;
		public string launchClass;
		public string launchMethod;
		/// <summary>
		/// 是否在Mono组件使用，热更组件挂载了Mono GameObejct对象上
		/// </summary>
		public bool useByMono;
	}
}