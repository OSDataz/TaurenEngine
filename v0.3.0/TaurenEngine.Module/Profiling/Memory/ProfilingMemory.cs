/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 9:16:30
 *└────────────────────────┘*/

using System;
using System.Runtime.InteropServices;
using TaurenEngine.Core;

namespace TaurenEngine.Profiling
{
	/// <summary>
	/// 内存监控
	/// https://www.cnblogs.com/Yellow0-0River/p/7551908.html
	/// </summary>
	internal sealed class ProfilingMemory
	{
#if UNITY_EDITOR
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

		private IFrameUpdate _frameUpdate;

		public void Start()
		{
			_frameUpdate ??= FrameManager.Instance.GetUpdate(Update);
			_frameUpdate.Start();
		}

		public void Stop()
		{
			_frameUpdate?.Stop();
		}

		private void Update()
		{
			MemoryInfo info = new MemoryInfo();
			GlobalMemoryStatus(ref info);

			var mb = Convert.ToInt64(info.dwAvailPhys.ToString()) / 1024 / 1024;

			TDebug.LogLabel("剩余内存：", $"{mb} MB");
		}
#endif
	}
}