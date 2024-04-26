/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/2 17:57:40
 *└────────────────────────┘*/

using System;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public interface IEventService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IEventService Instance { get; internal set; }

		void ListenEvent(string key, Action callAction, bool isOnce = false);

		void ListenEvent(string key, Action<object> callAction, bool isOnce = false);

		void ListenEvent<TData>(string key, Action<TData> callAction, bool isOnce = false);

		void RemoveEvent(string key, Action callAction);

		void RemoveEvent(string key, Action<object> callAction);

		void RemoveEvent(string key);

		void TriggerEvent(string key, object data = null, bool async = false);
	}

	public static class IEventServiceExtension
	{
		public static void InitInterface(this IEventService @object)
		{
			IEventService.Instance = @object;
		}
	}
}