/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 20:48:58
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.Game
{
	public class ActorModule
	{
		public void Clear()
		{
			ClearCells();
		}

		#region 模块单元
		protected readonly List<ActorModCellBase> cells = new List<ActorModCellBase>();

		public void ClearCells()
		{
			var len = cells.Count;
			for (int i = 0; i < len; ++i)
			{
				cells[i].Clear();
			}

			cells.Clear();
		}

		protected void DestroyCell(ActorModCellBase cell)
		{
			cell.Clear();
			cells.Remove(cell);

			if (cell is ActorModSkinCell skinCell)
				skin.RemoveCell(skinCell);
		}
		#endregion

		#region 模块更新
		public void Update(ActorModuleItem[] list)
		{
			// 注1：必须先删除老模块，再加新模板，不然主骨骼上额外骨骼数据会出错。原因：额外骨骼名可能会一样。
			// 注2：同部位模型替换，需要加载完成后再删除老部位。否则会出现部位闪烁情况。
			// 总结：先只添加新模块，新模块加载完成后再检测删除同部位模块

			// 比较列表
			var cLen = list.Length;
			for (int oi = cells.Count - 1; oi >= 0; --oi)
			{
				var cell = cells[oi];
				cell.Active = false;

				for (int ci = 0; ci < cLen; ++ci)
				{
					if (cell.Data == list[ci])
					{
						cell.Active = true;
						break;
					}
				}

				if (!cell.Active && !cell.Data.IsFixed)
					DestroyCell(cell);// 删除移除的非固定的模块
			}

			// 检查新增部分
			var loadList = new List<(ActorModCellBase cell, ActorModuleItem data)>();
			for (int i = 0; i < cLen; ++i)
			{
				var data = list[i];
				var cell = cells.Find(item => item.Data == data);
				if (cell == null)
				{
					if (data.IsSkin)
					{
						var skillCell = new ActorModSkinCell();
						skin.AddCell(skillCell, data);

						cell = skillCell;
					}
					else
						cell = new ActorModCell();

					cells.Add(cell);// 修改

					loadList.Add((cell, data));// 添加待加载的新模块
				}
			}

			var len = loadList.Count;

			// 开始下载模块
			for (int i = 0; i < len; ++i)
			{
				//loadList[i].cell.Load(loadList[i].data.path, OnLoadComplete);// 如果已加载，这里会直接调用完成回调
			}
		}

		//protected void OnLoadComplete(ActorObject actor)
		//{
		//	if (actor == null)
		//		return;

		//	if (!IsInit || !actor.Active)
		//	{
		//		DestroyActor(actor);
		//		return;
		//	}

		//	//if (actor.GameObject == null)
		//	//	return;// 资源加载失败

		//	//if (ComType != ActorComType.Simple)
		//	//    actor.HideMesh();// 加载完成

		//	if (root != null && root.Loaded)
		//	{
		//		CheckAllLoaded(true);
		//	}
		//}
		#endregion

		#region 皮肤模块
		public readonly ActorSkin skin = new ActorSkin();


		#endregion
	}
}