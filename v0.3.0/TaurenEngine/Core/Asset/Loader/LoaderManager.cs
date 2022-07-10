/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/21 13:49:38
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.Core
{
	internal class LoaderManager
	{
		private readonly Dictionary<int, Loader> _loaderMap = new Dictionary<int, Loader>();

		/// <summary>
		/// 注册加载器
		/// </summary>
		/// <param name="loader"></param>
		public void RegisterLoader(Loader loader)
		{
			if (loader == null)
				return;

			_loaderMap.Set(loader.Type, loader);
		}

		/// <summary>
		/// 获取加载器
		/// </summary>
		/// <param name="loadType"></param>
		/// <returns></returns>
		public Loader GetLoader(int loadType)
			=> _loaderMap.TryGetValue(loadType, out var loader) ? loader : null;
	}
}