/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/9/8 10:13:51
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEngine.Core
{
    /// <summary>
    /// 超级对象池，支持各种类型的对象添加和取出
    /// </summary>
    public sealed class SuperPool : Singleton<SuperPool>
    {
        private readonly Dictionary<Type, object> _map = new Dictionary<Type, object>();

        private ObjectPool<T> GetPool<T>() where T : IRecycle, new()
        {
            Type type = typeof(T);
            if (_map.ContainsKey(type))
                return (ObjectPool<T>)_map[type];

            var pool = new ObjectPool<T>();
            _map.Add(type, pool);
            return pool;
        }

        private ObjectPool<T> TryGetPool<T>() where T : IRecycle, new()
        {
            Type type = typeof(T);
            if (_map.ContainsKey(type))
                return (ObjectPool<T>)_map[type];

            return null;
        }

        #region 调用接口
        /// <summary>
        /// 向对象池放入一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add<T>(T item) where T : IRecycle, new()
        {
            return GetPool<T>().Add(item);
        }

        /// <summary>
        /// 从对象池取出一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : IRecycle, new()
        {
            return GetPool<T>().Get();
        }

        /// <summary>
        /// 对象池中是否有该对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains<T>(T item) where T : IRecycle, new()
        {
            return GetPool<T>().Contains(item);
        }

        /// <summary>
        /// 设置对象池最大缓存数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maxCount"></param>
        public void SetMaximum<T>(int maximum) where T : IRecycle, new()
        {
            GetPool<T>().Maximum = maximum;
        }

        /// <summary>
        /// 清理对象池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ClearPool<T>() where T : IRecycle, new()
        {
            TryGetPool<T>()?.Clear();
        }

        /// <summary>
        /// 销毁对象池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DestroyPool<T>() where T : IRecycle, new()
        {
            TryGetPool<T>()?.Destroy();
            _map.Remove(typeof(T));
        }
        #endregion
    }
}