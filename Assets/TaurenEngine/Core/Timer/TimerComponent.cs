/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/31 10:38:12
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 计时器/循环组件
	/// </summary>
	public class TimerComponent : MonoBehaviour
	{
		private float _time;
		private float _fixedTime;

		private void Update()
		{
			_time = Time.time;

			Timer.UpdateList.ForEach(OnUpdate, true);
		}

		private void LateUpdate()
		{
			Timer.LateUpdateList.ForEach(OnUpdate, true);
		}

		private void FixedUpdate()
		{
			_fixedTime = Time.fixedTime;

			Timer.FixedUpdateList.ForEach(OnFixedUpdate, true);
		}

		private void OnUpdate(Timer timer)
		{
			if (timer.IsInterval)
			{
				if (_time >= timer.TriggerTime)
					timer.ExecuteInterval();
			}
			else
			{
				timer.Execute();
			}
		}

		private void OnFixedUpdate(Timer timer)
		{
			if (timer.IsInterval)
			{
				if (_fixedTime >= timer.TriggerTime)
					timer.ExecuteInterval();
			}
			else
			{
				timer.Execute();
			}
		}
	}
}