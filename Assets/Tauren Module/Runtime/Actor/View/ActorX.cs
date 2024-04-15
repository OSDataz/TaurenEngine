/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 21:48:15
 *└────────────────────────┘*/

using System.Collections.Generic;
using Tauren.Core.Runtime;
using UnityEngine;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 组合对象
	/// </summary>
	public class ActorX : ActorBase
	{
		internal readonly ActorBone bone;
		internal readonly ActorSkin skin;

		public ActorX() : base()
		{
			bone = new ActorBone(this);
			skin = new ActorSkin(this);
		}

		public override void Clear()
		{
			ClearCells();

			skin.Clear();
			bone.Clear();

			base.Clear();
		}

		#region 模型根对象
		protected override void ClearRoot()
		{
			bone.Clear();

			base.ClearRoot();
		}

		protected override void OnLoadRootComplete(bool result)
		{
			base.OnLoadRootComplete(result);

			if (result)
			{
				bone.RefreshMainBones();// 初始化主骨骼
			}
		}
		#endregion

		#region 模块单元
		internal readonly List<ActorModCellBase> cells = new List<ActorModCellBase>();

		public void ClearCells()
		{
			var len = cells.Count;
			for (int i = 0; i < len; ++i)
			{
				ClearCell(cells[i]);
			}

			cells.Clear();
		}

		protected void ClearCell(ActorModCellBase cell)
		{


			cell.Clear();
		}

		protected void DestroyCell(ActorModCellBase cell)
		{
			ClearCell(cell);

			cells.Remove(cell);

			if (cell is ActorModSkinCell skinCell)
				skin.RemoveSkin(skinCell);
		}
		#endregion

		#region 加载模块
		protected void LoadCell(ActorModCellBase cell, string path)
		{
			ClearCell(cell);

			cell.Load(path, OnLoadComplete);// 如果已加载，这里会直接调用完成回调
		}

		private void OnLoadComplete(ActorModCellBase modCell)
		{
			if (modCell == null)
				return;

			if (cells.Count == 0 || !modCell.Active)
			{
				DestroyCell(modCell);
				return;
			}

			if (bone.HasMainBones)
			{
				CheckAllLoaded();
			}
		}

		private void CheckAllLoaded()
		{
			var len = cells.Count;

			if (len == 0)
				return;

			var isSuccess = true;
			for (int i = 0; i < len; ++i)
			{
				if (!cells[i].IsLoaded)
					return;

				if (!cells[i].IsLoadSuccess && cells[i].Data.IsFixed)
					isSuccess = false;
			}

			if (isSuccess)
			{
				StartCombine();
			}
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
						var skillCell = new ActorModSkinCell(this);// 创建皮肤模块
						skin.AddSkin(skillCell, data);

						cell = skillCell;
					}
					else
						cell = new ActorModCell(this);// 创建部分模块

					cells.Add(cell);// 修改

					loadList.Add((cell, data));// 添加待加载的新模块
				}
			}

			var len = loadList.Count;

			// 开始下载模块
			for (int i = 0; i < len; ++i)
			{
				LoadCell(loadList[i].cell, loadList[i].data.path);// 如果已加载，这里会直接调用完成回调
			}
		}
		#endregion

		#region 组合合并
		private void StartCombine()
		{
			var len = cells.Count;
			for (int i = 0; i < len; ++i)
			{
				var cell = cells[i];
				if (cell.Active && cell.IsLoadSuccess && !cell.IsCombined)
				{
					if (CheckMutex(cell.Data))
					{
						i = cells.IndexOf(cell);
						len = cells.Count;
					}

					if (cell is ActorModCell modCell)
						CombineCell(modCell);
					else if (cell is ActorModSkinCell skinCell)
						skinCell.SetCombined();
				}
			}

			skin.UpdateSkin();
		}

		private bool CheckMutex(ActorModuleItem data)
		{
			bool isDelete = false;

			// 删除同部位模块
			for (int i = cells.Count - 1; i >= 0; --i)
			{
				if (data.CheckMutex(cells[i].Data))
				{
					if (cells[i].Active)
						Log.Error($"异常：换装待删除同部位模块是激活状态。新模块：{data.path} 待删除模块：{cells[i].Data.path}");
					else
					{
						DestroyCell(cells[i]);
						isDelete = true;
					}
				}
			}

			return isDelete;
		}

		private void CombineCell(ActorModCell cell)
		{
			var smrList = cell.CreateMeshs();
			var meshList = cell.Meshs;
			var len = smrList.Length;

			if (len == 0)
				Log.Warn($"模型-未找到Mesh {cell.GameObject.name}");

			for (int i = 0; i < len; ++i)
			{
				if (meshList[i] == null)
				{
					var subSmr = smrList[i];

					// 创建新Mesh
					var newObj = GameObject.Instantiate(subSmr.gameObject);
					newObj.transform.SetParentOrigin(root.GameObject.transform);
					newObj.SetActive(true);

					meshList[i] = newObj;
					var newSmr = newObj.GetComponent<SkinnedMeshRenderer>();

					// 重新绑定骨骼 - 找骨骼
					var oldBones = subSmr.bones;
					var oldLen = oldBones.Length;
					var newBones = new Transform[oldLen];

					for (int oi = 0; oi < oldLen; ++oi)// 原始部位骨骼遍历
					{
						var oldBone = oldBones[oi];

						// 找到对应的主骨骼
						if (bone.TryFindBone(oldBone.name, out var mainBone))
						{
							newBones[oi] = mainBone;
						}
						else
						{
							bone.AddExDyColliders(cell, smrList);// 添加额外的组件
							bone.AddExBones(cell, smrList);// 添加额外骨骼

							if (bone.TryFindBone(oldBone.name, out var mainBone2))
							{
								newBones[oi] = mainBone2;
							}
							else
							{
								Log.Error($"添加额外骨骼后仍没找到对应骨骼。AssetId：{cell.Data.path} Bone：{oldBone.name}");
							}
						}
					}

					// 重新绑定骨骼 - 绑定骨骼
					newSmr.bones = newBones;

					if (bone.TryFindBone(subSmr.rootBone.name, out var exValue))
						newSmr.rootBone = exValue;

					// 动态骨骼
					var newDyBones = newObj.GetComponents<DynamicBone>();
					var ndbLen = newDyBones?.Length;
					if (ndbLen > 0)
					{
						for (int dbi = 0; dbi < ndbLen; ++dbi)
						{
							var dyBone = newDyBones[dbi];

							// 设置根骨骼
							if (dyBone.m_Root != null && bone.TryFindBone(dyBone.m_Root.name, out var dyValue))
								dyBone.m_Root = dyValue;

							// 设置碰撞体
							var cLen = dyBone.m_Colliders?.Count;
							if (cLen > 0)
							{
								for (int ci = 0; ci < cLen; ++ci)
								{
									dyBone.m_Colliders[ci] = cell.modBone.GetExComponent<DynamicBoneColliderBase>(dyBone.m_Colliders[ci]?.gameObject.name);
								}
							}
						}
					}
				}
			}
		}
		#endregion
	}
}