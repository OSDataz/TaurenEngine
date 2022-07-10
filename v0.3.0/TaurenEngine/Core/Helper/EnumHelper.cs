/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/22 21:08:09
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace TaurenEngine.Core
{
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetEnumList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).OfType<T>().ToList();
        }
	}
}