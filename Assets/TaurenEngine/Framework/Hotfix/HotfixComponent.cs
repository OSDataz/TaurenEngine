/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/9 15:29:54
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 热更新模块
	/// </summary>
	public class HotfixComponent : OnceComponent
	{
		[SerializeField]
		private HotfixType _hotfixType;

		protected override void Awake()
		{
			base.Awake();

			if (TaurenFramework.Hotfix == null)
			{
				var hotfix = new HotfixManager();
				hotfix.hotfixType = _hotfixType;
				TaurenFramework.Hotfix = hotfix;
			}
		}
	}
}