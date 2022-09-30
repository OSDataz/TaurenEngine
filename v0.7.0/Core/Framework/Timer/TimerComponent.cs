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
	public class TimerComponent : FrameworkComponent
	{
		private LoopList<Timer> _updateList;
		private LoopList<Timer> _lateUpdateList;
		private LoopList<Timer> _fixedUpdateList;

		protected override void Awake()
		{
			base.Awake();

			var timerData = InstanceManager.Instance.Get<TimerData>();
			_updateList = timerData.updateList;
			_lateUpdateList = timerData.lateUpdateList;
			_fixedUpdateList = timerData.fixedUpdateList;
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