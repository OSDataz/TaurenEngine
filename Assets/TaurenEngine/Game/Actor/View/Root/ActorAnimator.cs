/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 20:47:37
 *└────────────────────────┘*/

using System;
using System.Collections;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.Game
{
	public class ActorAnimator
	{
		protected ActorBase actor;

		protected Animator animator;
		protected string animationName;
		protected string defaultAnimationName;

		private int _playWeight = -2;

		private const float crossFadeTimeNormalized = 0.1f;
		private const int animLayer = 0;

		public ActorAnimator(ActorBase actor)
		{
			this.actor = actor;
		}

		public void Clear()
		{
			ClearAnimator();
		}

		/// <summary>
		/// 设置默认动画
		/// </summary>
		/// <param name="animationName"></param>
		/// <param name="crossFade"></param>
		/// <returns></returns>
		public bool SetDefaultAnimation(string animationName, bool crossFade = true)
		{
			defaultAnimationName = animationName;

			if (_playWeight > -1)
				return false;

			return Play(animationName, crossFade);
		}

		/// <summary>
		/// 播放动画
		/// </summary>
		/// <param name="animationName"></param>
		/// <param name="weight">播放权重，-1是默认动画。其他可设置值 >= 0</param>
		/// <param name="crossFade"></param>
		public void Play(string animationName, int weight, bool crossFade = true)
		{
			if (weight < _playWeight)
				return;

			_playWeight = weight;

			if (this.animationName == animationName)
				return;

			actor.container.behaviour.StartCoroutine(Play(animationName, crossFade, (success) =>
			{
				if (success)
				{
					_playWeight = -1;
					Play(defaultAnimationName, crossFade);
				}
			}));
		}

		/// <summary>
		/// 播放动画直到结束才返回
		/// </summary>
		/// <param name="animationName"></param>
		/// <param name="crossFade"></param>
		/// <param name="onPlayComplete"></param>
		/// <returns></returns>
		public IEnumerator Play(string animationName, bool crossFade = true, Action<bool> onPlayComplete = null)
		{
			if (Play(animationName, crossFade))
			{
				yield return new WaitForSeconds(GetAnimationTime(animationName));
				onPlayComplete?.Invoke(true);
			}
			else
				onPlayComplete?.Invoke(false);
		}

		/// <summary>
		/// 播放动画
		/// </summary>
		/// <param name="animationName"></param>
		/// <param name="crossFade"></param>
		/// <returns></returns>
		public bool Play(string animationName, bool crossFade = true)
		{
			if (this.animationName == animationName)
				return false;

			this.animationName = animationName;

			if (crossFade)
				animator.CrossFade(animationName, crossFadeTimeNormalized, animLayer, 0);
			else
				animator.Play(animationName, animLayer, 0);

			return true;
		}

		/// <summary>
		/// 当前是否在播放这个动画
		/// </summary>
		/// <param name="clip"></param>
		/// <returns></returns>
		public bool IsPlayingAnimation(string animationName)
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
		}

		#region 内部函数
		private RuntimeAnimatorController _rootAnimatorCtrl;
		private RuntimeAnimatorController _runAnimatorCtrl;

		/// <summary>
		/// 解析根对象动画
		/// </summary>
		internal void ParseRootAnimator()
		{
			if (actor.root.gameObject.TryGetComponent<Animator>(out var animator))
			{
				this.animator = animator;
			}
			else
			{
				Log.Error($"角色根对象没有Animator。 {actor.root.ToLog()}");

				this.animator = actor.root.gameObject.AddComponent<Animator>();

				_rootAnimatorCtrl = null;
				_runAnimatorCtrl = null;
				return;
			}

			_rootAnimatorCtrl = animator.runtimeAnimatorController;
			if (_rootAnimatorCtrl == null)
			{
				Log.Error($"角色根对象没有默认动作控制器。 {actor.root.ToLog()}");

				_runAnimatorCtrl = null;
				return;
			}
			else if (_runAnimatorCtrl == null)
				_runAnimatorCtrl = _rootAnimatorCtrl;
		}

		/// <summary>
		/// 添加部位模块动作控制器
		/// </summary>
		/// <param name="ctrl"></param>
		internal void AddAnimatorController(RuntimeAnimatorController ctrl)
		{
			if (ctrl == null)
				return;

			if (_runAnimatorCtrl == ctrl)
				return;

			_runAnimatorCtrl = ctrl;
			animator.runtimeAnimatorController = _runAnimatorCtrl;
		}

		/// <summary>
		/// 删除部位模块动作控制器
		/// </summary>
		/// <param name="ctrl"></param>
		internal void RemoveAnimatorController(RuntimeAnimatorController ctrl)
		{
			if (ctrl == null)
				return;

			if (_runAnimatorCtrl != ctrl)
				return;

			_runAnimatorCtrl = _rootAnimatorCtrl;
			animator.runtimeAnimatorController = _runAnimatorCtrl;
		}

		internal void ResetAnimatorController()
		{
			var ctrl = animator.runtimeAnimatorController;
			animator.runtimeAnimatorController = null;
			animator.runtimeAnimatorController = ctrl;
		}

		private void ClearAnimator()
		{
			_rootAnimatorCtrl = null;
			_runAnimatorCtrl = null;

			animator = null;
			animationName = string.Empty;
		}
		#endregion

		#region 播放时长
		private float GetAnimationTime(string animationName)
		{
			var clips = animator.runtimeAnimatorController?.animationClips;
			if (clips == null)
				return 0;

			foreach (var clip in clips)
			{
				if (clip.name == animationName)
					return clip.length;
			}

			return 0;
		}
		#endregion
	}
}