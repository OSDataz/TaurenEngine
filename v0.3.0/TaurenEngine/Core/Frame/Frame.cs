/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/7/30 23:18:30
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 【注意】切勿自行创建，请使用FrameManager
	/// </summary>
	internal class Frame : IRecycle, IFrameUpdate
	{
		#region 静态数据
		private static uint To_Id = 0;
		#endregion

		/// <summary> 唯一ID </summary>
		public uint Id { get; private set; }
		/// <summary> 运行中 </summary>
		public bool Running { get; protected set; }

		/// <summary> 是否使用对象池 </summary>
		public bool usePool;

		public Action callAction;

		public Frame() { Id = ++To_Id; }

		/// <summary>
		/// 开始帧计时器
		/// </summary>
		public virtual void Start()
		{
			if (Running)
				return;

			Running = true;

			FrameManager.Instance.AddFrame(this);
		}

		/// <summary>
		/// 停止帧计时器
		/// </summary>
		public void Stop()
		{
			if (!Running)
				return;

			Running = false;

			FrameManager.Instance.RemoveFrame(this);
		}

		public virtual void Clear()
		{
			Stop();

			Id = ++To_Id;

			callAction = null;
		}

		/// <summary>
		/// 销毁帧计时器
		/// </summary>
		public virtual void Destroy()
		{
			Stop();

			callAction = null;
		}

		public virtual void CheckPoolDestroy()
		{
			if (usePool)
				FrameManager.Instance.framePool.Add(this);
			else
				Destroy();
		}
	}
}