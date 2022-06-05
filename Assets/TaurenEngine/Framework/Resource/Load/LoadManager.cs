/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/6 23:54:27
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 加载管理器
	/// </summary>
	internal class LoadManager
	{
		public T Load<T>(LoadTask<T> loadTask)
		{
			loadTask.isAsync = false;


			return default;
		}

		public void LoadAsync<T>(LoadTask<T> loadTask)
		{
			loadTask.isAsync = true;
		}

		public bool Unload(uint id)
		{
			return false;
		}
	}
}