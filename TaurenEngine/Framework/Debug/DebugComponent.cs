/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/17 0:36:39
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 日志组件
	/// </summary>
	public class DebugComponent : FrameworkComponent
	{
		public DebugManager Debug { get; private set; }

		[SerializeField]
		private bool _debugEnabled = false;
		[SerializeField]
		private int _maxLogCount = 1000;

		protected override void Awake()
		{
			base.Awake();

			Debug = new DebugManager();
			Debug.Enabled = _debugEnabled;
			Debug.MaxLogCount = _maxLogCount;
		}
	}
}