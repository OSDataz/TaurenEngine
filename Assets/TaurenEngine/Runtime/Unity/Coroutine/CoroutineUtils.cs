/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/3 20:07:02
 *└────────────────────────┘*/

using System.Collections;
using UnityEngine;

namespace TaurenEngine.Runtime.Unity
{
	public class CoroutineUtils
	{
		#region 静态接口
		/// <summary>
		/// 等待指定秒数
		/// </summary>
		/// <param name="seconds"></param>
		/// <returns></returns>
		public static IEnumerator WaitForSeconds(float seconds)
		{
			seconds += Time.time;

			while (seconds < Time.time)
				yield return null;
		}

		/// <summary>
		/// 等待指定帧数
		/// </summary>
		/// <param name="frame"></param>
		/// <returns></returns>
		public static IEnumerator WaitForFrame(uint frame)
		{
			while (frame-- > 0)
				yield return null;
		}
		#endregion
	}
}