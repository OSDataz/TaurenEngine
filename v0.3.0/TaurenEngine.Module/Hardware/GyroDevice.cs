/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 17:06:06
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.HardwareEx
{
	/// <summary>
	/// 陀螺仪管理器
	/// </summary>
	public class GyroDevice : MultSwitchControl<GyroDevice>
	{
		private readonly Gyroscope _gyro;

		public GyroDevice()
		{
			_gyro = Input.gyro;
		}

		protected override void UpdateEnabled()
		{
			_gyro.enabled = Enabled;
		}

		public Gyroscope Gyro => _gyro;
		public bool IsAvailable => SystemInfo.supportsGyroscope;
	}
}