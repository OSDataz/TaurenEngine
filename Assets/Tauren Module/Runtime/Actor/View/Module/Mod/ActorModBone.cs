/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/12/1 14:40:24
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 单个模块骨骼
	/// </summary>
	public class ActorModBone
	{
		protected ActorX actor;

		public ActorModBone(ActorX actor)
		{
			this.actor = actor;
		}

		public virtual void Clear()
		{
			ClearExComponents();
			ClearExBones();
		}

		#region 额外骨骼
		/// <summary>
		/// 额外骨骼
		/// </summary>
		public List<(GameObject rootBone, Transform[] bones)> ExBones { get; private set; }

		public void AddExBones(GameObject rootBone, Transform[] bones)
		{
			if (ExBones == null)
				ExBones = new List<(GameObject rootBone, Transform[] bones)>();

			ExBones.Add((rootBone, bones));
		}

		/// <summary>
		/// 清理动态骨骼
		/// </summary>
		private void ClearExBones()
		{
			if (ExBones == null)
				return;

			var len = ExBones.Count;
			for (int i = 0; i < len; ++i)
			{
				actor.bone.RemoveExBones(ExBones[i].rootBone, ExBones[i].bones);
			}

			ExBones.Clear();
			ExBones = null;
		}
		#endregion

		#region 额外组件 - 动态碰撞体
		private List<Component> _exComponents;

		public void AddExComponent(Component component)
		{
			if (_exComponents == null)
				_exComponents = new List<Component>();

			_exComponents.Add(component);
		}

		public T GetExComponent<T>(string objectName) where T : Component
		{
			if (string.IsNullOrEmpty(objectName))
				return default(T);

			var len = _exComponents.Count;
			for (int i = 0; i < len; ++i)
			{
				if (_exComponents[i].gameObject.name == objectName && _exComponents[i] is T tResult)
					return tResult;
			}

			return default(T);
		}

		private void ClearExComponents()
		{
			if (_exComponents == null)
				return;

			var len = _exComponents.Count;
			for (int i = 0; i < len; ++i)
			{
				GameObject.Destroy(_exComponents[i]);
			}

			_exComponents.Clear();
			_exComponents = null;
		}
		#endregion
	}
}