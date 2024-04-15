/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/10 15:47:22
 *└────────────────────────┘*/

using UnityEngine.SceneManagement;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 加载场景
	/// </summary>
	public partial class LoadService
	{
		#region 同步加载
		private void LoadScene(LoadItem loadItem)
		{
			//SceneManager.LoadScene
		}
		#endregion

		#region 异步加载
		private void LoadSceneAsync(LoadItemAsync loadItem)
		{
			//SceneManager.LoadSceneAsync();
		}
		#endregion
	}
}