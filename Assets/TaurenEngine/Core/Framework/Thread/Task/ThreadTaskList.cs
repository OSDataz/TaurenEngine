﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/8/24 20:51:39
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine
{
	/// <summary>
	/// 多线程任务列表
	/// </summary>
	public class ThreadTaskList
	{
		/// <summary> 任务列表 </summary>
		private List<IThreadTask> _taskList = new List<IThreadTask>();

		/// <summary> 进行中任务数量 </summary>
		public int RunningTaskCount { get; private set; }

		/// <summary> 已完成任务数量 </summary>
		public int FinishTaskCount { get; private set; }

		/// <summary> 获取所有任务数量(剩余任务、进行中任务、已完成任务的数量之和)，非线程安全 </summary>
		public int TotalTaskCount => _taskList.Count + RunningTaskCount + FinishTaskCount;

		public bool Run()
		{
			IThreadTask task = null;
			lock (this)
			{
				if (_taskList.Count > 0)
				{
					task = _taskList[0];
					_taskList.RemoveAt(0);
					RunningTaskCount += 1;
				}
			}

			if (task != null)
			{
				task.Run();
				lock (this)
				{
					RunningTaskCount -= 1;
					FinishTaskCount += 1;
				}

				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// 添加任务
		/// </summary>
		/// <param name="task"></param>
		public void Add(IThreadTask task)
		{
			if (task == null)
				return;

			lock (this)
			{
				_taskList.Add(task);
			}
		}

		/// <summary>
		/// 移除任务
		/// </summary>
		/// <param name="task"></param>
		public void Remove(IThreadTask task)
		{
			if (task == null)
				return;

			lock (this)
			{
				_taskList.Remove(task);
			}
		}

		/// <summary>
		/// 清空任务列表
		/// </summary>
		public void Clear()
		{
			lock (this)
			{
				_taskList.Clear();
			}
		}
	}
}