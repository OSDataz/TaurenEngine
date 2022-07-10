/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 17:06:12
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.HardwareEx
{
	/// <summary>
	/// 指南针管理器
	/// </summary>
	public class CompassDevice : MultSwitchControl<CompassDevice>
	{
		private readonly Compass _compass;

		public CompassDevice()
		{
			_compass = Input.compass;
		}

		protected override void UpdateEnabled()
		{
			_compass.enabled = Enabled;
		}

		public Compass Compass => _compass;
	}
}