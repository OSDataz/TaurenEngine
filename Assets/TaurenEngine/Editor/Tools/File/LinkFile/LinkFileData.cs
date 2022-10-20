/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2022/1/15 11:38:58
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TaurenEditor
{
	public sealed class LinkFileData : ScriptableObject
	{
		public List<LinkFileGroup> groups;
	}

	[Serializable]
	public sealed class LinkFileGroup
	{
		public bool showDetails;
		public string name;
		public List<LinkFileRootGroup> groups;
	}

	[Serializable]
	public sealed class LinkFileRootGroup
	{
		public string rootFromPath;
		public string rootToPath;
		public List<LinkFileItem> items;
	}

	[Serializable]
	public sealed class LinkFileItem
	{
		public string fromPath;
		public string toPath;
	}
}