/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/10 14:54:59
 *└────────────────────────┘*/

using System.Collections;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	public static class CoroutineHelper
	{
		public static Coroutine StartCoroutine(IEnumerator routine)
		{
			return ICoroutineService.Instance.StartCoroutine(routine);
		}

		public static void StopCoroutine(IEnumerator routine)
		{
			ICoroutineService.Instance.StopCoroutine(routine);
		}

		public static void StopCoroutine(Coroutine routine)
		{
			ICoroutineService.Instance.StopCoroutine(routine);
		}
	}
}