/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.AR
{
	public class ARPosition
	{
		#region 事件
		/// <summary>
		/// AR绑定对象重定位
		/// </summary>
		public const string EventArReset = "ARPosition_EventArReset";
		/// <summary>
		/// AR定位错误
		/// </summary>
		public const string EventArError = "ARPosition_EventArError";
		/// <summary>
		/// AR定位恢复
		/// </summary>
		public const string EventArRecovery = "ARPosition_EventArRecovery";
		#endregion

		internal bool isMoveCamera = false;

		internal void Clear()
		{
			SetBind(null);

			_resetCCount = 0;
		}

		private int _testCount = 0;

		internal void UpdateReset()
		{
			if (!isMoveCamera)
				return;

			_testCount += 1;

			if (_resetDistanceSqrtBind > 0)
			{
				var pos = _bindTransform.position - _lastBindPosition;
				_lastBindPosition = _bindTransform.position;
				if (pos.sqrMagnitude > _resetDistanceSqrtBind)
				{
					// 重定位
					_resetCCount += 1;

					TDebug.Log($"AR重定位次数：{_resetCCount} 运行：{_testCount}");

					if (_resetCCount >= 3)
					{
						EventCenter.Dispatcher.Send(EventArError);
					}
					else
					{
						_resetOff.Value = pos;
						EventCenter.Dispatcher.Send(EventArReset, _resetOff);
					}
				}
				else
				{
					if (_resetCCount > 0)
					{
						if (_resetCCount >= 3)
							EventCenter.Dispatcher.Send(EventArRecovery);

						_resetCCount = 0;
					}
				}
			}
		}

		internal void Destroy()
		{
			_bindTransform = null;
		}

		#region 绑定对象
		private Transform _bindTransform;

		internal void SetBind(Transform transform)
		{
			_bindTransform = transform;

			if (_bindTransform != null)
			{
				_lastBindPosition = _bindTransform.position;
			}
		}

		public Transform BindTransform => _bindTransform;
		#endregion

		#region 重定位
		private Vector3 _lastBindPosition;
		/// <summary> 重定位偏移距离</summary>
		private readonly EventData<Vector3> _resetOff = new EventData<Vector3>();
		/// <summary> 重定位连续计数</summary>
		private int _resetCCount = 0;
		private float _resetDistanceSqrtBind = 0.25f;// 默认0.5米
		/// <summary>
		/// 【赋值接口】摄像机更新，AR重定位检测距离
		/// </summary>
		public float ResetDistanceBind
		{
			get => Mathf.Sqrt(_resetDistanceSqrtBind);
			set { _resetDistanceSqrtBind = value * value; }
		}
		#endregion
	}
}