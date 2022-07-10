/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 8:35:48
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.MoveEx
{
	internal class MoveBind
	{
		/// <summary>
		/// 移动对象
		/// </summary>
		internal Transform target;
		/// <summary>
		/// 绑定的对象
		/// </summary>
		private Transform _bindTarget;

		/// <summary>
		/// 偏差位置
		/// </summary>
		private Vector3 _offset;

		public void SetData(Transform bindTarget)
		{
			_bindTarget = bindTarget;
			_offset = target.position - _bindTarget.position;
		}

		public void SetData(Transform bindTarget, Vector3 offset)
		{
			_bindTarget = bindTarget;
			_offset = offset;
		}

		public void Update()
		{
			target.position = _bindTarget.position + _offset;
		}
	}
}