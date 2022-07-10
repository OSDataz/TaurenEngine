/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 9:10:01
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.Profiling
{
	/// <summary>
	/// 性能测试管理器
	/// </summary>
	public sealed class ProfilingManager : Singleton<ProfilingManager>
	{
		#region 代码块耗时检测
		private ProfilingCodeBlock _codeBlock;

		/// <summary>
		/// 开始记录代码模块
		/// </summary>
		/// <param name="code"></param>
		public void StartCodeBlock(string code) => (_codeBlock ??= new ProfilingCodeBlock()).StartCodeBlock(code);

		/// <summary>
		/// 停止记录代码模块
		/// </summary>
		/// <param name="code"></param>
		public void EndCodeBlock(string code) => _codeBlock?.EndCodeBlock(code);
		#endregion

		#region 内存监控
		private ProfilingMemory _memory;

#if UNITY_EDITOR
		/// <summary>
		/// 开始内存监测
		/// </summary>
		public void StartMemoryDebug() => (_memory ??= new ProfilingMemory()).Start();

		/// <summary>
		/// 停止内存监测
		/// </summary>
		public void StopMemoryDebug() => _memory?.Stop();
#endif
		#endregion
	}
}