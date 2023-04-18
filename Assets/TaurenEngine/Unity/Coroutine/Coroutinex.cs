/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/9/3 10:29:31
 *└────────────────────────┘*/

using System;
using System.Collections;
using UnityEngine;

namespace TaurenEngine.Unity
{
	/// <summary>
	/// 自定义协程
	/// todo 使用结构待具体思考设计
	/// </summary>
	public class Coroutinex : CustomYieldInstruction
	{
		private MonoBehaviour _behaviour;
		private Coroutine _coroutine;
		private bool _isFinished = false;

		private Action<Coroutinex> _onComplete;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="behaviour"></param>
		/// <param name="enumerator"></param>
		/// <param name="onComplete">执行完成回调</param>
		public void Start(MonoBehaviour behaviour, IEnumerator enumerator, Action<Coroutinex> onComplete)
		{
			_behaviour = behaviour;
			_onComplete = onComplete;

			_coroutine = behaviour.StartCoroutine(CoroutineAux(enumerator));
		}

		private IEnumerator CoroutineAux(IEnumerator enumerator)
		{
			_isFinished = false;
			yield return enumerator;
			_isFinished = true;

			_coroutine = null;
			_onComplete?.Invoke(this);
		}

		public void Stop()
		{
			if (_coroutine != null)
			{
				_behaviour.StopCoroutine(_coroutine);
				_onComplete?.Invoke(this);

				_coroutine = null;
				_isFinished = true;
			}
		}

		public override bool keepWaiting => _coroutine != null && _isFinished;
	}
}