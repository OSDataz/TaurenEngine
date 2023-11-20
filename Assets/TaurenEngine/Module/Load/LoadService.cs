/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 20:07:21
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.ModLoad
{
	/// <summary>
	/// 加载服务
	/// </summary>
	public class LoadService : ILoadService
	{
		public LoadService()
		{
			this.InitInterface();
		}

		public ILoadData Load(string path)
		{
			return null;
		}

		public void LoadAsync<T>(string path, Action<ILoadData> onComplete) where T : UnityEngine.Object
		{
			
		}
	}
}