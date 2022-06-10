/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/10 19:55:37
 *└────────────────────────┘*/

using System;
using System.Reflection;

namespace TaurenEngine.Framework
{
	public enum HotfixType
	{
		None,
		ILRuntime,
		Huatuo
	}

	public class HotfixManager
	{
		/// <summary>
		/// 是否使用热更
		/// </summary>
		public HotfixType hotfixType;

		public void RunHotfixDll(ref byte[] dllBytes, ref byte[] pdbBytes, 
			string typeFullName, string methodName, Action registAction, params object[] paramList)
		{
			if (hotfixType == HotfixType.ILRuntime)
			{
				new ILRuntimeRun().RunHotfixDll(
					ref dllBytes, ref pdbBytes, typeFullName, methodName, registAction, paramList);
			}
			else
			{
				var assembly = Assembly.Load(dllBytes, pdbBytes);
				registAction?.Invoke();
				new DefaultStaticMethod(assembly, typeFullName, methodName).Run(paramList);
			}
		}
	}
}