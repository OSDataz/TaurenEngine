/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 9:16:30
 *└────────────────────────┘*/

#if UNITY_EDITOR
using System;
using System.Runtime.InteropServices;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 内存监控
	/// </summary>
	public sealed class MemoryManager : InstanceBase<MemoryManager>
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct MemoryInfo
		{
			public uint dwLength;
			public uint dwMemoryLoad;
			//系统内存总量
			public ulong dwTotalPhys;
			//系统可用内存
			public ulong dwAvailPhys;
			public ulong dwTotalPageFile;
			public ulong dwAvailPageFile;
			public ulong dwTotalVirtual;
			public ulong dwAvailVirtual;
		}

		[DllImport("kernel32")]
		public static extern void GlobalMemoryStatus(ref MemoryInfo info);

		private ITimer _timer;

		public void Start()
		{
			_timer ??= TimerHelper.Create(Update, true);
			_timer.Start();
		}

		public void Stop()
		{
			_timer?.Stop();
		}

		private void Update()
		{
			MemoryInfo info = new MemoryInfo();
			GlobalMemoryStatus(ref info);

			var mb = Convert.ToInt64(info.dwAvailPhys.ToString()) / 1024 / 1024;

			Log.Info($"剩余内存：{mb} MB");
		}
	}
}
#endif