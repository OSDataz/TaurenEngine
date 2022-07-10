/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.6.0
 *│　Time    ：2022/6/26 18:44:54
 *└────────────────────────┘*/

using System.IO;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public static class HotfixTool
	{
		public static void UpdateHotfixDll()
		{
			var hotfixData = HotfixEditorData.Instance.Data;
			foreach (var hotfixDll in hotfixData.dlls)
			{
				var dllName = Path.GetFileNameWithoutExtension(hotfixDll.name);

				var dllPath = $"./Library/ScriptAssemblies/{dllName}.dll";
				if (File.Exists(dllPath))
				{
					File.Copy(dllPath, $"{hotfixData.hotfixDllSavePath}/{dllName}.dll.bytes", true);
					Debug.Log("更新热更DLL：" + dllName);
				}
				else
					Debug.LogError("更新热更DLL失败：" + dllName);

				var pdbPath = $"./Library/ScriptAssemblies/{dllName}.pdb";
				if (File.Exists(pdbPath))
				{
					File.Copy(pdbPath, $"{hotfixData.hotfixDllSavePath}/{dllName}.pdb.bytes", true);
					Debug.Log("更新热更PDB：" + dllName);
				}
				else
					Debug.LogError("更新热更PDB失败：" + dllName);
			}
		}
	}
}