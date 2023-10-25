/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 11:35:41
 *└────────────────────────┘*/

using System;
using System.Reflection;

namespace TaurenEngine.Runtime
{
	public static class TypeExtension
	{
		public static FieldInfo GetFieldEx(this Type type, string name, BindingFlags bindingAttr)
		{
			var info = type.GetField(name, bindingAttr);
			if (info != null)
				return info;

			var list = type.GetFields(bindingAttr);
			var len = list.Length;
			for (int i = 0; i < len; ++i)
			{
				if (list[i].Name == name && list[i].DeclaringType == type)
					return list[i];
			}

			return null;
		}

		public static MethodInfo GetMethodEx(this Type type, string name, BindingFlags bindingAttr)
		{
			var info = type.GetMethod(name, bindingAttr);
			if (info != null)
				return info;

			var list = type.GetMethods(bindingAttr);
			var len = list.Length;
			for (int i = 0; i < len; ++i)
			{
				if (list[i].Name == name && list[i].DeclaringType == type)
					return list[i];
			}

			return null;
		}
	}
}