/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/10 9:51:49
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 优先级执行队列
	/// </summary>
	public class ExecuteList
	{
		/// <summary> 最大同时执行的数量 </summary>
		public int maxCount;
		/// <summary> 执行回调 </summary>
		private Action<IExecuteItem> _onExecute;

		/// <summary> 等待队列（越靠后优先级越大，越先执行） </summary>
		private readonly List<IExecuteItem> _waitList = new List<IExecuteItem>();
		/// <summary> 进行中队列 </summary>
		private readonly List<IExecuteItem> _execList = new List<IExecuteItem>();

		/// <summary> 等待队列数量 </summary>
		public int WaitCount => _waitList.Count;
		/// <summary> 执行队列数量 </summary>
		public int ExecCount => _execList.Count;
		/// <summary> 执行队列已满 </summary>
		private bool IsFull => _execList.Count >= maxCount;

		public ExecuteList(Action<IExecuteItem> onExecute, int maxCount)
		{
			_onExecute = onExecute;
			this.maxCount = maxCount;
		}

		public void Execute(IExecuteItem item)
		{
			if (_waitList.Contains(item))
			{
				Log.Error($"重复执行，当前已在等待队列：{item}");
				return;
			}

			if (_execList.Contains(item))
			{
				Log.Error($"重复执行，当前已在执行队列：{item}");
				return;
			}

			if (IsFull)
			{
				AddWaitList(item);// 执行队列已满，添加到等待队列
				return;
			}

			ExecuteAux(item);
		}
		
		/// <summary>
		/// 执行完成
		/// </summary>
		/// <param name="item"></param>
		public void Complete(IExecuteItem item)
		{
			if (!_execList.Remove(item))
				return;

			ExecuteNext();
		}

		public bool Remove(IExecuteItem item)
		{
			if (_waitList.Remove(item))
				return true;

			if (_execList.Remove(item))
			{
				ExecuteNext();
				return true;
			}

			return false;
		}

		private void AddWaitList(IExecuteItem item)
		{
			var len = _waitList.Count;
			for (var i = 0; i < len; ++i)
			{
				if (item.Priority <= _waitList[i].Priority)
				{
					_waitList.Insert(i, item);
					return;
				}
			}

			_waitList.Add(item);
		}

		private void ExecuteAux(IExecuteItem item)
		{
			_execList.Add(item);
			_onExecute.Invoke(item);
		}

		private void ExecuteNext()
		{
			if (_waitList.Count == 0)
				return;

			var idx = _waitList.Count - 1;
			var item = _waitList[idx];

			_waitList.RemoveAt(idx);

			ExecuteAux(item);
		}
	}
}