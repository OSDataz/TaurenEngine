/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/24 10:57:07
 *└────────────────────────┘*/

using System.Reflection;

namespace Tauren.Core.Runtime
{
	public static class AssemblyExtension
	{
		/// <summary>
		/// 反射执行程序集指定类的指定函数
		/// </summary>
		/// <param name="object"></param>
		/// <param name="typeFullName"></param>
		/// <param name="methodName"></param>
		/// <param name="paramList"></param>
		public static void Invoke(this Assembly @object, string typeFullName, string methodName, params object[] paramters)
		{
			var type = @object.GetType(typeFullName);
			if (type == null)
			{
				Log.Error($"程序集{@object}找不到类型{typeFullName}");
				return;
			}

			var method = type.GetMethod(methodName);
			if (method == null)
			{
				Log.Error($"程序集{@object}类型{typeFullName}找不到函数{methodName}");
				return;
			}

			if (paramters?.Length > method.GetParameters().Length)
			{
				Log.Error($"程序集{@object}类型{typeFullName}函数{methodName}传参数异常，函数参数数：{method.GetParameters().Length} 传入参数数：{paramters.Length}");
				return;
			}

			method.Invoke(null, paramters);
		}
	}
}