/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 22:17:13
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Game
{
	public class ActorModCell : ActorModCellBase
	{
		internal readonly ActorModBone modBone;

		public ActorModCell(ActorX actor) : base(actor)
		{
			modBone = new ActorModBone(actor);
		}

		public override void Clear()
		{
			ClearMeshs();

			base.Clear();
		}

		#region Mesh管理
		public GameObject[] Meshs { get; private set; }

		public SkinnedMeshRenderer[] CreateMeshs()
		{
			var smrList = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);

			ClearMeshs();
			Meshs = new GameObject[smrList.Length];

			return smrList;
		}

		private void ClearMeshs()
		{
			if (Meshs != null)
			{
				for (var i = Meshs.Length - 1; i >= 0; --i)
				{
					GameObject.Destroy(Meshs[i]);
				}

				Meshs = null;
			}
		}
		#endregion

		#region 动作控制器
		public RuntimeAnimatorController RuntimeAnimatorController { get; private set; }

		private void CheckAnimatorController()
		{
			if (gameObject != null && gameObject.TryGetComponent<Animator>(out var animator))
				RuntimeAnimatorController = animator.runtimeAnimatorController;
		}

		private void ClearAnimatorController()
		{
			if (RuntimeAnimatorController == null)
				return;

			actor.animator.RemoveAnimatorController(RuntimeAnimatorController);

			RuntimeAnimatorController = null;
		}
		#endregion

		public override bool IsCombined => Meshs != null;
	}
}