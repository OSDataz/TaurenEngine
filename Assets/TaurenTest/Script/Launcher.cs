/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/20 12:24:42
 *└────────────────────────┘*/

using System.IO;
using TaurenEngine;
using UnityEngine;
using UnityEngine.UI;

namespace TaurenTest
{
	public class Launcher : MonoBehaviour
	{
		public Transform tran;

		private void Start()
		{

			var path = "A/B/cED.Tsf";

			Debug.Log(path);
			var pe = Path.GetExtension(path);
			Debug.Log(pe);

			var ext = ".Tsf";
			Debug.Log(Path.HasExtension(ext));
			Debug.Log(PathEx.EqualExtension(pe, ext));
		}
	}
}