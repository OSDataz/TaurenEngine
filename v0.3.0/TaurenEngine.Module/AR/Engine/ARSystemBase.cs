/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.AR
{
	internal abstract class ARSystemBase<T> where T : AREngineBase
	{
		protected T _arEngine;

		public bool IsEnable { get; private set; }
		public bool IsAvailable { get; protected set; }

		private bool _isInit = false;
		public bool IsDestroyed { get; private set; }

		public ARSystemBase(T arEngine)
		{
			_arEngine = arEngine;
		}

		protected virtual void Init()
		{
			TDebug.Log($"初始化AR组件：{GetType().Name} IsAvailable：{IsAvailable}");
		}

		public virtual void Enable()
		{
			IsEnable = true;

			if (!_isInit)
			{
				Init();
				_isInit = true;
			}
		}

		public virtual void Disable()
		{
			IsEnable = false;
		}

		public virtual void Destroy()
		{
			_arEngine = null;
			IsDestroyed = true;
		}
    }
}