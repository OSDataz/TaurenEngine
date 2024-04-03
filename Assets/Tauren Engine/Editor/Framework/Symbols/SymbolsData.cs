/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/12/11 21:08:18
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tauren.Framework.Editor
{
	public sealed class SymbolsData : ScriptableObject
	{
		/// <summary>
		/// 宏标记列表
		/// </summary>
		public List<SymbolsItem> symbolsList;
	}

	[Serializable]
	public sealed class SymbolsItem
	{
		public string value;
		public bool selected;
		public string description;
	}
}