/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/28 9:22:20
 *└────────────────────────┘*/

using System.Collections.Generic;
using System.Diagnostics;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public sealed class CodeBlockManager : InstanceBase<CodeBlockManager>
	{
		private List<CodeBlock> _mapBlocks = new List<CodeBlock>();

		private Stopwatch _stopwatch;

		public void Startup()
		{
			if (_stopwatch == null)
				_stopwatch = new Stopwatch();

			if (!_stopwatch.IsRunning)
				_stopwatch.Start();
		}

		public void Stop()
		{
			if (_stopwatch == null)
				return;

			if (_stopwatch.IsRunning)
				_stopwatch.Stop();
		}

		public void Start(string code)
		{
			var data = _mapBlocks.Find(item => item.tag == code);
			if (data == null)
			{
				data = new CodeBlock();
				data.tag = code;
				_mapBlocks.Add(data);
			}

			data.Start(_stopwatch.Elapsed.TotalMilliseconds);
		}

		public void End(string code)
		{
			var data = _mapBlocks.Find(item => item.tag == code);
			if (data?.End(_stopwatch.Elapsed.TotalMilliseconds) ?? false)
			{
				Log.Info($"Tag:{data.tag} time:{(data.TotalTime * 0.001).ToString("F3")}s ave:{data.AverageTime.ToString("F2")}ms max:{data.MaxTime.ToString("F2")}ms");
			}
		}
	}
}