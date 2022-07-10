/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/9/26 13:44:16
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Core
{
    public abstract class MonoComponent : MonoBehaviour
    {
        private bool _isDestroyed = false;
        public bool IsDestroyed => _isDestroyed;

        /// <summary>
        /// 销毁对象。默认立即销毁
        /// <para>子类销毁逻辑写在 <c>OnDestroy</c> 中</para>
        /// </summary>
        public void Destroy() => Destroy(true);

        /// <summary>
        /// 销毁对象
        /// <para>子类销毁逻辑写在 <c>OnDestroy</c> 中</para>
        /// </summary>
        /// <param name="immediate">是否立即销毁</param>
        public void Destroy(bool immediate)
        {
            if (_isDestroyed) return;

            _isDestroyed = true;

            if (immediate) DestroyImmediate(this);
            else Destroy(this);
        }
    }
}