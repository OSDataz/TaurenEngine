/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 22:17:02
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Game
{
	public class ActorModSkinCell : ActorModCellBase
	{
		public ActorModSkinCell(ActorX actor) : base(actor) { }

		#region 资源加载 - 材质
		public Material Material { get; private set; }
		#endregion

		#region 合并
		private bool _isCombined;

		public void SetCombined()
		{
			_isCombined = true;
		}

		public override bool IsCombined => _isCombined;
		#endregion
	}
}