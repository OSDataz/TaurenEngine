/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/1 14:31:53
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 网络服务管理器
	/// </summary>
	public class NetworkManager
	{
		public void Send<TReq, TRecv>(TReq message, Action<TRecv> onReceive = null)
		{
			
		}

		public void AddEvent<TRecv>(Action<TRecv> onReceive)
		{

		}

		public void RemoveEvent<TRecv>(Action<TRecv> onReceive)
		{

		}
	}
}