/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 22:21:32
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Game
{
	/// <summary>
	/// 皮肤模块
	/// </summary>
	public class ActorSkin
	{
		private class Skin
		{
			/// <summary>
			/// Mesh名字
			/// </summary>
			public string meshName;
			/// <summary>
			/// 皮肤模块
			/// </summary>
			public ActorModSkinCell actorSkin;
			/// <summary>
			/// 默认材质
			/// </summary>
			public Material originMaterial;
		}

		private ActorX _actor;
		private List<Skin> _skins;

		public ActorSkin(ActorX actor)
		{
			_actor = actor;
		}

		public void Clear()
		{
			ClearSkin();
		}

		internal void UpdateSkin()
		{
			if (_skins == null)
				return;

			for (int i = _skins.Count - 1; i >= 0; --i)
			{
				var skin = _skins[i];
				if (skin.actorSkin != null)
				{
					// 换皮肤
					for (int j = _actor.cells.Count - 1; j >= 0; --j)
					{
						if (_actor.cells[j] is ActorModCell modCell)
						{
							for (int k = modCell.Meshs.Length - 1; k >= 0; --k)
							{
								if (modCell.Meshs[k].name == skin.meshName)
								{
									var skinMesh = modCell.Meshs[k].GetComponent<SkinnedMeshRenderer>();

									if (skin.originMaterial == null)
										skin.originMaterial = skinMesh.material;// 逻辑上，第一次的材质就是默认材质

									skinMesh.material = skin.actorSkin.Material;// 更新新材质
								}
							}
						}
					}
				}
				else
				{
					// 还原皮肤
					for (int j = _actor.cells.Count - 1; j >= 0; --j)
					{
						if (_actor.cells[j] is ActorModCell modCell)
						{
							for (int k = modCell.Meshs.Length - 1; k >= 0; --k)
							{
								if (modCell.Meshs[k].name == skin.meshName)
								{
									var skinMesh = modCell.Meshs[k].GetComponent<SkinnedMeshRenderer>();
									skinMesh.material = skin.originMaterial;// 还原默认材质
								}
							}
						}
					}

					_skins.RemoveAt(i);
				}
			}
		}

		public void AddSkin(ActorModSkinCell cell, ActorModuleItem data)
		{
			if (_skins == null)
				_skins = new List<Skin>();

			var len = data.Parts.Count;
			for (int i = 0; i < len; ++i)
			{
				AddSkin(cell, data.Parts[i].SkinMesh);
			}
		}

		private void AddSkin(ActorModSkinCell cell, string meshName)
		{
			var skin = _skins.Find(item => item.meshName == meshName);
			if (skin == null)
			{
				skin = new Skin();
				skin.meshName = meshName;
				_skins.Add(skin);
			}

			skin.actorSkin = cell;
		}

		public void RemoveSkin(ActorModSkinCell cell)
		{
			if (_skins == null)
				return;

			for (int i = _skins.Count - 1; i >= 0; --i)
			{
				var skin = _skins[i];
				if (skin.actorSkin == cell)
				{
					if (skin.originMaterial != null)
					{
						skin.actorSkin = null;
					}
					else
					{
						_skins.RemoveAt(i);
					}
				}
			}
		}

		private void ClearSkin()
		{
			if (_skins == null)
				return;

			_skins.Clear();
			_skins = null;
		}
	}
}