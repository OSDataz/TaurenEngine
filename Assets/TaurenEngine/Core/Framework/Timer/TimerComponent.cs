/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/31 10:38:12
 *└────────────────────────┘*/


using UnityEngine;

namespace TaurenEngine
{
	/// <summary>
	/// 计时器/循环组件
	/// </summary>
	public class TimerComponent : MonoComponent
	{
		private LoopList<Timer> _updateList;
		private LoopList<Timer> _lateUpdateList;
		private LoopList<Timer> _fixedUpdateList;

		protected void Awake()
		{
			var timerService = ServiceManager.Instance.Get<ITimerService>();
			_updateList = timerService.UpdateList;
			_lateUpdateList = timerService.LateUpdateList;
			_fixedUpdateList = timerService.FixedUpdateList;
		}

		private void Update()
		{
			_updateList.ForEach(OnUpdate, true);
		}

		private void LateUpdate()
		{
			_lateUpdateList.ForEach(OnUpdate, true);
		}

		private void FixedUpdate()
		{
			_fixedUpdateList.ForEach(OnUpdate, true);
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