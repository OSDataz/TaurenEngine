/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/31 15:15:29
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine.Framework
{
	[Serializable]
	public class UIGroup
	{
		[SerializeField]
		private string _name = null;
		public string Name => _name;

		[SerializeField]
		private int _depth = 0;
		public int Depth => _depth;
	}
}