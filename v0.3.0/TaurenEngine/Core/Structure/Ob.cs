/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2022/1/15 20:51:34
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	/// <summary>
	/// Observable
	/// </summary>
	public class Ob<T>
	{
		public delegate void ValueChanged(T oldValue, T newValue);
		public event ValueChanged onValueChanged;

		private T _value;

		public T Value
		{
			get => _value;
			set 
			{
				if (Equals(_value, value))
					return;

				T old = _value;
				_value = value;

				onValueChanged?.Invoke(old, _value);
			}
		}

		public Ob(T value = default)
		{
			_value = value;
		}

		public static implicit operator T(Ob<T> ob) => ob.Value;

		public override string ToString() => _value != null ? _value.ToString() : "null";
	}
}