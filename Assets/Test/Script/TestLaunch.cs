/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/2 14:21:59
 *└────────────────────────┘*/

using System.IO;
using System.Reflection;
using TaurenEngine.Framework;
using UnityEngine;

namespace Test
{
	public class TestLaunch : MonoBehaviour
	{
		public bool useILRuntime;

		private string hotfixDllPath = @"Test.Hotfix.dll";
		private string hotfixPdbPath = @"Test.Hotfix.pdb";
		private string hotfixClass = "Test.Hotfix.HotfixMain";
		private string hotfixMethod = "Start";

		private void Start()
		{
			LoadHotfixCodeFile();
		}

		private void LoadHotfixCodeFile()
		{
			var hotfixDll = TaurenFramework.Resource.Load<TextAsset>(hotfixDllPath, LoadType.Resources);
			var hotfixPdb = TaurenFramework.Resource.Load<TextAsset>(hotfixPdbPath, LoadType.Resources);

			if (hotfixDll == null || hotfixPdb == null)
			{
				Debug.LogError("热更代码加载失败");
				return;
			}

			StaticMethod startMethod;
			if (useILRuntime)
			{
				Debug.Log($"当前使用的是ILRuntime模式");
				var appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();
				var dllStream = new MemoryStream(hotfixDll.bytes);
				var pdbStream = new MemoryStream(hotfixPdb.bytes);
				appDomain.LoadAssembly(dllStream, pdbStream, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
				startMethod = new ILRuntimeStaticMethod(appDomain, hotfixClass, hotfixMethod, 0);

#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
				//由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
				appDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif

				ILRuntime.Runtime.Generated.CLRBindings.Initialize(appDomain);

				new ILRuntimeRegister().Init(appDomain);

				//appDomain.Invoke(hotfixClass, hotfixMethod, null, null);
				//return;
			}
			else
			{
				Debug.Log($"当前使用的是Mono模式");
				var assembly = Assembly.Load(hotfixDll.bytes, hotfixPdb.bytes);
				var hotfixInit = assembly.GetType(hotfixClass);
				startMethod = new MonoStaticMethod(hotfixInit, hotfixMethod);
			}

			startMethod.Run();
		}
	}
}