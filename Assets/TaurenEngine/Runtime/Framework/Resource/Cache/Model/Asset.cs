/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/21 20:18:01
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Runtime.Framework
{
	/// <summary>
	/// 资源对象
	/// </summary>
	public class Asset : RefrenceObject
	{
		/// <summary>
		/// 资源键值（资源路径）
		/// </summary>
		public string key;

		/// <summary>
		/// 加载出来的原始资源
		/// </summary>
		public UnityEngine.Object data;

		/// <summary>
		/// 资源弱引用
		/// </summary>
		private WeakReference _dataWeakRef;

		/// <summary>
		/// 是否可缓存
		/// </summary>
		public bool cacheable;

		/// <summary>
		/// 最近访问时间
		/// </summary>
		internal float visitTime;

		/// <summary>
		/// 内存占用（字节）
		/// </summary>
		public int MemorySize { get; private set; }

		/// <summary>
		/// 获取转化指定类型的资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public bool TryGet<T>(out T asset) where T : UnityEngine.Object
		{
			if (data is T tData)
			{
				asset = tData;
				return true;
			}

			asset = null;
			return false;
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

				if (_dataWeakRef == null)
					return false;

				return _dataWeakRef.IsAlive && _dataWeakRef.Target != null;
			}
		}

		/// <summary>
		/// 是否是弱引用状态
		/// </summary>
		public bool IsWeakReference
		{
			get => _dataWeakRef != null;
			set
			{
				if (value)
				{
					if (_dataWeakRef == null)
					{
						_dataWeakRef = new WeakReference(data);
						data = null;
					}
				}
				else
				{
					if (_dataWeakRef != null)
					{
						data = _dataWeakRef.Target as UnityEngine.Object;
						_dataWeakRef = null;
					}
				}
			}
		}
	}
}