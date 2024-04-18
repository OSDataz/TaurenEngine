/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/28 9:23:44
 *└────────────────────────┘*/

namespace Tauren.Framework.Runtime
{
	internal class CodeBlock
	{
		public string tag;

		/// <summary>
		/// 总计消耗（毫秒）
		/// </summary>
		public double TotalTime { get; private set; }
		/// <summary>
		/// 平均消耗时间（毫秒）
		/// </summary>
		public double AverageTime { get; private set; }
		/// <summary>
		/// 总计执行次数
		/// </summary>
		public int TotalCount { get; private set; }
		/// <summary>
		/// 最大消耗时间（毫秒）
		/// </summary>
		public double MaxTime { get; private set; }

		/// <summary>
		/// 开始执行记录时间（毫秒）
		/// </summary>
		private double _startTime;

		public void Start(double ms)
		{
			_startTime = ms;
		}

		public bool End(double ms)
		{
			if (_startTime <= 0.0f)
				return false;

			_startTime = ms - _startTime;

			TotalTime += _startTime;
			TotalCount += 1;
			AverageTime = TotalTime / TotalCount;

			if (_startTime > MaxTime)
				MaxTime = _startTime;

			_startTime = 0.0f;
			return true;
		}
	}
}