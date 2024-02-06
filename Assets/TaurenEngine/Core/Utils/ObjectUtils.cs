/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/12/1 12:05:13
 *└────────────────────────┘*/

using System.Reflection;
using System;

namespace TaurenEngine.Core
{
	public static class ObjectUtils
	{
		/// <summary>
		/// 将oriObject的值克隆到toObject
		/// </summary>
		/// <param name="oriObject"></param>
		/// <param name="toObject"></param>
		public static void CopyValue(object oriObject, object toObject)
		{
			var oriType = oriObject.GetType();
			var toType = toObject.GetType();
			if (oriType != toType)
			{
				Log.Error($"克隆数值，两者对象不一样。oriObject:{oriObject} toObject:{toObject}");
				return;
			}

			BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
			PropertyInfo[] pinfos = toType.GetProperties(flags);
			var len = pinfos.Length;
			for (int i = 0; i < len; ++i)
			{
				var pinfo = pinfos[i];
				if (pinfo.CanWrite && pinfo.CanRead)
				{
					try
					{
						pinfo.SetValue(toObject, pinfo.GetValue(oriObject));
					}
					catch (Exception ex)
					{
						Log.Error($"克隆数值字段错误，字段：{pinfo.Name} {ex}");
					}
				}
			}

			FieldInfo[] fInfos = toType.GetFields(flags);
			len = fInfos.Length;
			for (int i = 0; i < len; ++i)
			{
				var fInfo = fInfos[i];
				fInfo.SetValue(toObject, fInfo.GetValue(oriObject));
			}
		}
	}
}