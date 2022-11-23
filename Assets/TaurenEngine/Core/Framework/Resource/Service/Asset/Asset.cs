/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/21 20:18:01
 *└────────────────────────┘*/

using System;

namespace TaurenEngine
{
	/// <summary>
	/// 加载完成的资源
	/// </summary>
	internal class Asset : RefObject
	{
		/// <summary>
		/// 加载出来的原始资源
		/// </summary>
		public UnityEngine.Object data;

		/// <summary>
		/// 资源弱引用
		/// </summary>
		public WeakReference dataWeakRef;

		/// <summary>
		/// 是否可缓存
		/// </summary>
		public bool cacheable;

		/// <summary>
		/// 最近访问时间
		/// </summary>
		public float visitTime;

		/// <summary>
		/// 内存占用
		/// </summary>
		public int MemorySize { get; private set; }

		/// <summary>
		/// 获取转化指定类型的资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T Get<T>() where T : UnityEngine.Object
		{
			return data as T;
		}

		/// <summary>
		/// 是否有资源数据
		/// </summary>
		public bool HasData
		{
			get
			{
				if (data != null)
					return true;

				if (dataWeakRef == null)
					return false;

				return dataWeakRef.IsAlive && dataWeakRef.Target != null;
			}
		}

		public override void Destroy()
		{
			if (data != null)
			{
				dataWeakRef = new WeakReference(data);
				data = null;
			}
			else
			{
				base.Destroy();
			}
		}
	}
}