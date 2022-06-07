/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/6 14:59:22
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public sealed class ILRuntimeData : ScriptableObject
	{
		public string dllPath;
		public string generateCodeSavePath;

		public List<ILRuntimeAdaptorGroup> adaptorGroupList;
	}

	[Serializable]
	public sealed class ILRuntimeAdaptorGroup
	{
		public bool showDetails;
		public string name;
		public string assemblyName;
		public string generateNamespace;
		public string adaptorSavePath;

		public List<ILRuntimeAdaptorItem> adaptorList;
	}

	[Serializable]
	public sealed class ILRuntimeAdaptorItem
	{
		public bool selected;
		public string fileName;
		public string fullName;
	}
}