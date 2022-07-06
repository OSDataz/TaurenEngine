/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/31 15:48:28
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 摄像机组件
	/// </summary>
	public class CameraComponent : FrameworkComponent
	{
		/// <summary>
		/// 世界摄像机
		/// </summary>
		public Camera worldCamera;
		/// <summary>
		/// UI摄像机
		/// </summary>
		public Camera uiCamera;
	}
}