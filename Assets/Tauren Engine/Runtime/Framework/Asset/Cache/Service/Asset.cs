/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 20:46:40
 *└────────────────────────┘*/

using System;
using Tauren.Core.Runtime;
using UnityEngine;
using UnityEngine.Profiling;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 资源对象
	/// </summary>
	public class Asset : RefrenceObject, IAsset
	{
		/// <summary>
		/// 资源键值（资源路径）
		/// </summary>
		public string key;

		/// <summary>
		/// 加载出来的原始资源
		/// </summary>
		private UnityEngine.Object _data;
		public UnityEngine.Object Data
		{
			get => _data;
			set
			{
				_data = value;

#if BUILD_MODE_DEBUG
				UpdateMemorySize();
#endif
			}
		}

		/// <summary>
		/// 资源弱引用
		/// </summary>
		private WeakReference _dataWeakRef;

		/// <summary>
		/// 最近访问时间
		/// </summary>
		internal float visitTime;

		/// <summary>
		/// 获取转化指定类型的资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public bool TryGetAsset<T>(out T asset) where T : UnityEngine.Object
		{
			if (Data is T tData)
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
				if (Data != null)
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
						_dataWeakRef = new WeakReference(_data);
						Data = null;
					}
				}
				else
				{
					if (_dataWeakRef != null)
					{
						Data = _dataWeakRef.Target as UnityEngine.Object;
						_dataWeakRef = null;
					}
				}
			}
		}

		#region 更新内存占用
#if BUILD_MODE_DEBUG
		/// <summary>
		/// 内存占用（字节）
		/// </summary>
		public long MemorySize { get; private set; }

		private void UpdateMemorySize()
		{
			if (_data != null)
			{
				if (_data is Sprite sp)
				{
					MemorySize = Profiler.GetRuntimeMemorySizeLong(sp.texture);
				}
				else
				{
					MemorySize = Profiler.GetRuntimeMemorySizeLong(_data);
				}
			}
			else
			{
				MemorySize = 0;
			}
		}
#endif
		#endregion
	}
}