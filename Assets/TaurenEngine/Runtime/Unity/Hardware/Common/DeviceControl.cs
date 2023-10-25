/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/28 17:06:32
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.Launch;

namespace TaurenEngine.Runtime
{
	/// <summary>
	/// 多单元开关控制
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class DeviceControl<T> : InstanceBase<T> where T : InstanceBase<T>, new()
	{
		private readonly Dictionary<object, bool> _data = new Dictionary<object, bool>();

		public bool Enabled { get; private set; }

		public void SetEnabled(object target, bool enabled)
		{
			_data[target] = enabled;

			if (Enabled != GetEnabled())
			{
				Enabled = !Enabled;
				UpdateEnabled();
			}
		}

		protected abstract void UpdateEnabled();

		private bool GetEnabled()
		{
			foreach (var item in _data)
			{
				if (item.Value)
					return true;
			}

			return false;
		}
	}
}