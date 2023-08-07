/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/8/24 20:46:47
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Runtime.Core
{
	/// <summary>
	/// 多线程任务，让多个线程去完成同一个任务列表
	/// </summary>
	public class MultiThread
	{
		/// <summary> 任务池-多线程共享 </summary>
		private ThreadTaskList _taskList = new ThreadTaskList();

		/// <summary> 是否停止任务(停止未启动任务，已启动任务无法停止) </summary>
		public bool IsStop { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maxThreadCount">最大线程数（最小默认是3线程）</param>
		/// <param name="priority">线程优先级</param>
		/// <param name="onProgress">线程进度回调</param>
		/// <param name="onComplete">全部完成回调</param>
		public void Start(int maxThreadCount, int priority = -1, Action<int, int> onProgress = null, Action onComplete = null)
		{
			var threadCount = maxThreadCount <= 0 ? 3 : maxThreadCount;
			int finishCount = 0;// 统计任务完成的线程

			for (int i = 0; i < threadCount; ++i)
			{
				ProxyThread thread = new ProxyThread();
				thread.SetPriority(priority);
				thread.runAction = () =>
				{
					do
					{
						onProgress?.Invoke(_taskList.FinishCount, _taskList.TotalCount);
					}
					while (!IsStop && _taskList.Run());

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