/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/24 23:37:56
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Framework
{
	internal abstract class Asset<T> : Asset
	{
		/// <summary>
		/// 资源
		/// </summary>
		public T data;

		public override Type AssetType { get; } = typeof(T);
	}

	internal abstract class Asset
	{
		/// <summary>
		/// 资源类型
		/// </summary>
		public abstract Type AssetType { get; }

		/// <summary>
		/// 释放资源
		/// </summary>
		public abstract void Release();
	}
}