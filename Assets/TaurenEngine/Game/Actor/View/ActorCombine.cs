/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 21:48:15
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.Game
{
	/// <summary>
	/// 组合对象
	/// </summary>
	public class ActorCombine : ActorBase
	{
		public override void Clear()
		{


			base.Clear();
		}

		#region 模型根对象
		protected override void ClearRoot()
		{
			ClearBone();

			base.ClearRoot();
		}

		protected override void OnLoadRootComplete(bool result)
		{
			base.OnLoadRootComplete(result);

			if (result)
			{
				InitBone();
			}
		}
		#endregion

		#region 骨骼
		internal ActorBone bone;

		protected void InitBone()
		{
			bone = root.gameObject.GetOrAddComponent<ActorBone>();
		}

		protected void ClearBone()
		{
			bone = null;
		}
		#endregion

		#region 模块组合
		internal readonly ActorModule module = new ActorModule();

		protected void RefreshModule()
		{

		}

		protected void ClearModule()
		{

		}
		#endregion
	}
}