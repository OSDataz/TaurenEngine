/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/10 20:11:56
 *└────────────────────────┘*/

using ILRuntime.Mono.Cecil.Pdb;
using ILRuntime.Runtime.Enviorment;
using System.IO;
using UnityEngine;

namespace TaurenEngine.Framework
{
	public class ILRuntimeRun : ILRuntimeRegister
	{
		/// <summary> ILRuntime程序域 </summary>
		public static AppDomain AppDomain { get; private set; }

		public void RunHotfixDll(ref byte[] dllBytes, ref byte[] pdbBytes,
			string typeFullName, string methodName, System.Action registAction, params object[] paramList)
		{
			if (AppDomain != null)
				Debug.LogError($"ILRuntime执行了两个程序集，设计上只能执行一个");

			AppDomain = new AppDomain();
			var dllStream = new MemoryStream(dllBytes);
			var pdbStream = new MemoryStream(pdbBytes);
			AppDomain.LoadAssembly(dllStream, pdbStream, new PdbReaderProvider());
			var method = new ILRuntimeStaticMethod(AppDomain, typeFullName, methodName, paramList.Length);

#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
			//由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
			AppDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif

			registAction?.Invoke();
			// 这换代码需要写在回调，先注释在这里备注下
			//ILRuntime.Runtime.Generated.CLRBindings.Initialize(appDomain);

			InitAdaptation(AppDomain);

			method.Run(paramList);
		}
	}
}