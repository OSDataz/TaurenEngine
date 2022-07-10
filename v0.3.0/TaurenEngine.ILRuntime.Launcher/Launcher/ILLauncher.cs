/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/10/6 15:47:43
 *└────────────────────────┘*/

using ILRuntime.Mono.Cecil.Pdb;
using ILRuntime.Runtime.Enviorment;
using System.IO;
using System.Reflection;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.ILRuntime
{
	public class ILLauncher : MonoBehaviour
	{
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{

		}

		[SerializeField]
		private bool useMono;

		[SerializeField]
		private string startType;
		[SerializeField]
		private string startMethod;

		private IStaticMethod _staticMethod;

		private void Start()
		{
			LoadHotfixAssembly();
		}

		private void OnApplicationQuit()
		{
			
		}

		private void LoadHotfixAssembly()
		{
			var dllBytes = LoadHotfixCodeFile();
			var pbdBytes = LoadHotfixCodeFile();

			if (useMono)
			{
				TDebug.Log("Mono模式运行");
				var assembly = Assembly.Load(dllBytes, pbdBytes);
				var hotfixType = assembly.GetType(startType);
				_staticMethod = new MonoStaticMethod(hotfixType, startMethod);
			}
			else
			{
				TDebug.Log("ILRuntime模式运行");
				var appDomain = new AppDomain();
				var dllStream = new MemoryStream(dllBytes);
				var pdbStream = new MemoryStream(pbdBytes);
				appDomain.LoadAssembly(dllStream, pdbStream, new PdbReaderProvider());
				_staticMethod = new ILRuntimeStaticMethod(appDomain, startType, startMethod, 0);
			}

			_staticMethod.Run();
		}

		private byte[] LoadHotfixCodeFile()
		{
			return null;
		}
	}
}