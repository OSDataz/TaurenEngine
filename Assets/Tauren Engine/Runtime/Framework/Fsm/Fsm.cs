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
	public class Fsm<TState> : RefrenceObject where TState : FsmState
	{
		private readonly Dictionary<Type, TState> _states;

		public Fsm()
		{
			_states = new Dictionary<Type, TState>();
		}

		public void Clear()
		{
			LeaveCurrentState(true);

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

		#region 【可选】预初始化
		/// <summary>
		/// 【可选】提前初始化状态
		/// </summary>
		/// <param name="states"></param>
		public void Init(params TState[] states)
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

				Add(state);
			}
		}

		public TState Create<T>() where T : TState, new()
		{
			var type = typeof(T);
			if (_states.TryGetValue(type, out var state))
			{
				Log.Error($"初始化状态机检测有重复状态：{type.FullName}");
				return state;
			}

			state = new T();
			Add(state);
			return state;
		}

		protected virtual bool Add(TState state)
		{
			var type = state.GetType();
			if (_states.ContainsKey(type))
			{
				Log.Error($"初始化状态机检测有重复状态：{type.FullName}");
				return false;
			}

			_states.Add(type, state);
			state.OnInit();
			return true;
		}

		public void Destroy<T>() where T : TState
		{
			var type = typeof(T);
			if (!_states.TryGetValue(type, out var state))
			{
				Log.Error($"状态机移除状态未找到：{typeof(T)}");
				return;
			}

			if (state == _currentState)
			{
				LeaveCurrentState(true);
			}
			else
			{
				state.OnDestroy();
			}

			_states.Remove(type);
		}
		#endregion

		#region 当前状态
		/// <summary> 当前状态 </summary>
		private TState _currentState;

		private void LeaveCurrentState(bool isDestroy)
		{
			if (_currentState == null)
				return;

			_currentState.OnLeave();

			if (isDestroy)
				_currentState.OnDestroy();

			_currentState = null;
		}
		#endregion

		#region 切换状态
		/// <summary>
		/// 切换状态
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void Change<T>() where T : TState, new()
		{
			if (IsDestroyed)
			{
				Log.Error("状态机已被销毁");
				return;
			}

			var type = typeof(T);
			if (!_states.TryGetValue(type, out var state))
			{
				state = Create<T>();
			}

			LeaveCurrentState(false);

			_currentState = state;
			_currentState.OnEnter();
		}
		#endregion

		#region 判断接口
		/// <summary>
		/// 是否有指定状态
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public bool Has<T>() where T : TState
		{
			return Has(typeof(T));
		}

		/// <summary>
		/// 是否有指定状态
		/// </summary>
		/// <param name="stateType"></param>
		/// <returns></returns>
		public bool Has(Type stateType)
		{
			return _states.ContainsKey(stateType);
		}

		/// <summary>
		/// 是否是当期状态
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public bool CheckCurrent<T>() where T : TState
		{
			return _currentState != null && _currentState is T;
		}

		/// <summary>
		/// 是否是当期状态
		/// </summary>
		/// <param name="stateType"></param>
		/// <returns></returns>
		public bool CheckCurrent(Type stateType)
		{
			return _currentState != null && _currentState.GetType() == stateType;
		}
		#endregion
	}
}