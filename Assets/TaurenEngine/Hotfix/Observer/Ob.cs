/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/7 0:13:27
 *└────────────────────────┘*/

namespace TaurenEngine.Hotfix
{
	/// <summary>
	/// 数据观察者，用于数据绑定。
	/// 放在热更层考虑泛型对象，且业务代码用的较多，不适合在非热更层跨界调用
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

		public static implicit operator T(Ob<T> ob) 
			=> ob.Value;

		public override string ToString() 
			=> _value != null ? _value.ToString() : "null";
	}
}