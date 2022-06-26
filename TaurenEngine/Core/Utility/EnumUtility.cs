/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:55:26
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace TaurenEngine.Core
{
    public static class EnumUtility
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