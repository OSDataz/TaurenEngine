/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/9 12:08:15
 *└────────────────────────┘*/

using System.IO;
using UnityEngine;

namespace TaurenEngine.Editor
{
	public class EditorPath
	{
		public static string ProjectPath => Path.GetDirectoryName(Application.dataPath);
	}
}