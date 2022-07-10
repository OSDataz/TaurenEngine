/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 9:22:20
 *└────────────────────────┘*/

using System.Collections.Generic;
using System.Diagnostics;
using TaurenEngine.Core;

namespace TaurenEngine.Profiling
{
	internal sealed class ProfilingCodeBlock
	{
		private List<CodeBlock> _mapBlocks = new List<CodeBlock>();

		private Stopwatch _stopwatch;

		public ProfilingCodeBlock()
		{
			_stopwatch = new Stopwatch();
			_stopwatch.Start();
		}

		public void StartCodeBlock(string code)
		{
			var data = _mapBlocks.Find(item => item.code == code);
			if (data == null)
			{
				data = new CodeBlock();
				data.code = code;
				_mapBlocks.Add(data);
			}

			data.Start(_stopwatch.Elapsed.TotalMilliseconds);

			TDebug.LogLabel("Current Run Code", data.code);
		}

		public void EndCodeBlock(string code)
		{
			var data = _mapBlocks.Find(item => item.code == code);
			if (data?.Stop(_stopwatch.Elapsed.TotalMilliseconds) ?? false)
			{
				TDebug.LogLabel(data.code, $"time:{(data.totalTime * 0.001).ToString("F3")}s ave:{data.averageTime.ToString("F2")}ms max:{data.maxTime.ToString("F2")}ms");
			}
		}
	}
}