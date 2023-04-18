/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/31 10:38:12
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Unity
{
	/// <summary>
	/// 计时器/循环组件
	/// </summary>
	public class TimerComponent : MonoComponent
	{
		private void Update()
		{
			Timer.UpdateList.ForEach(OnUpdate, true);
		}

		private void LateUpdate()
		{
			Timer.LateUpdateList.ForEach(OnUpdate, true);
		}

		private void FixedUpdate()
		{
			Timer.FixedUpdateList.ForEach(OnUpdate, true);
		}

		private void OnUpdate(Timer timer)
		{
			if (timer.IsInterval)
			{
				if (Time.time >= timer.TriggerTime)
					timer.ExecuteInterval();
			}
			else
			{
				timer.Execute();
			}
		}
	}
}