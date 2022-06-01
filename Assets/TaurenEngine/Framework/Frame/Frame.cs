/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/25 17:12:40
 *└────────────────────────┘*/

using System;
using TaurenEngine.Core;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 【注意】切勿自行创建，请使用 TaurenFramework.Frame
	/// </summary>
	internal abstract class Frame : IFrame, IRecycle
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
		/// <summary> 回调函数 </summary>
		public Action callAction;

		public Frame() { Id = ++To_Id; }

		public abstract void Start();

		public abstract void Stop();

		public virtual void Clear()
		{
			Stop();

			Id = ++To_Id;

			callAction = null;
		}

		public virtual void Destroy()
		{
			Stop();

			callAction = null;
		}

		public abstract void RecoveryOrDestroy();
	}
}