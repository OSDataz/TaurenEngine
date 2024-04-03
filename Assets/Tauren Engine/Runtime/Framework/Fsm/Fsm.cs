/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/12/14 20:49:03
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 有限状态机
	/// </summary>
	public class Fsm : RefrenceObject
	{
		private readonly Dictionary<Type, FsmState> _states;

		/// <summary> 当前状态 </summary>
		public FsmState _currentState;

		public Fsm()
		{
			_states = new Dictionary<Type, FsmState>();
		}

		public void Clear()
		{
			if (_currentState != null)
			{
				_currentState.OnLeave(true);
				_currentState = null;
			}

			foreach (var kv in _states)
			{
				kv.Value.OnDestroy();
			}

			_states.Clear();
		}

		protected override void OnDestroy()
		{
			if (IsDestroyed)
				return;

			Clear();

			base.OnDestroy();
		}

		public void Init(params FsmState[] states)
		{
			if (IsDestroyed)
			{
				Log.Error("状态机已被销毁");
				return;
			}

			Clear();

			foreach (var state in states)
			{
				if (state == null)
				{
					Log.Error("初始化状态机检测有空状态");
					continue;
				}

				AddState(state);
			}
		}

		public void AddState<T>() where T : FsmState, new()
		{
			AddState(new T());
		}

		private void AddState(FsmState state)
		{
			var type = state.GetType();
			if (_states.ContainsKey(type))
			{
				Log.Error($"初始化状态机检测有重复状态：{type.FullName}");
				return;
			}

			_states.Add(type, state);
			state.OnInit();
		}

		public void RemoveState<T>() where T : FsmState
		{
			var type = typeof(T);
			if (!_states.TryGetValue(type, out var state))
			{
				Log.Error($"状态机移除状态未找到：{typeof(T)}");
				return;
			}

			if (state == _currentState)
			{
				_currentState.OnLeave(false);
				_currentState = null;
			}

			state.OnDestroy();
			_states.Remove(type);
		}

		/// <summary>
		/// 切换状态
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void ChangeState<T>() where T : FsmState
		{
			ChangeState(typeof(T));
		}

		/// <summary>
		/// 切换状态
		/// </summary>
		/// <param name="stateType"></param>
		public void ChangeState(Type stateType)
		{
			if (IsDestroyed)
			{
				Log.Error("状态机已被销毁");
				return;
			}

			if (!_states.TryGetValue(stateType, out var state))
			{
				Log.Error($"有限状态机切换状态未找到：{stateType.FullName}");
				return;
			}

			if (_currentState != null)
				_currentState.OnLeave(false);

			_currentState = state;
			_currentState.OnEnter();
		}

		/// <summary>
		/// 是否有指定状态
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public bool HasState<T>() where T : FsmState
		{
			return HasState(typeof(T));
		}

		/// <summary>
		/// 是否有指定状态
		/// </summary>
		/// <param name="stateType"></param>
		/// <returns></returns>
		public bool HasState(Type stateType)
		{
			return _states.ContainsKey(stateType);
		}

		/// <summary>
		/// 是否是当期状态
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public bool IsCurrentState<T>() where T : FsmState
		{
			return _currentState != null && _currentState is T;
		}

		/// <summary>
		/// 是否是当期状态
		/// </summary>
		/// <param name="stateType"></param>
		/// <returns></returns>
		public bool IsCurrentState(Type stateType)
		{
			return _currentState != null && _currentState.GetType() == stateType;
		}
	}
}