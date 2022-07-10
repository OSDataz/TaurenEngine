/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/19 20:20:46
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public static class Asset
	{
		#region 加载
		private static PlayerPrefsLoader _playerPrefs;
		/// <summary>
		/// 本地存储
		/// </summary>
		public static PlayerPrefsLoader PlayerPrefs
			=> _playerPrefs ??= new PlayerPrefsLoader();

		private static FileLoader _fileLoader;
		/// <summary>
		/// 文件系统
		/// </summary>
		public static FileLoader FileLoader
			=> _fileLoader ??= new FileLoader();

		private static ResourcesLoader _resourcesLoader;
		/// <summary>
		/// 本地资源加载
		/// </summary>
		public static ResourcesLoader Resources
			=> _resourcesLoader ??= new ResourcesLoader();
		#endregion

		#region 设置
		


		#endregion
	}
}