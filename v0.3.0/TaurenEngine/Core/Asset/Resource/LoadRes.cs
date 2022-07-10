/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/11/11 11:15:56
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 加载资源数据
	/// </summary>
	/// <typeparam name="T">资源类型</typeparam>
	public abstract class LoadRes<T> : LoadRes
	{
		/// <summary>
		/// 资源
		/// </summary>
		public T data;

		public override Type ResType { get; } = typeof(T);
	}

	public abstract class LoadRes
	{
		/// <summary>
		/// 资源类型
		/// </summary>
		public abstract Type ResType { get; }

		/// <summary>
		/// 释放资源
		/// </summary>
		public abstract void Release();
	}
}