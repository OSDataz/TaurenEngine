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
	public class ActorBone : MonoBehaviour
	{
		protected void Awake()
		{
			RefreshMainBones();
		}

		#region 主骨骼
		/// <summary> 主骨骼 </summary>
		private List<Transform> _mainBones;

		public void RefreshMainBones()
		{
			_mainBones = gameObject.GetComponentsInChildren<Transform>().ToList();
		}

		private void ClearMainBones()
		{
			if (_mainBones == null)
				return;

			_mainBones.Clear();
			_mainBones = null;
		}

		private bool TryFindBone(string name, out Transform value)
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

		public void ToLogMainBones()
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
	}
}