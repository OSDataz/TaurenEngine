/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/15 11:41:49
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public interface ILogService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static ILogService Instance { get; internal set; }

		/// <summary>
		/// 日志信息
		/// </summary>
		/// <param name="message"></param>
		void Info(object message);

		/// <summary>
		/// 警告信息
		/// </summary>
		/// <param name="message"></param>
		void Warn(object message);

		/// <summary>
		/// 报错信息
		/// </summary>
		/// <param name="messsage"></param>
		void Error(object messsage);
	}

	public static class ILogServiceExtension
	{
		public static void InitInterface(this ILogService @object)
		{
			ILogService.Instance = @object;
		}
	}
}