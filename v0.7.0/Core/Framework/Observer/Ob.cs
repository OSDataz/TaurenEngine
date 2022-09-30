/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/7 0:13:27
 *└────────────────────────┘*/

namespace TaurenEngine
{
	/// <summary>
	/// 数据观察者，用于数据绑定
	/// </summary>
	/// <typeparam name="T"></typeparam>
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

		public override string ToString() 
			=> _value != null ? _value.ToString() : "null";
	}
}