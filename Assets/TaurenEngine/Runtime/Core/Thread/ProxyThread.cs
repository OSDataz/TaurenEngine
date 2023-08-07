/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/8/22 22:07:29
 *└────────────────────────┘*/

using System;
using System.Security;
using System.Threading;
using TaurenEngine.Runtime.Framework;

namespace TaurenEngine.Runtime.Core
{
	/// <summary>
	/// 代理线程（先作为内部类，需要扩展时再开放）
	/// </summary>
	internal class ProxyThread
	{
		/// <summary> 系统线程 </summary>
		protected Thread thread;

		/// <summary> 线程优先级（-1表示未设置） </summary>
		private int priority = -1;

		/// <summary> 任务代理 </summary>
		public Action runAction;

		/// <summary>
		/// 设置线程优先级
		/// </summary>
		/// <param name="priority"></param>
		public virtual void SetPriority(ThreadPriority priority)
		{
			this.priority = (int)priority;

			if (thread != null)
				thread.Priority = priority;
		}

		/// <summary>
		/// 设置线程优先级
		/// </summary>
		/// <param name="priority"></param>
		public virtual void SetPriority(int priority)
		{
			this.priority = priority;

			if (thread != null && priority != -1)
				thread.Priority = (ThreadPriority)priority;
		}

		/// <summary>
		/// 启动线程
		/// </summary>
		public virtual void Start()
		{
			if (thread != null)
				return;

			thread = new Thread(ThreadProc);
			thread.IsBackground = true;
			if (priority >= 0)
				thread.Priority = (ThreadPriority)priority;

			thread.Start(this);
		}

		/// <summary>
		/// 线程执行回调，要求是static函数
		/// </summary>
		/// <param name="obj"></param>
		protected static void ThreadProc(object obj)
		{
			var proxyThread = (ProxyThread)obj;

			// 执行自身线程任务
			proxyThread.Run();

			// 执行代理任务
			proxyThread.runAction?.Invoke();

			// 执行完毕，释放线程
			proxyThread.Clear();
		}

		public virtual void Run()
		{
		}

		/// <summary>
		/// 中断线程运行
		/// </summary>
		public virtual void Abort()
		{
			if (thread == null)
				return;

			try
			{
				thread.Abort();
				thread = null;
			}
			catch (SecurityException se)
			{
				Log.Error("ProxyThread.Abort：" + se.ToString());
			}
			catch (ThreadStateException te)
			{
				Log.Error("ProxyThread.Abort:" + te.ToString());
			}
			catch (Exception e)
			{
				Log.Error("ProxyThread.Abort:" + e.ToString());
			}
		}

		/// <summary>
		/// 线程停止后清理
		/// </summary>
		protected virtual void Clear()
		{
			thread = null;
		}
	}
}