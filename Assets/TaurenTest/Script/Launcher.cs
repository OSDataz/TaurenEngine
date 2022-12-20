/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/20 12:24:42
 *└────────────────────────┘*/

using UnityEngine;
using UnityEngine.UI;

namespace TaurenTest
{
	public class Launcher : MonoBehaviour
	{
		public Transform tran;

		private void Start()
		{
			Debug.Log(tran.GetComponents<Image>().Length);
			Debug.Log(tran.GetComponentsInChildren<Image>().Length);
		}
	}
}