/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/8/24 20:46:47
 *└────────────────────────┘*/

using System;

namespace TaurenEngine
{
	/// <summary>
	/// 多线程任务，让多个线程去完成同一个任务列表
	/// </summary>
	public class MultiThread
	{
		/// <summary> 任务池-多线程共享 </summary>
		private ThreadTaskList _taskList = new ThreadTaskList();

		/// <summary> 是否停止任务(停止未启动任务，已启动任务无法停止) </summary>
		public bool Stop { get; set; }

		public void Start(int maxThreadCount, int priority = -1, Action<int, int> onProgress = null, Action onComplete = null)
		{
			var threadCount = maxThreadCount <= 0 ? 3 : maxThreadCount;
			int finishCount = 0;// 统计任务完成的线程

			for (int i = 0; i < threadCount; ++i)
			{
				ProxyThread thread = new ProxyThread();
				thread.SetPriority(priority);

				Action taskAction = () =>
				{
					do
					{
						onProgress?.Invoke(_taskList.FinishTaskCount, _taskList.TotalTaskCount);
					}
					while (!Stop && _taskList.Run());

					var isFinish = false;
					lock (_taskList)
					{
						finishCount += 1;// 统计任务完成的线程
						isFinish = finishCount >= threadCount;
					}

					if (isFinish)// 完成所有任务
					{
						onComplete?.Invoke();
					}
				};

				thread.runAction = taskAction;
				thread.Start();
			}
		}

		public void AddTask(IThreadTask task)
		{
			_taskList.Add(task);
		}

		public void RemoveTask(IThreadTask task)
		{
			_taskList.Remove(task);
		}
	}
}