/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/8 18:28:06
 *└────────────────────────┘*/

using System;
using System.IO;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	internal class LoadItem : DObject, ILoadData
	{
		#region 加载参数
		/// <summary> 资源地址 </summary>
		public string path;

		/// <summary> 加载类型 </summary>
		public LoadType loadType;
		#endregion

		#region 加载结果
		public IAsset Asset { get; set; }

		public int Code { get; set; } = LoadCode.None;
		#endregion

		#region 类型判断
		/// <summary> 是否是场景资源 </summary>
		public bool IsScene => Path.GetExtension(path).ToLower() == ".unity";
		#endregion
	}

	internal class LoadItemAsync : LoadItem, ILoadHandler, IExecuteItem, IDoublyLinkedNode
	{
		#region 加载参数
		/// <summary> 加载优先级 </summary>
		public int Priority { get; set; }

		/// <summary> 加载完成回调 </summary>
		public Action<ILoadData> onComplete;
		#endregion

		#region 链表节点
		public DoublyLinkedNode Node { get; }
		#endregion

		public LoadItemAsync()
		{
			Node = new DoublyLinkedNode(this);
		}

		public void Unload()
		{
			ILoadService.Instance.UnloadAsync(this);
		}
	}
}