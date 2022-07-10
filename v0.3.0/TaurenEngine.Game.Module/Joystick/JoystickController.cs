/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 16:40:16
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Game
{
	/// <summary>
	/// 虚拟摇杆控制器
	/// </summary>
	public class JoystickController
	{
		private Vector2 _centerPos;
		private float _radius;

		public void SetPosition(Vector2 center, float radius)
		{
			_centerPos = center;
			_radius = radius;
		}
	}
}