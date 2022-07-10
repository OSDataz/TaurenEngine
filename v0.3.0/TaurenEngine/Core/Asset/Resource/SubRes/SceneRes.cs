/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/22 10:44:06
 *└────────────────────────┘*/

using UnityEngine.SceneManagement;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 场景资源
	/// </summary>
	internal class SceneRes : LoadRes<Scene>
	{
		public override void Release()
		{

		}
	}
}