/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 9:23:44
 *└────────────────────────┘*/

namespace TaurenEngine.Profiling
{
	internal class CodeBlock
	{
		public string code;

		/// <summary>
		/// 总计消耗（毫秒）
		/// </summary>
		public double totalTime;
		/// <summary>
		/// 平均消耗时间（毫秒）
		/// </summary>
		public double averageTime;
		/// <summary>
		/// 总计执行次数
		/// </summary>
		public int totalCount;
		/// <summary>
		/// 最大消耗时间（毫秒）
		/// </summary>
		public double maxTime;

		/// <summary>
		/// 开始执行记录时间（毫秒）
		/// </summary>
		private double _startTime;

		public void Start(double ms)
		{
			_startTime = ms;
		}

		public bool Stop(double ms)
		{
			if (_startTime <= 0.0f)
				return false;

			_startTime = ms - _startTime;

			totalTime += _startTime;
			totalCount += 1;
			averageTime = totalTime / totalCount;

			if (_startTime > maxTime)
				maxTime = _startTime;

			_startTime = 0.0f;
			return true;
		}
	}
}