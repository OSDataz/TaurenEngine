/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/16 20:22:58
 *└────────────────────────┘*/

using TaurenEngine;
using UnityEditor;

namespace TaurenEditor
{
	[CustomEditor(typeof(LocalizationComponent))]
	public class LocalizationComponentEditor : UnityEditor.Editor
	{
		protected void OnEnable()
		{

		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();


		}
	}
}