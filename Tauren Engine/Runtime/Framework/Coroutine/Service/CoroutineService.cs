/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/10 14:36:10
 *└────────────────────────┘*/

using System.Collections;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 协程服务
	/// </summary>
	public class CoroutineService : ICoroutineService
	{
		private MonoBehaviour _monoBehaviour;

		public CoroutineService(MonoBehaviour component) 
		{
			this.InitInterface();

			_monoBehaviour = component;
		}

		public Coroutine StartCoroutine(IEnumerator routine)
		{
			return _monoBehaviour.StartCoroutine(routine);
		}

		public void StopCoroutine(IEnumerator routine) 
		{
			_monoBehaviour.StopCoroutine(routine); 
		}

		public void StopCoroutine(Coroutine routine)
		{
			_monoBehaviour.StopCoroutine(routine);
		}
	}
}