/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 17:06:32
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.Core;

namespace TaurenEngine.HardwareEx
{
	/// <summary>
	/// 多单元开关控制
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class MultSwitchControl<T> : Singleton<T> where T : class, new()
	{
		private readonly Dictionary<int, bool> _data = new Dictionary<int, bool>();

		public bool Enabled { get; private set; }

		public void SetEnabled(object o, bool enabled)
		{
			SetEnabled(o.GetHashCode(), enabled);
		}

		public void SetEnabled(int key, bool enabled)
		{
			if (_data.ContainsKey(key))
				_data[key] = enabled;
			else
				_data.Add(key, enabled);

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