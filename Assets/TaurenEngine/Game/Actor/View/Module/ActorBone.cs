/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 20:47:30
 *└────────────────────────┘*/

using System.Collections.Generic;
using System.Linq;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.Game
{
	public class ActorBone
	{
		protected readonly ActorX actor;

		public ActorBone(ActorX actor)
		{
			this.actor = actor;
		}

		public void Clear()
		{
			ClearMainBones();
		}

		#region 主骨骼
		/// <summary> 主骨骼 </summary>
		private List<Transform> _mainBones;

		/// <summary> 有主骨骼 </summary>
		public bool HasMainBones => _mainBones != null;

		public void RefreshMainBones()
		{
			_mainBones = actor.root.gameObject.GetComponentsInChildren<Transform>().ToList();
		}

		private void ClearMainBones()
		{
			if (_mainBones == null)
				return;

			_mainBones.Clear();
			_mainBones = null;
		}

		public bool TryFindBone(string name, out Transform value)
		{
			var len = _mainBones.Count;
			for (var i = 0; i < len; ++i)
			{
				if (_mainBones[i].name == name)
				{
					value = _mainBones[i];
					return true;
				}
			}

			value = null;
			return false;
		}

		/// <summary>
		/// 在主骨骼中查询骨骼
		/// </summary>
		/// <param name="name"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		private bool HasBone(string name, ref int length)
		{
			for (var i = 0; i < length; ++i)
			{
				if (_mainBones[i].name == name)
					return true;
			}

			return false;
		}

		public void ToLog()
		{
			if (_mainBones == null)
			{
				Log.Print("模型：无根骨骼");
				return;
			}

			var len = _mainBones.Count;
			if (len == 0)
			{
				Log.Print("模型：根骨骼为空");
				return;
			}

			for (int i = 0; i < len; ++i)
			{
				Log.Print($"模型：骨骼 {_mainBones[i].name}");
			}
		}
		#endregion

		#region 额外骨骼
		/// <summary>
		/// 添加额外骨骼
		/// </summary>
		/// <param name="originBone">原模型上的额外根骨骼</param>
		/// <param name="exRootBone">新的额外根骨骼（和originBone是相同的，但实例不同）</param>
		/// <param name="exBones">额外骨骼列表</param>
		/// <returns></returns>
		private bool AddExBones(Transform originBone, Transform exRootBone, Transform[] exBones)
		{
			if (!TryFindBone(originBone.parent.name, out var parentBone))
			{
				Log.Error($"主骨骼添加额外骨骼，未找到父骨骼。根骨骼：{exRootBone.name} 父骨骼：{originBone.parent.name}");

				//MainBonesToLog();
				return false;
			}

			exRootBone.SetParent(parentBone);
			exRootBone.localPosition = originBone.localPosition;
			exRootBone.localRotation = originBone.localRotation;
			exRootBone.localScale = originBone.localScale;

			_mainBones.AddRange(exBones);
			return true;
		}

		public void RemoveExBones(GameObject rootBone, Transform[] bones)
		{
			if (_mainBones != null)
			{
				var len = bones.Length;
				for (int i = 0; i < len; ++i)
				{
					//Log.Print($"移除骨骼:{bones[i].name}");
					_mainBones.Remove(bones[i]);
				}
			}

			//Log.Print($"移除主骨骼:{rootBone.name}");
			rootBone.transform.SetParent(null);
			GameObject.Destroy(rootBone);
		}
		#endregion

		#region 额外骨骼
		/// <summary>
		/// 添加额外的骨骼
		/// </summary>
		/// <param name="meshs"></param>
		public void AddExBones(ActorModCell cell, SkinnedMeshRenderer[] meshs)
		{
			//Log.Print("模型-添加额外骨骼");

			var exBones = new List<Transform>();
			var mainLen = _mainBones.Count;

			// 找到所有的额外骨骼
			var len = meshs.Length;
			for (int i = 0; i < len; ++i)
			{
				// 遍历mesh
				var bones = meshs[i].bones;
				var bLen = bones.Length;

				for (int bi = 0; bi < bLen; ++bi)
				{
					// 遍历骨骼
					var bone = bones[bi];

					//Log.Print($"模型-Mesh骨骼：{bone.name}");

					if (!HasBone(bone.name, ref mainLen))
					{
						var eLen = exBones.Count;
						bool isFind = false;
						for (int ei = 0; ei < eLen; ++ei)// 检查重复添加
						{
							if (exBones[ei].name == bone.name)
							{
								isFind = true;
								break;
							}
						}

						if (!isFind)
							exBones.Add(bone);
					}
				}
			}

			// 查询根骨骼
			len = exBones.Count;

			//for (int i = 0; i < len; ++i)
			//{
			//	Log.Print($"模型-查询的额外骨骼：{exBones[i].name}");
			//}

			bool isRoot;
			for (int i = 0; i < len; ++i)
			{
				var bone = exBones[i];
				isRoot = true;

				for (int j = 0; j < len; ++j)
				{
					if (bone.parent == exBones[j])
					{
						// 子节点
						//Log.Print($"模型-子额外骨骼：{bone.name} 父额外骨骼：{bone.parent.name}");
						isRoot = false;
						break;
					}
				}

				if (isRoot)
				{
					// 找到根骨骼
					var newBone = actor.Instantiate(bone.gameObject);
					newBone.name = bone.gameObject.name;
					var newBones = newBone.GetComponentsInChildren<Transform>(true);

					if (AddExBones(bone, newBone.transform, newBones))// 主骨骼添加额外骨骼
					{
						cell.modBone.AddExBones(newBone, newBones);// 部位记录额外骨骼

						actor.animator.ResetAnimatorController();
					}
					else
					{
						GameObject.Destroy(newBone);
					}
				}
			}
		}

		/// <summary>
		/// 添加额外的动态碰撞体
		/// </summary>
		/// <param name="actor"></param>
		/// <param name="meshs"></param>
		public void AddExDyColliders(ActorModCell cell, SkinnedMeshRenderer[] meshs)
		{
			List<(DynamicBoneColliderBase collider, Transform bone)> exComponents = null;
			var mLen = meshs.Length;
			for (int i = 0; i < mLen; ++i)
			{
				var dyBones = meshs[i].GetComponents<DynamicBone>();
				var bLen = dyBones?.Length;
				if (bLen > 0)
				{
					for (int bi = 0; bi < bLen; ++bi)
					{
						var dyBone = dyBones[bi];
						var cLen = dyBone.m_Colliders?.Count;
						if (cLen > 0)
						{
							if (exComponents == null)
								exComponents = new List<(DynamicBoneColliderBase collider, Transform bone)>();

							for (int ci = 0; ci < cLen; ++ci)
							{
								var collider = dyBone.m_Colliders[ci];
								if (collider == null)
								{
									Log.Error($"模型{actor.container.gameObject.name}-骨骼{dyBone.name} 碰撞体列表未赋值");
									continue;
								}

								var eLen = exComponents.Count;
								for (int ei = 0; ei < eLen; ++ei)
								{
									if (exComponents[ei].collider == collider)
									{
										eLen = -1;
										break;
									}
								}

								if (eLen != -1 && TryFindBone(collider.gameObject.name, out var bone))
								{
									exComponents.Add((collider, bone));
								}
							}
						}
					}
				}
			}

			if (exComponents == null)
				return;// 没有额外组件

			var len = exComponents.Count;
			for (int i = 0; i < len; ++i)
			{
				var newComponent = exComponents[i].bone.gameObject.AddComponent(exComponents[i].collider.GetType());
				cell.modBone.AddExComponent(newComponent);

				// 克隆组件值
				if (newComponent is DynamicBoneCollider newCollider && exComponents[i].collider is DynamicBoneCollider oldCollider)
				{
					newCollider.m_Direction = oldCollider.m_Direction;
					newCollider.m_Center = oldCollider.m_Center;
					newCollider.m_Bound = oldCollider.m_Bound;
					newCollider.m_Radius = oldCollider.m_Radius;
					newCollider.m_Height = oldCollider.m_Height;
				}
				else
				{
					Log.Warn($"克隆额外组件，新类型待处理。元组件{exComponents[i].collider} 克隆组件{newComponent}");

					ObjectUtils.CopyValue(exComponents[i].collider, newComponent);
				}
			}
		}
		#endregion
	}
}