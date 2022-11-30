/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/4 10:49:27
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine
{
	/// <summary>
	/// 日志服务
	/// </summary>
	public interface ILogService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static ILogService Instance { get; internal set; }
	}

	public static class ILogServiceExtension
	{
		public static void InitInterface(this ILogService @object, ILogService instance)
		{
			if (ILogService.Instance != null)
				Debug.LogError("IPoolService重复创建实例");

			ILogService.Instance = instance;
		}
	}
}