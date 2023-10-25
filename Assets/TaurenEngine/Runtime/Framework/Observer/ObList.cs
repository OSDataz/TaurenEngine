/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/7 0:13:35
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.Runtime
{
	/// <summary>
	/// 数组观察者
	/// </summary>
	public class ObList<T>
	{
		public delegate void ValueChanged();
		public event ValueChanged onValueChanged;

		private List<T> _value;

		public ObList(List<T> value = null)
		{
			_value = value ?? new List<T>();
		}

		public List<T> Value
		{
			get => _value;
			set
			{
				if (Equals(_value, value))
					return;

				_value = value ?? new List<T>();

				onValueChanged?.Invoke();
			}
		}

		public T this[int index]
		{
			get => _value[index];
			set
			{
				if (Equals(_value[index], value))
					return;

				_value[index] = value;

				onValueChanged?.Invoke();
			}
		}

		public void Add(T item)
		{
			_value.Add(item);

			onValueChanged?.Invoke();
		}

		public void Insert(int index, T item)
		{
			_value.Insert(index, item);

			onValueChanged?.Invoke();
		}

		public bool Remove(T item)
		{
			if (_value.Remove(item))
			{
				onValueChanged?.Invoke();
				return true;
			}

			return false;
		}

		public void RemoveAt(int index)
		{
			_value.RemoveAt(index);

			onValueChanged?.Invoke();
		}

		public static implicit operator List<T>(ObList<T> ob) 
			=> ob.Value;

		public override string ToString()
			=> _value.ToString();
	}
}