/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/1 21:49:42
 *└────────────────────────┘*/

using TaurenEngine.Runtime.Launch;
using UnityEditor;

namespace TaurenEngine.Editor
{
	/// <summary>
	/// 启动组件编辑
	/// </summary>
	[CustomEditor(typeof(LaunchComponent))]
	public class LaunchComponentEditor : UnityEditor.Editor
	{
	}
}